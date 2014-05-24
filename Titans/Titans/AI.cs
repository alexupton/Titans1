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
                    if (rangeSet.Contains(adj))
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

        public static void MakeAIMove(Battle battle, Unit active)
        {
            if (!battle.GameUI.taunted && active.AP > 0)
            {

                if (active is Soldier)
                {

                    if (MakeSoldierMove(battle, active))
                    {
                        return;
                    }
                }
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
                        battle.GameUI.moveWait = true;
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
        
        //method better suited for splash damage
        public static List<Tile> GetSplashDamageTiles(Unit target, Map map)
        {
            List<Tile> adjacent = GetAllAdjacentTiles(map, map.GetTileAt(target.Location[0], target.Location[1]));
            List<Tile> enemyTiles = new List<Tile>();

            foreach (Tile adj in adjacent)
            {
                if (adj.hasUnit)
                {
                    if (adj.TileUnit.isPlayerUnit == target.isPlayerUnit)
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

        //determines whether a unit is the range of any enemy
        public static bool IsInEnemyRange(Battle battle, Unit active)
        {
            bool inRange = false;
            List<Tile> cumulativeEnemyRange = new List<Tile>();
            foreach (Unit unit in battle.BattleQueue)
            {
                if (unit.isPlayerUnit != active.isPlayerUnit)
                {
                    cumulativeEnemyRange.AddRange(battle.BattleMap.GetAttackRangeTiles(unit));
                }
            }

            foreach (Tile test in cumulativeEnemyRange)
            {
                if (test.hasUnit)
                {
                    if (test.TileUnit == active)
                    {
                        inRange = true;
                    }
                }
            }
            return inRange;
        }
        //same as above, only tests to see if a tile is safely out of range of enemies
        public static bool IsInEnemyRange(Battle battle, Unit active, Tile test)
        {
            bool inRange = false;
            List<Tile> cumulativeEnemyRange = new List<Tile>();
            foreach (Unit unit in battle.BattleQueue)
            {
                if (unit.isPlayerUnit != active.isPlayerUnit)
                {
                    cumulativeEnemyRange.AddRange(battle.BattleMap.GetAttackRangeTiles(unit));
                }
            }

            foreach (Tile tile in cumulativeEnemyRange)
            {
                if (tile == test)
                {
                    inRange = true;
                }
            }
            return inRange;
        }
        //helper method to get cumulative range from all enemies
        public static List<Tile> GetCumulativeEnemyRange(Battle battle, Unit active)
        {
            List<Tile> cumulativeEnemyRange = new List<Tile>();
            foreach (Unit unit in battle.BattleQueue)
            {
                if (unit.isPlayerUnit != active.isPlayerUnit)
                {
                    cumulativeEnemyRange.AddRange(battle.BattleMap.GetAttackRangeTiles(unit));
                }
            }
            return cumulativeEnemyRange;
        }

        //get all enemy units in attack range
        public static List<Unit> GetEnemiesInRange(Battle battle, Unit active)
        {
            List<Tile> rangeTiles = battle.BattleMap.GetAttackRangeTiles(active);
            List<Unit> enemies = new List<Unit>();
            foreach (Tile tile in rangeTiles)
            {
                if (HasEnemyUnit(tile, active))
                {
                    enemies.Add(tile.TileUnit);
                }
            }
            return enemies;
        }


        //AI runs away, preferably to a defender
        public static void AIFlee(Battle battle, Unit active)
        {
            List<Tile> enemyRange = GetCumulativeEnemyRange(battle, active);
            List<Unit> defenders = new List<Unit>();
            foreach (Unit unit in battle.BattleQueue)
            {
                if (unit is Defender && unit.isPlayerUnit == active.isPlayerUnit)
                {
                    defenders.Add(unit);
                }
            }

            List<Tile> reachableTiles = battle.BattleMap.GetLegalMoveTiles(active);
            List<Tile> safeTiles = reachableTiles;

            foreach (Tile eTile in enemyRange)
            {
                if (safeTiles.Contains(eTile))
                {
                    safeTiles.Remove(eTile);
                }
            }
            //if there are safe places to move, pick one close to defenders
            if (safeTiles.Count > 0)
            {
                Tile destination = new Tile();
                int maxDefenderCount = 0;
                foreach (Tile safe in safeTiles)
                {
                    List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap, safe);
                    int defenderCount = 0;

                    foreach (Tile adj in adjacent)
                    {
                        if (adj.hasUnit)
                        {
                            if (defenders.Contains(adj.TileUnit))
                            {
                                defenderCount++;
                            }
                        }
                    }

                    if (defenderCount > maxDefenderCount)
                    {
                        maxDefenderCount = defenderCount;
                        destination = safe;
                    }

                }

                if (maxDefenderCount > 0)
                {
                    battle.StartMove(destination);
                    return;
                }
                else
                {
                    Random rand = new Random();
                    int next = rand.Next(safeTiles.Count);
                    destination = safeTiles.ElementAt(next);

                    battle.StartMove(destination);
                    return;
                }
            }
            else
            {
                Tile destination = new Tile();
                int maxDefenderCount = 0;
                foreach (Tile tile in reachableTiles)
                {
                    List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap, tile);
                    int defenderCount = 0;

                    foreach (Tile adj in adjacent)
                    {
                        if (adj.hasUnit)
                        {
                            if (defenders.Contains(adj.TileUnit))
                            {
                                defenderCount++;
                            }
                        }
                    }

                    if (defenderCount > maxDefenderCount)
                    {
                        maxDefenderCount = defenderCount;
                        destination = tile;
                    }

                }

                if (maxDefenderCount > 0)
                {
                    battle.StartMove(destination);
                    return;
                }
                else
                {
                    //run in the exact opposite direction of nearest enemy
                    Unit nearestEnemy = GetClosestEnemyUnit(battle, active);

                    if (nearestEnemy != null)
                    {
                        Tile enemyTile = battle.BattleMap.GetTileAt(nearestEnemy.Location[0], nearestEnemy.Location[1]);
                        Tile activeTile = battle.BattleMap.GetTileAt(active.Location[0], active.Location[1]);
                        Tile escapeTile = reachableTiles.ElementAt(0);
                        if (enemyTile.X < activeTile.X)
                        {
                            escapeTile = GetFurthestTile(battle, reachableTiles, 1);
                        }
                        else if (enemyTile.X > activeTile.X)
                        {
                            escapeTile = GetFurthestTile(battle, reachableTiles, 0);
                        }
                        else if (enemyTile.Y < activeTile.Y)
                        {
                            escapeTile = GetFurthestTile(battle, reachableTiles, 3);
                        }
                        else if (enemyTile.Y > activeTile.Y)
                        {
                            escapeTile = GetFurthestTile(battle, reachableTiles, 2);
                        }

                        battle.StartMove(escapeTile);
                        return;
                    }
                }
            }
        }

        //get furthest reachable tile in a certain direction
        public static Tile GetFurthestTile(Battle battle, List<Tile> moveRange, int direction)
        {
            Tile moveTile = moveRange.ElementAt(0);
            switch (direction)
            {
                case 0:
                    {
                        foreach (Tile tile in moveRange)
                        {
                            if (tile.X < moveTile.X)
                            {
                                moveTile = tile;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        foreach (Tile tile in moveRange)
                        {
                            if (tile.X > moveTile.X)
                            {
                                moveTile = tile;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (Tile tile in moveRange)
                        {
                            if (tile.Y < moveTile.Y)
                            {
                                moveTile = tile;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (Tile tile in moveRange)
                        {
                            if (tile.Y > moveTile.Y)
                            {
                                moveTile = tile;
                            }
                        }
                        break;
                    }
            }

            return moveTile;
        }
        //returns the unit closest to a given unit that is also an enemy
        public static Unit GetClosestEnemyUnit(Battle battle, Unit active)
        {
            Map map = battle.BattleMap;
            Tile location = map.GetTileAt(active.Location[0], active.Location[1]);
            List<Unit> enemies = new List<Unit>();
            foreach (Unit unit in battle.BattleQueue)
            {
                if (unit.isPlayerUnit != unit.isPlayerUnit)
                {
                    enemies.Add(unit);
                }
            }
            int nearestEnemyDistance = 500000;
            Unit nearestEnemy = new Soldier();
            Tile nearestLocation = new Tile();
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

            if (nearestEnemyDistance != 500000)
            {
                return nearestEnemy;
            }
            else
            {
                return null;
            }
        }



        
        //see how much damage will be done if every enemy in range attacks the active unit twice
        public static int GetEstimatedCumulativeDamage(Battle battle, Unit active, List<Unit> enemies)
        {
            Tile activeTile = battle.BattleMap.GetTileAt(active.Location[0], active.Location[1]);
            List<Unit> attackingEnemies = new List<Unit>();
            foreach (Unit enemy in enemies)
            {
                List<Tile> enemyRange = battle.BattleMap.GetAttackRangeTiles(enemy);
                foreach (Tile test in enemyRange)
                {
                    if (test == activeTile && !attackingEnemies.Contains(enemy))
                    {
                        attackingEnemies.Add(enemy);
                    }
                }
            }

            int potentialDamage = 0;

            foreach (Unit enemy in attackingEnemies)
            {
                potentialDamage += AttackResolver.Attack(enemy, active, enemy.AttackModifiers) * 2;
            }
            return potentialDamage;
        }

        

        //make AI move for soldiers
        public static bool MakeSoldierMove(Battle battle, Unit active)
        {
            Tile activeTile = battle.BattleMap.GetTileAt(active.Location[0], active.Location[1]);
            Random rand = new Random();
            int chance = rand.Next(100);

            //use First Aid on self to heal and remove status effects if any
            if ((active.HP < active.MaxHP || StatusEffect.HasNegativeStatusEffects(active.StatusEffects)) && active.MP >= 10 && chance < 85)
            {
                active.Special3(battle);
            }

            List<Unit> enemies = GetEnemiesInRange(battle, active);

            //Can I kill an enemy on this turn?
            foreach (Unit enemy in enemies)
            {
                int theoreticalDamage = AttackResolver.Attack(active, enemy, active.AttackModifiers);

                if (theoreticalDamage >= enemy.HP)
                {
                    battle.Attack(enemy); return true;
                }
                else if (theoreticalDamage * 2 >= enemy.HP && active.AP >= 2)
                {
                    battle.Attack(enemy); return true;
                }
                else if (theoreticalDamage * 3 >= enemy.HP && active.AP >= 2 && active.MP >= 20)
                {
                    battle.CurrentTarget = enemy;
                    active.Special2(battle); return true;
                }
                else if (theoreticalDamage * 1.5 >= enemy.HP)
                {
                    battle.CurrentTarget = enemy;
                    active.Special2(battle); return true;
                }
            }


            //can my enemies kill me?
            int potentialDamage = GetEstimatedCumulativeDamage(battle, active, enemies);
            if (potentialDamage >= active.HP && chance < 70)
            {
                AIFlee(battle, active);
                return true;
            }
                //even if they can't kill me, am I weak enough that i should defend myself?
            else if (potentialDamage < active.HP && active.HP <= active.MaxHP / 2 && active.AP <= 1)
            {
                battle.SelectDefend();
                return true;
            }



            


            return false;
        }
     
    }


}
