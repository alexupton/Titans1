using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public static class AI
    {
        //runs path algorithm and checks if there is
        //path between two points
        public static bool PathExists(Tile startTile, Tile endTile, Map map, List<Tile> rangeSet, int speed)
        {
            //HOLY SHIT, A WORKING A* ALGORITHM
            startTile.IsRoot = true;
            List<Tile> open = new List<Tile>();
            List<Tile> closed = new List<Tile>();
            open.Add(startTile);

            if (startTile == endTile)
            {
                return false;
            }

            Tile current = new Tile();

            while (open.Count > 0)
            {
                int LowestFScore = 10000;
                for (int i = 0; i < open.Count; i++ )
                {
                    if (open.ElementAt(i).FScore < LowestFScore)
                    {
                        LowestFScore = open.ElementAt(i).FScore;
                        current = open.ElementAt(i);
                    }
                }
                open.Remove(current);
                closed.Add(current);

                List<Tile> adjacent = GetAdjacentLegalTiles(map, current);
                List<Tile> adjacentTemp = new List<Tile>();
                foreach (Tile adj in adjacent)
                {
                    if(rangeSet.Contains(adj))
                    {
                        adjacentTemp.Add(adj);
                    }

                    if (closed.Contains(adj))
                    {
                        adjacentTemp.Remove(adj);
                    }
                }

                adjacent = adjacentTemp;

                foreach (Tile tile in adjacent)
                {
                    if (!open.Contains(tile))
                    {
                        open.Add(tile);
                    }

                    tile.parentTile = current;
                    tile.GScore = GetGScore(tile);
                    tile.HScore = GetManhattanDistance(current, endTile);
                    tile.FScore = tile.GScore + tile.HScore;

                    if (open.Contains(tile))
                    {
                        Tile test = new Tile();
                        foreach (Tile o in open)
                        {
                            if (o == tile)
                            {
                                test = o;
                            }
                        }

                        if (tile.GScore < test.GScore)
                        {
                            test.parentTile = current;
                            test.GScore = GetGScore(test);
                            test.FScore = test.GScore + test.HScore;
                        }
                    }

                    
                }

                if(closed.Contains(endTile))
                {
                    List<Tile> path = BuildPath(endTile);
                    if (Reachable(path, speed))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


            }

            return false;


        }

        public static bool Reachable(List<Tile> path, int speed)
     {
         int totalDistance = 0;
         foreach (Tile tile in path)
         {
             totalDistance += tile.MoveCost;
         }

         if (totalDistance > speed)
         {
             return false;
         }
         else
         {
             return true;
         }
     }
     public static List<Tile> GetAdjacentLegalTiles(Map map, Tile currentTile)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            int x = currentTile.X;
            int y = currentTile.Y;

            if (x - 1 >= 0)
            {
                Tile left = map.map[x - 1][y];
                if (!left.IsImpassible)
                {
                    adjacentTiles.Add(left);

                }
            }

            if (x + 1 < map.Size[0])
            {
                Tile right = map.map[x + 1][y];
                    if(!right.IsImpassible)
                    {
                        adjacentTiles.Add(right);
                    }
            }

            if(y - 1 >= 0)
            {
                Tile up = map.map[x][y - 1];
                if(!up.IsImpassible)
                   adjacentTiles.Add(up);
                
            }

            if(y + 1 < map.Size[1])
            {
                Tile down = map.map[x][y + 1];
                if(!down.IsImpassible)
                {
                    adjacentTiles.Add(down);
                }
            }

            return adjacentTiles;
            
        }
        /// <summary>
        /// Heuristic function for pathfinding. Returns the straight line distance between two tiles.
        /// </summary>
        /// <param name="testTile"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static int GetManhattanDistance(Tile testTile, Tile destination)
        {
            int mDistance = 0;
            if (testTile.X > destination.X)
            {
                mDistance += (testTile.X - destination.X);
            }
            
            if(testTile.X < destination.X)
            {
                mDistance += (destination.X - testTile.X);
            }

            if (testTile.Y > destination.Y)
            {
                mDistance += (testTile.Y - destination.Y);
            }

            if (testTile.Y < destination.Y)
            {
                mDistance += (destination.Y - testTile.Y);
            }

            return mDistance;
        }

        public static int GetFCost(Map map, Tile tile, Tile destination)
        {
            int GCost = 0;
            bool root = false;
            Tile test = tile;
            while (!root)
            {
                GCost += test.MoveCost;
                if (test.IsRoot)
                {
                    root = true;
                }
                else
                {
                    test = test.parentTile;
                }
            }
            int HCost = (GetManhattanDistance(tile, destination));
            return GCost + HCost;
        }

        public static int GetGScore(Tile tile)
        {
            int GScore = 0;
            bool root = false;
            Tile test = tile;
            try
            {
                if (tile.parentTile == null)
                    tile.IsRoot = true;
            }
            catch
            {
                tile.IsRoot = true;
            }
            while (!root)
            {
                GScore += test.MoveCost;
                if (test.IsRoot)
                {
                    root = true;
                }
                else
                {
                    test = test.parentTile;
                }
            }
            return GScore;
        }

        public static List<Tile> BuildPath(Tile endpoint)
        {
            List<Tile> path = new List<Tile>();
            bool root = false;
            Tile current = endpoint;
            while (!root)
            {
                path.Add(current);
                if (current.IsRoot)
                {
                    root = true;
                }
                else
                {
                    current = current.parentTile;
                }
                
            }
            return path;
        }



        //same as above, only returns the path
        public static List<Tile> GetPath(Tile startTile, Tile endTile, Map map)
        {

            //HOLY SHIT, ANOTHER WORKING A* ALGORITHM
            startTile.IsRoot = true;
            List<Tile> open = new List<Tile>();
            List<Tile> closed = new List<Tile>();
            open.Add(startTile);

            Tile current = new Tile();

            while (open.Count > 0)
            {
                int LowestFScore = 10000;
                for (int i = 0; i < open.Count; i++ )
                {
                    if (open.ElementAt(i).FScore < LowestFScore)
                    {
                        LowestFScore = open.ElementAt(i).FScore;
                        current = open.ElementAt(i);
                    }
                }
                open.Remove(current);
                closed.Add(current);

                List<Tile> adjacent = GetAdjacentLegalTiles(map, current);
                List<Tile> adjacentTemp = new List<Tile>();
                foreach (Tile adj in adjacent)
                {
                    adjacentTemp.Add(adj);

                    if (closed.Contains(adj))
                    {
                        adjacentTemp.Remove(adj);
                    }
                }

                adjacent = adjacentTemp;

                foreach (Tile tile in adjacent)
                {
                    if (!open.Contains(tile))
                    {
                        open.Add(tile);
                    }

                    tile.parentTile = current;
                    tile.GScore = GetGScore(tile);
                    tile.HScore = GetManhattanDistance(current, endTile);
                    tile.FScore = tile.GScore + tile.HScore;

                    if (open.Contains(tile))
                    {
                        Tile test = new Tile();
                        foreach (Tile o in open)
                        {
                            if (o == tile)
                            {
                                test = o;
                            }
                        }

                        if (tile.GScore < test.GScore)
                        {
                            test.parentTile = current;
                            test.GScore = GetGScore(test);
                            test.FScore = test.GScore + test.HScore;
                        }
                    }

                    
                }

                if(closed.Contains(endTile))
                {
                    List<Tile> path = BuildPath(endTile);
                    return path;
                }




            }

            return new List<Tile>();        



        }
     
    }


}
