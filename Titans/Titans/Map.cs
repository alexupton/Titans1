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
            map[mover.Location[0]][mover.Location[1]].TileUnit = null;
            map[mover.Location[0]][mover.Location[1]].hasUnit = false;
            map[mover.Location[0]][mover.Location[1]].IsImpassible = false;

            map[X][Y].TileUnit = mover;
            mover.Location[0] = X; //update unit location
            mover.Location[1] = Y;
            map[X][Y].hasUnit = true;
            map[X][Y].IsImpassible = true;
        }
        //return the tile object at a given coordinate pair
        public Tile GetTileAt(int X, int Y)
        {
            if (X >= 0 && Y >= 0)
            {
                return map[X][Y];
            }
            else
                return new Tile();
        }

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
                if ((Math.Abs(tile.X - location.X) + Math.Abs(tile.Y - location.Y)) <= tileRange && !tile.IsImpassible)
                {
                    trueReachableTiles.Add(tile);
                }
            }
            //...
            List<Tile> tempTileList = new List<Tile>();
            foreach (Tile tile in trueReachableTiles)
            {
                tile.IsRoot = false;
                tile.parentTile = null;
                tile.HScore = 0;
                tile.GScore = 0;
                tile.FScore = 0;
                if(AI.PathExists(location, tile, this, trueReachableTiles, selected.Speed))
                {
                    tempTileList.Add(tile);
                }
            }
            //...
            trueReachableTiles = tempTileList;

            //cleanup
            foreach (Tile tile in trueReachableTiles)
            {
                tile.IsRoot = false;
                tile.parentTile = null;
                tile.FScore = 0;
                tile.GScore = 0;
                tile.HScore = 0;
            }


            foreach (Tile tile in trueReachableTiles)
            {
                legalmoves.Add(new int[] { tile.X, tile.Y });
            }

        

            
            return legalmoves;
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

        //Clear one particular Highlight
        public void ClearSpecificHighlight(int x, int y)
        {
            map[x][y].ClearHighlight();
            map[x][y].IsHighlighted = false;
        }



    }
}
