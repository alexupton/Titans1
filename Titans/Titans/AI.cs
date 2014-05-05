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
                    //if(rangeSet.Contains(adj))
                    //{
                        adjacentTemp.Add(adj);
                    //}

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

        public static void MakeAIMove(Battle battle, Unit active)
        {
            if (!battle.GameUI.taunted && active.AP > 0)
            {
                Map map = battle.BattleMap;
                Tile location = map.GetTileAt(active.Location[0], active.Location[1]);

                //the AI favors nearby units with low health, preferrably out of range of enemies
                List<Unit> enemies = new List<Unit>();
                foreach (Unit unit in battle.BattleQueue)
                {
                    if (unit.isPlayerUnit)
                        enemies.Add(unit);
                }

                Unit nearestEnemy = new Soldier();
                int nearestEnemyDistance = 30000;
                Tile nearestLocation = new Tile();

                //calculate the closest enemy
                foreach (Unit enemy in enemies)
                {
                    Tile enemyTile = map.GetTileAt(enemy.Location[0], enemy.Location[1]);

                    List<Tile> enemyAdjTiles = GetAdjacentLegalTiles(map, enemyTile);
                    foreach (Tile adj in enemyAdjTiles)
                    {
                        List<Tile> path = GetPath(location, adj, map);
                        foreach (Tile p in path)
                        {
                            int pathTotal = 0;
                            p.FScore = 0;
                            p.GScore = 0;
                            p.HScore = 0;
                            p.parentTile = null;
                            pathTotal += p.MoveCost;
                            if (pathTotal < nearestEnemyDistance)
                            {
                                nearestEnemyDistance = pathTotal;
                                nearestEnemy = enemy;
                                nearestLocation = adj;
                            }
                        }
                    }




                }





                //calculate the best unit, if any, in range
                Unit attackEnemy;
                int range = active.Range;
                int x = active.Location[0];
                int y = active.Location[1];
                List<int[]> rangeSquare = new List<int[]>();

                for (int i = (range * -1); i <= range; i++)
                {
                    for (int j = (range * -1); j <= range; j++)
                    {
                        if ((i + x) < map.Size[0] && (j + y) < map.Size[1] && (i + x) >= 0 & (j + y) >= 0)
                        {
                            rangeSquare.Add(new int[] { i + x, j + y });
                        }
                    }
                }
                List<Tile> actualRange = new List<Tile>();
                foreach (int[] tile in rangeSquare)
                {
                    if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                    {
                        actualRange.Add(map.GetTileAt(tile[0], tile[1]));
                    }
                }

                List<Unit> attackableUnits = new List<Unit>();
                foreach (Tile tile in actualRange)
                {
                    if (tile.hasUnit)
                    {
                        if (tile.TileUnit.isPlayerUnit)
                        {
                            attackableUnits.Add(tile.TileUnit);
                        }
                    }
                }

                //look for the enemy with the highest HP that can be killed in one or two hits
                if (attackableUnits.Count > 0)
                {
                    int highestKillableHP = 0;
                    Unit highestKillableUnit = new Soldier();
                    foreach (Unit enemy in attackableUnits)
                    {
                        if (AttackResolver.Attack(active, enemy, active.AttackModifiers) * active.AP > enemy.HP && enemy.HP > highestKillableHP)
                        {
                            highestKillableHP = enemy.HP;
                            highestKillableUnit = enemy;
                        }
                    }
                    int highestDamage = 0;
                    Unit highestDamageUnit = new Soldier();
                    //if no enemy is killable, find the one we can do the greatest damage to
                    if (highestKillableHP == 0)
                    {
                        foreach (Unit enemy in attackableUnits)
                        {
                            int damage = AttackResolver.Attack(active, enemy, active.AttackModifiers);
                            if (damage * active.AP > highestDamage)
                            {
                                highestDamage = damage * active.AP;
                                highestDamageUnit = enemy;
                            }
                        }
                        attackEnemy = highestDamageUnit;
                    }
                    else
                    {
                        attackEnemy = highestKillableUnit;
                    }


                    battle.GameUI.unitDamage = battle.Attack(attackEnemy);
                    battle.GameUI.attackedUnitTrueX = attackEnemy.Location[0] * 55 - 13;
                    battle.GameUI.attackedUnitTrueY = attackEnemy.Location[1] * 55 - 20;
                    battle.GameUI.displayDamage = true;
                    battle.GameUI.timeSinceLastDamageFrame = 0;
                    battle.GameUI.wait = true;
                    return;
                }



                //if the unit is weak, defend
                if (active.HP < active.MaxHP / 4 && active.AP == 1 && !(active is Ranger))
                {
                    battle.SelectDefend();
                    battle.GameUI.timeSinceLastDamageFrame = 0;
                    battle.GameUI.wait = true;
                    return;
                }

                if (!battle.GameUI.rooted)
                {
                    //now we check on which tile to move to
                    List<int[]> reachableTilesAsInt = map.GetLegalMoveCoordinates(active);
                    List<Tile> reachableTiles = new List<Tile>();

                    foreach (int[] coords in reachableTilesAsInt)
                    {
                        reachableTiles.Add(map.GetTileAt(coords[0], coords[1]));
                    }


                    Tile closest = map.GetTileAt(active.Location[0], active.Location[1]);
                    int nearestTileToEnemy = 30000;

                    foreach (Tile test in reachableTiles)
                    {
                        if (!test.hasUnit)
                        {
                            int pathCost = 0;
                            List<Tile> path = GetPath(nearestLocation, test, map);
                            foreach (Tile p in path)
                            {
                                pathCost += p.MoveCost;
                            }

                            if (pathCost < nearestTileToEnemy)
                            {
                                nearestTileToEnemy = pathCost;
                                closest = test;
                            }
                        }
                    }
                    if (closest != map.GetTileAt(active.Location[0], active.Location[1]))
                    {
                        battle.StartMove(closest);
                        battle.GameUI.timeSinceLastDamageFrame = 0;
                        battle.GameUI.wait = true;
                        return;
                    }
                    else
                    {
                        battle.GameUI.sfx.PlayPassSound(active);
                        active.AP = 0;
                    }
                }
                else if (active.AP > 0 && battle.GameUI.taunted)
                {
                    Taunt effect = null;
                    Unit Target = new Soldier();
                    Target.StatusEffects = new List<StatusEffect>();
                    foreach (StatusEffect status in active.StatusEffects)
                    {
                        if (status is Taunt)
                        {
                            effect = (Taunt)status;
                        }
                    }

                        Target = effect.TauntTarget;
                        if (active.StatusEffects.Count > 0)
                        {
                            MakeTauntedMove(battle, active, Target);
                        }
                }
                else
                {
                    active.AP = 0;
                    battle.GameUI.wait = true;
                    battle.GameUI.sfx.PlayPassSound(active);
                }
            }
            else
            {
                active.AP = 0;
                if (!(active is Ranger) && !HasStatusEffect(active, "Stun"))
                {
                    active.DefenseModifiers.Add(5);
                    battle.GameUI.wait = true;
                    battle.GameUI.sfx.PlayDefendSound(active);
                }
                else
                {
                    battle.GameUI.wait = true;
                    battle.GameUI.sfx.PlayPassSound(active);
                }
            }

            



        }
        //get all adjacent tiles, for splash damage purposes
        public static List<Tile> GetAllAdjacentTiles(Map map, Tile currentTile)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            int x = currentTile.X;
            int y = currentTile.Y;

            if (x - 1 >= 0)
            {
                Tile left = map.map[x - 1][y];
                    adjacentTiles.Add(left);
            }

            if (x + 1 < map.Size[0])
            {
                Tile right = map.map[x + 1][y];
                    adjacentTiles.Add(right);

            }

            if (y - 1 >= 0)
            {
                Tile up = map.map[x][y - 1];
                    adjacentTiles.Add(up);

            }

            if (y + 1 < map.Size[1])
            {
                Tile down = map.map[x][y + 1];
                    adjacentTiles.Add(down);
            }

            return adjacentTiles;
        }

        public static List<Tile> GetAdjacentEnemyTiles(Unit ally, Map map)
        {
            List<Tile> adjacent = GetAllAdjacentTiles(map, map.GetTileAt(ally.Location[0], ally.Location[1]));
            List<Tile> enemyTiles = new List<Tile>();

            foreach (Tile adj in adjacent)
            {
                if (adj.hasUnit)
                {
                    if (adj.TileUnit.isPlayerUnit != ally.isPlayerUnit)
                    {
                        enemyTiles.Add(adj);
                    }
                }

            }

            return enemyTiles;
        }

        public static void MakeTauntedMove(Battle battle, Unit active, Unit target)
        {

            Map map = battle.BattleMap;
            Tile location = map.GetTileAt(active.Location[0], active.Location[1]);


            int range = active.Range;
            int x = active.Location[0];
            int y = active.Location[1];
            List<int[]> rangeSquare = new List<int[]>();

            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < map.Size[0] && (j + y) < map.Size[1] && (i + x) >= 0 & (j + y) >= 0)
                    {
                        rangeSquare.Add(new int[] { i + x, j + y });
                    }
                }
            }
            List<Tile> actualRange = new List<Tile>();
            foreach (int[] tile in rangeSquare)
            {
                if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                {
                    actualRange.Add(map.GetTileAt(tile[0], tile[1]));
                }
            }

            bool targetInRange = false;

            foreach (Tile tile in actualRange)
            {
                if (tile.hasUnit)
                {
                    if (tile.TileUnit == target)
                    {
                        targetInRange = true;
                    }
                }
            }

            if (targetInRange)
            {
                battle.GameUI.unitDamage = battle.Attack(target);
                battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
                battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;
                battle.GameUI.displayDamage = true;
            }
            else //calculate the closest tile to the target that is reachable
            {
                Tile activeLocation = map.GetTileAt(active.Location[0], active.Location[1]);
                Tile targetLocation = map.GetTileAt(target.Location[0], target.Location[1]);

                List<int[]> reachableTilesAsInt = map.GetLegalMoveCoordinates(active);
                List<Tile> reachableTiles = new List<Tile>();

                foreach (int[] coords in reachableTilesAsInt)
                {
                    reachableTiles.Add(map.GetTileAt(coords[0], coords[1]));
                }

                List<Tile> enemyAdjacent = GetAdjacentLegalTiles(map, targetLocation);
                int closestPathCost = 500000;
                Tile closest = new Tile();

                foreach (Tile adj in enemyAdjacent)
                {
                    
                    List<Tile> path = GetPath(activeLocation, adj, map);
                    int pathCost = 0;
                    foreach (Tile tile in path)
                    {
                        pathCost += tile.MoveCost;
                    }
                    if (pathCost < closestPathCost)
                    {
                        closest = adj;
                        closestPathCost = pathCost;
                    }
                }
                Tile destination = closest;
                List<Tile> destinationPath = GetPath(activeLocation, destination, map);
                if (Reachable(destinationPath, active.Speed) && destination != activeLocation)
                {
                    battle.StartMove(destination);
                }
                else if (destination != activeLocation)
                {
                    Tile closestReachableTile = new Tile();
                    int closestReachableTileCost = 10000;
                    foreach (Tile test in reachableTiles)
                    {
                        List<Tile> path = GetPath(test, destination, map);
                        int pathCost = 0;
                        foreach (Tile tile in path)
                        {
                            pathCost += tile.MoveCost;
                        }
                        if (pathCost < closestReachableTileCost)
                        {
                            closestReachableTile = test;
                            closestReachableTileCost = pathCost;
                        }
                    }
                    if (PathExists(location, closestReachableTile, map, actualRange, active.Speed))
                    {
                        battle.StartMove(closestReachableTile);
                    }
                    else
                    {
                        active.AP = 0;
                        battle.GameUI.sfx.PlayPassSound(active);
                    }
                }
                else
                {
                    battle.GameUI.sfx.PlayPassSound(active);
                    active.AP = 0;
                }
            }

            if (active.AP <= 0)
            {
                battle.GameUI.taunted = false;
            }



            
        }

        //determine if a unit already has a particular status effect
        public static bool HasStatusEffect(Unit unit, string effect)
        {
            switch(effect)
            {
                case "Stun":
                    {
                        foreach (StatusEffect fx in unit.StatusEffects)
                        {
                            if (fx is Stun)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case "Root":
                    {
                        foreach (StatusEffect fx in unit.StatusEffects)
                        {
                            if (fx is Root)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case "Taunt":
                    {
                        foreach (StatusEffect fx in unit.StatusEffects)
                        {
                            if (fx is Taunt)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case "Haste":
                    {
                        foreach (StatusEffect fx in unit.StatusEffects)
                        {
                            if (fx is Haste)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case "Slow":
                    {
                        foreach (StatusEffect fx in unit.StatusEffects)
                        {
                            if (fx is Slow)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                
            }
            return false;
        }

        public static bool HasEnemyUnit(Tile tile, Unit active)
        {
            if (tile.hasUnit)
            {
                if (tile.TileUnit.isPlayerUnit != active.isPlayerUnit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
     
    }


}
