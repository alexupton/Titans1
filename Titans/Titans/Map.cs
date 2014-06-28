using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Titans
{
    public class Map
    {
        
        public int[] Size { get; set; } //map size in X, Y directions
        public int TileSet { get; set; } //which tile set determined by this number
        public int Music { get; set; } //determines which music track plays

        public Tile[][] map { get; set; } //the actual map state. Game.Draw() will read this to draw the screen

        public Unit SavedUnit { get; set; } //used to remember unit locations when they are moved over i.e. by an air unit
       
        public Map(int[] size, int tileSet, int music)
        {
            Size = size;
            TileSet = tileSet;
            Music = music;
            map = new Tile[size[0]][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Tile[size[1]];
            }
            
        }

        //Move one Unit to a new spot on the map
        //Note there is no checking to make sure it is a legal move. This is done elsewhere
        public void Move(Unit mover, int X, int Y)
        {
            
            if (SavedUnit != null)
            {
                map[mover.Location[0]][mover.Location[1]].TileUnit = SavedUnit;
                map[mover.Location[0]][mover.Location[1]].hasUnit = true;
                
                    map[mover.Location[0]][mover.Location[1]].IsImpassible = true;

                SavedUnit = null;
            }
            else
            {
                map[mover.Location[0]][mover.Location[1]].TileUnit = null;
                map[mover.Location[0]][mover.Location[1]].hasUnit = false;
                map[mover.Location[0]][mover.Location[1]].IsImpassible = false;
                if (map[mover.Location[0]][mover.Location[1]].type == "water")
                {
                    map[mover.Location[0]][mover.Location[1]].IsImpassible = true;
                }
                else
                {
                    map[mover.Location[0]][mover.Location[1]].IsImpassible = false;
                }
            }
            if (map[X][Y].hasUnit)
            {
                SavedUnit = map[X][Y].TileUnit;
            }
            map[X][Y].TileUnit = mover;
            map[X][Y].hasUnit = true;
            map[X][Y].IsImpassible = true;
            
            
            mover.Location[0] = X; //update unit location
            mover.Location[1] = Y;


            
        }
        //return the tile object at a given coordinate pair
        public Tile GetTileAt(int X, int Y)
        {
            if (X >= 0 && Y >= 0 && X < Size[0] && Y < Size[1])
            {
                return map[X][Y];
            }
            else
                return new Tile();
        }

        //Get tile where selected unit is at and find possible reachable tiles
        public List<int[]> GetLegalMoveCoordinates(Unit selected)
        {
            List<int[]> legalmoves = new List<int[]>();

            Tile location = GetTileAt(selected.Location[0], selected.Location[1]);

            int tileRange = selected.Speed / 25;

            List<Tile> reachableTiles = new List<Tile>();
            for (int i = -tileRange; i <= tileRange; i++)
            {
                for (int j = -tileRange; j <= tileRange; j++)
                {
                    if ((i + location.X) < Size[0] && (j + location.Y) < Size[1] && (i + location.X) >= 0 & (j + location.Y) >= 0)
                    {
                        reachableTiles.Add(GetTileAt(i + location.X, j + location.Y) );
                    }
                }
            }

            List<Tile> trueReachableTiles = new List<Tile>();

            foreach (Tile tile in reachableTiles)
            {
                if ((Math.Abs(tile.X - location.X) + Math.Abs(tile.Y - location.Y)) <= tileRange)
                {
                    if (selected is Scout || selected is Bomber || selected is Fighter || !tile.IsImpassible)
                    {
                        trueReachableTiles.Add(tile);
                    }
                }
            }
            //Run possible tiles through pathfinding algorithm to see what tiles can actually be reached
            List<Tile> tempTileList = new List<Tile>();
            if (!(selected is Scout || selected is Bomber || selected is Fighter))
            {
                foreach (Tile tile in trueReachableTiles)
                {
                    tile.IsRoot = false;
                    tile.parentTile = null;
                    tile.HScore = 0;
                    tile.GScore = 0;
                    tile.FScore = 0;
                    if (AI.PathExists(location, tile, this, trueReachableTiles, selected.Speed, selected))
                    {
                        tempTileList.Add(tile);
                    }
                }

                //Set list equal to actual tiles you can move to
                trueReachableTiles = tempTileList;

                //cleanup
                foreach (Tile tile in trueReachableTiles)
                {
                    tile.IsRoot = false;
                    tile.parentTile = null;
                    tile.FScore = 0;
                    tile.GScore = 0;
                    tile.HScore = 0;
                    tile.IsRedHighlighted = false;
                }


                foreach (Tile tile in trueReachableTiles)
                {
                    legalmoves.Add(new int[] { tile.X, tile.Y });
                }

            }
            else
            {
                foreach (Tile tile in trueReachableTiles)
                {
                    if (!(tile.hasUnit))
                    {
                        legalmoves.Add(new int[]{tile.X, tile.Y});
                    }
                }
            }

            
            return legalmoves;
        }

        //get a list of tiles that are legal to move to
        //way better than the shitty coordinates thing
        public List<Tile> GetLegalMoveTiles(Unit active)
        {
            List<int[]> legalCoords = GetLegalMoveCoordinates(active);
            List<Tile> legalTiles = new List<Tile>();
            foreach(int[] coords in legalCoords)
            {
                legalTiles.Add(GetTileAt(coords[0], coords[1]));
            }

            return legalTiles;
        }

        //highlight entire unit attack range, highlighting enemies in red
        public void HighlightAttack(Unit active)
        {
            List<Tile> tileRange = GetAttackRangeTiles(active);
            foreach (Tile tile in tileRange)
            {
                if (AI.HasEnemyUnit(tile, active))
                {
                    AddSpecificRedHighlight(tile.X, tile.Y);
                }
                else
                    AddSpecificBlueHighlight(tile.X, tile.Y);
            }
        }

        //get the set of tiles that are within the attack range
        public List<Tile> GetAttackRangeTiles(Unit active)
        {
            int x = active.Location[0];
            int y = active.Location[1];
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();
            int range = active.Range;
            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < Size[0] && (j + y) < Size[1] && (i + x) >= 0 & (j + y) >= 0)
                    {
                        rangeSquare.Add(new int[] { i + x, j + y });
                    }
                }
            }

            foreach (int[] tile in rangeSquare)
            {
                if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                {
                    actualRange.Add(tile);
                }
            }
            List<Tile> tileRange = new List<Tile>();
            foreach (int[] tile in actualRange)
            {
                tileRange.Add(map[tile[0]][tile[1]]);
            }
            return tileRange;
        }

        //gets a theoretical attack range of the specified unit at the specified test tile
        public List<Tile> GetAttackRangeTiles(Tile test, Unit active)
        {
            int x = test.X;
            int y = test.Y;
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();
            int range = active.Range;
            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < Size[0] && (j + y) < Size[1] && (i + x) >= 0 & (j + y) >= 0)
                    {
                        rangeSquare.Add(new int[] { i + x, j + y });
                    }
                }
            }

            foreach (int[] tile in rangeSquare)
            {
                if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                {
                    actualRange.Add(tile);
                }
            }
            List<Tile> tileRange = new List<Tile>();
            foreach (int[] tile in actualRange)
            {
                tileRange.Add(map[tile[0]][tile[1]]);
            }
            return tileRange;
        }
        //highlight entire unit range, allies highlighted in red
        public void HighlightAllies(Unit active)
        {
            int x = active.Location[0];
            int y = active.Location[1];
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();
            int range = active.Range;
            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < Size[0] && (j + y) < Size[1] && (i + x) >= 0 & (j + y) >= 0)
                    {
                        rangeSquare.Add(new int[] { i + x, j + y });
                    }
                }
            }

            foreach (int[] tile in rangeSquare)
            {
                if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                {
                    actualRange.Add(tile);
                }
            }
            List<Tile> tileRange = new List<Tile>();
            foreach (int[] tile in actualRange)
            {
                tileRange.Add(map[tile[0]][tile[1]]);
            }
            foreach (Tile tile in tileRange)
            {
                if (tile.hasUnit && !AI.HasEnemyUnit(tile, active))
                {
                    AddSpecificRedHighlight(tile.X, tile.Y);
                }
                else
                {
                    AddSpecificBlueHighlight(tile.X, tile.Y);
                }
            }
        
        }

        //Takes a set of tile coordinates and highlights each tile in the set
        public void HighlightTiles(List<int[]> tileCoordinates)
        {
            foreach (int[] coords in tileCoordinates)
            {
                int X = coords[0];
                int Y = coords[1];

                map[X][Y].Highlight();
                map[X][Y].IsHighlighted = true;
            }
        }

        public void RedHighlightTiles(List<int[]> tiles)
        {
            foreach (int[] coords in tiles)
            {
                int x = coords[0];
                int y = coords[1];

                map[x][y].RedHighlight();
            }
        }

        public void HighlightTiles(List<Tile> tiles)
        {
            foreach (Tile t in tiles)
            {
                t.Highlight();
            }
        }

        public void RedHighlightTiles(List<Tile> tiles)
        {
            foreach (Tile t in tiles)
            {
                t.RedHighlight();
            }
        }

        public void BlueHighlightTiles(List<Tile> tiles)
        {
            foreach (Tile t in tiles)
            {
                t.BlueHighlight();
            }
        }
        

        //Remove any and all highlighting from the map
        public void ClearHighlights()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++ )
                {
                    map[i][j].ClearHighlight();
                }
            }
        }

        public void ClearRedHighlights()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j].ClearRedHighlight();
                }
            }
        }

        public void ClearBlueHighlights()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j].ClearBlueHighlight();
                }
            }
        }
        public void ClearAllHighlights()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j].ClearHighlight();
                    map[i][j].ClearRedHighlight();
                    map[i][j].ClearBlueHighlight();
                }
            }
        }

        //Add one particular Highlight
        public void AddSpecificHighlight(int x, int y)
        {
            map[x][y].Highlight();
            map[x][y].IsHighlighted = true;
        }

        public void AddSpecificRedHighlight(int x, int y)
        {
            map[x][y].RedHighlight();
        }

        public void AddSpecificBlueHighlight(int x, int y)
        {
            map[x][y].BlueHighlight();
        }


        //Clear one particular Highlight
        public void ClearSpecificHighlight(int x, int y)
        {
            map[x][y].ClearHighlight();
            map[x][y].IsHighlighted = false;
        }

        public void ClearSpecificRedHighlight(int x, int y)
        {
            map[x][y].ClearRedHighlight();
            map[x][y].IsRedHighlighted = false;
        }



    }
}
