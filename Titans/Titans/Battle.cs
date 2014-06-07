using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Titans
{
    public class Battle
    {
        public List<Unit> Units { get; set; } //unit roster
        public int Points { get; set; } //total points available for unit purchase
        public int TurnLimit { get; set; } //set this to -1 if there is no turn limit
        public Map BattleMap { get; set; }
        public List<Unit> BattleQueue { get; set; }
        public Unit ActiveUnit { get; set; }
        public int QueuePosition { get; set; }
        public int TurnCount { get; set; }
        public bool MoveMode { get; set; }
        public bool AttackMode { get; set; }
        public bool MoveEnabled { get; set; }
        public bool AttackEnabled { get; set; }
        public Game1 GameUI { get; set; }
        public Tile[] pendingMoves { get; set; }
        int pendingIndex { get; set; }
        public bool SelectEnabled { get; set; }
        public bool gameOver { get; set; }
        public bool AIControlled { get; set; }
        public bool AttackRangeDisplayed { get; set; }
        public bool MoveRangeDisplayed { get; set; }
        public bool specialMode { get; set; }
        public bool specialMode1 { get; set; }
        public bool specialMode2 { get; set; }
        public bool specialMode3 { get; set; }
        public bool specialMode4 { get; set; }
        public bool specialMode5 { get; set; }
        public bool specialMode6 { get; set; }
        public Unit CurrentTarget { get; set; }
        public Tile SelectedTile { get; set; }
        public List<Ranger> rangersWithTargets { get; set; }
        public List<Unit> rangerTarget { get; set; }

        //any more custom rule options go here

        public Battle()
        {
            Units = new List<Unit>();
            Points = 1000; //suggested amount, can be changed
            TurnCount = 1;
            MoveMode = false;
            AttackMode = false;
            MoveEnabled = true;
            AttackEnabled = false;
            SelectEnabled = true;
            AttackRangeDisplayed = false;
            MoveRangeDisplayed = false;
            specialMode = false;
            specialMode1 = false;
            specialMode2 = false;
            specialMode3 = false;
            specialMode4 = false;
            specialMode5 = false;
            specialMode6 = false;
            rangersWithTargets = new List<Ranger>();
            rangerTarget = new List<Unit>();
        }

        public Battle(Map newMap)
        {
            BattleMap = newMap;
            Units = new List<Unit>();
            TurnCount = 1;
            MoveMode = false;
            AttackMode = false;
            MoveEnabled = true;
            AttackEnabled = false;
            for (int x = 0; x < newMap.Size[0]; x++)
            {
                for (int y = 0; y < newMap.Size[1]; y++)
                {
                    if (newMap.map[x][y].hasUnit)
                    {
                        Units.Add(newMap.map[x][y].TileUnit);
                    }
                }
            }
            rangersWithTargets = new List<Ranger>();
            rangerTarget = new List<Unit>();
        }

        //Add units to the pregame roster
        public void AddUnit(Unit unit)
        {
            if (Points - unit.Price >= 0)
            {
                Units.Add(unit);
                Points -= unit.Price;
            }
        }
        //Remove units FROM THE PREGAME ROSTER
        public void DeleteUnit(Unit unit)
        {
                Units.Remove(unit);
                Points += unit.Price;
                if (Points > 1000)
                    Points = 100;
        }

        //Place a unit on the map
        //Units are drawn from the roster, addressed by index
        public void PlaceUnit(int rosterIndex, int X, int Y)
        {
            BattleMap.map[X][Y].TileUnit = Units.ElementAt(rosterIndex);
            BattleMap.map[X][Y].hasUnit = true;
            Units.ElementAt(rosterIndex).Location[0] = X; //update unit coordinates
            Units.ElementAt(rosterIndex).Location[1] = Y;
        }

        //overload method for placing arbitrary unit on arbitrary tile
        //suck it, old method
        public void PlaceUnit(Unit unit, Tile location)
        {
            location.TileUnit = unit;
            location.hasUnit = true;
            location.IsImpassible = true;
            unit.Location[0] = location.X;
            unit.Location[1] = location.Y;

        }

        //Remove unit FROM THE MAP at the specified coodinates
        public void RemoveUnit(int X, int Y)
        {
            BattleMap.map[X][Y].TileUnit = null;
            BattleMap.map[X][Y].hasUnit = false;
            BattleMap.map[X][Y].IsImpassible = false;
        }

        public List<Unit> RollInitiative()
        {
            //if it's not the first turn, remove buffs from the current unit
            if (TurnCount > 1)
            {
                foreach (StatusEffect effect in ActiveUnit.StatusEffects)
                {
                    effect.UnInvoke(this);
                }

            }
            //release the wait hold, other cleanup
            if (GameUI != null)
            {
                GameUI.wait = false;
                GameUI.ResetButtons();
                GameUI.moveWait = false;
                GameUI.tickWait = false;
                GameUI.AILock = false;
            }
            SelectEnabled = true;

            //undo effects of charge
            if (ActiveUnit is Spearman)
            {
                Spearman dummy = (Spearman)ActiveUnit;
                if (dummy.isCharged)
                {
                    dummy.Speed -= 50;
                    dummy.isCharged = false;
                    ActiveUnit = dummy;
                }

            }



            QueuePosition = 0;
            List<Unit> turnOrder = new List<Unit>();
            Random rand = new Random();

            List<Unit> player1Units = new List<Unit>();
            List<Unit> player2Units = new List<Unit>();


                foreach (Unit unit in Units)
                {
                    if (TurnCount == 1)
                    {
                        unit.StatusEffects = new List<StatusEffect>();
                    }
                    unit.Init = unit.Speed + rand.Next(51);
                    unit.AP = 2;
                    if (unit.isPlayerUnit)
                    {

                        player1Units.Add(unit);
                    }
                    else
                    {
                        player2Units.Add(unit);
                    }
                }

                player1Units = player1Units.OrderByDescending(u => u.Init).ToList();
                player2Units = player2Units.OrderByDescending(u => u.Init).ToList();

                turnOrder.AddRange(player1Units);
                turnOrder.AddRange(player2Units);

            
            //    foreach (Unit unit in Units)
            //    {
            //        unit.Init = unit.Speed + rand.Next(51); //add a random roll to the speed to get initiative
            //        unit.AP = 2;
            //    }
            //    turnOrder = Units.OrderByDescending(u => u.Init).ToList(); //using LINQ to order by initiative value

            BattleQueue = turnOrder; //save the turn order
            ActiveUnit = BattleQueue.ElementAt(0); //set the ActiveUnit to the first in the turn order

            //clear defense buffs
            int defMod = 0;
            if (ActiveUnit.DefenseModifiers.Count > 0)
            {
                ActiveUnit.DefendMode = false;
                foreach (int mod in ActiveUnit.DefenseModifiers)
                {
                    defMod += mod;
                }
                ActiveUnit.DefenseModifiers.Clear();
            }
            
            ActiveUnit.Defense -= defMod;
            defMod = 0;
            //add passive defense from defenders back in
            List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
            foreach(Tile adj in adjacent)
            {
                if(adj.hasUnit)
                {
                    if(adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                    {
                        defMod += 5;
                        ActiveUnit.DefenseModifiers.Add(5);
                    }
                }
            }
            //rangers can't defend
            if (ActiveUnit is Ranger)
            {
                GameUI.defend = GameUI.defend_grey;
            }

            //regen unit MP
            foreach (Unit unit in BattleQueue)
            {
                if (unit.MP < unit.MaxMP)
                {
                    if (unit is Mage)
                    {
                        unit.MP += 15;
                        
                    }
                    else
                    {
                        unit.MP += (int)Math.Round((double)unit.MaxMP * (10.0 / 100.0));
                    }
                    //can't have more MP than max
                    if (unit.MP > unit.MaxMP)
                    {
                        unit.MP = unit.MaxMP;
                    }
                }
            }

            ActiveUnit.Defense += defMod;

            //apply status effects on the active unit
            
            for (int i = 0; i < ActiveUnit.StatusEffects.Count; i++ )
            {
                ActiveUnit.StatusEffects.ElementAt(i).DecreaseTime();
                if (ActiveUnit.StatusEffects.ElementAt(i).GetTimeRemaining() <= 0)
                {
                    ActiveUnit.StatusEffects.Remove(ActiveUnit.StatusEffects.ElementAt(i));
                }
                else
                {
                    ActiveUnit.StatusEffects.ElementAt(i).Invoke(this);
                }
            }
            return turnOrder;
        }
        
        //after a unit moves, call this method to access the next unit in the battle queue
        public Unit NextPlayer()
        {
                foreach (StatusEffect effect in ActiveUnit.StatusEffects)
                {
                    effect.UnInvoke(this);
                }

            //undo effects of charge
                if (ActiveUnit is Spearman)
                {
                    Spearman dummy = (Spearman)ActiveUnit;
                    if (dummy.isCharged)
                    {
                        dummy.Speed -= 50;
                        dummy.isCharged = false;
                        ActiveUnit = dummy;
                    }
                    
                }
            GameUI.wait = false;
            GameUI.ResetButtons();
            MoveMode = false;
            AttackMode = false;
            GameUI.moveWait = false;
            GameUI.tickWait = false;
            if (QueuePosition < BattleQueue.Count - 1)
            {
                QueuePosition++;
                ActiveUnit = BattleQueue.ElementAt(QueuePosition);
                if (!ActiveUnit.isPlayerUnit && AIControlled)
                {
                    GameUI.AILock = true;
                    GameUI.LockButtons();
                }
                
            }
            else
            {
                QueuePosition = 0;
                TurnCount++;
                RollInitiative();
                GameUI.AILock = false;
                GameUI.ResetButtons();
            }

            DeselectAttack();

            GameUI.SetOffsetValue(ActiveUnit.Location[0] * -55 + 750, ActiveUnit.Location[1] * -55 + 400);
            GameUI.sfx.PlaySelectSound(ActiveUnit);

            if (ActiveUnit is Ranger)
            {
                GameUI.defend = GameUI.defend_grey;
            }


            //clear defense buffs
            int defMod = 0;
            if (ActiveUnit.DefenseModifiers.Count > 0)
            {
                ActiveUnit.DefendMode = false;
                foreach (int mod in ActiveUnit.DefenseModifiers)
                {
                    defMod += mod;
                }
                ActiveUnit.DefenseModifiers.Clear();
            }


            ActiveUnit.Defense -= defMod;
            defMod = 0;
            //add passive defense from defenders back in
            List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
            foreach (Tile adj in adjacent)
            {
                if (adj.hasUnit)
                {
                    if (adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                    {
                        defMod += 5;
                        ActiveUnit.DefenseModifiers.Add(5);
                    }
                }
            }
            ActiveUnit.Defense += defMod;

            //apply status effects on the active unit
            //apply status effects on the active unit
            for (int i = 0; i < ActiveUnit.StatusEffects.Count; i++)
            {
                ActiveUnit.StatusEffects.ElementAt(i).DecreaseTime();
                if (ActiveUnit.StatusEffects.ElementAt(i).GetTimeRemaining() <= 0)
                {
                    ActiveUnit.StatusEffects.Remove(ActiveUnit.StatusEffects.ElementAt(i));
                }
                else
                {
                    ActiveUnit.StatusEffects.ElementAt(i).Invoke(this);
                }
            }

            return ActiveUnit;
        }

        //When a move action is selected, this is the first method called
        //This method selects legal moves for the active unit to make
        public void SelectMove()
        {
            SelectEnabled = false;
            BattleMap.ClearRedHighlights();
            List<int[]> moveTiles = BattleMap.GetLegalMoveCoordinates(ActiveUnit);
            BattleMap.RedHighlightTiles(moveTiles);
            MoveMode = true;
        }

        public void DeselectMove()
        {
            SelectEnabled = true;
            BattleMap.ClearRedHighlights();
            BattleMap.ClearHighlights();
            MoveMode = false;
        }
        //generate a list of moves between the unit and desired tile, for animation purposes
        public void StartMove(Tile move)
        {
            //remove defender buffs from nearby units
            if (ActiveUnit is Defender)
            {
                List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
                foreach (Tile adj in adjacent)
                {
                    if (adj.hasUnit)
                    {
                        if (adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                        {
                            adj.TileUnit.DefenseModifiers.Remove(5);
                        }
                    }
                }
            }
            else //look for nearby defenders and remove buffs if they are found
            {
                List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
                foreach (Tile adj in adjacent)
                {
                    if (adj.hasUnit)
                    {
                        if (adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                        {
                            ActiveUnit.Defense -= 5;
                            ActiveUnit.DefenseModifiers.Remove(5);
                        }
                    }
                }
            }
            GameUI.moveWait = true;
            List<Tile> movePath = AI.GetPath(BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]), move, BattleMap, ActiveUnit);
            pendingMoves = movePath.ToArray();
            pendingIndex = pendingMoves.Length - 1;
            GameUI.sfx.PlayMoveSound(ActiveUnit);
        }
        //advance to the next tile in the move list
        //if at the end, the move is finished
        public void ContinueMove()
        {
            
                BattleMap.Move(ActiveUnit, pendingMoves[pendingIndex].X, pendingMoves[pendingIndex].Y);
                GameUI.SetOffsetValue(ActiveUnit.Location[0] * -55 + 750, ActiveUnit.Location[1] * -55 + 400);
                pendingIndex--;
                if (pendingIndex == -1)
                {
                    GameUI.moveWait = false;
                    ActiveUnit.AP--;
                    BattleMap.ClearHighlights();
                    MoveMode = false;
                    pendingIndex = 0;
                    pendingMoves = new Tile[0];
                    if (ActiveUnit is Defender) //if the unit is a defender, add its passive defense to nearby units
                    {
                        List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
                        foreach (Tile adj in adjacent)
                        {
                            if (adj.hasUnit)
                            {
                                if (adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                                {
                                    adj.TileUnit.Defense += 5;
                                    adj.TileUnit.DefenseModifiers.Add(5);
                                }
                            }
                        }
                    }
                    //add nearby defender bonuses to your own
                        List<Tile> adja = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
                        foreach (Tile adj in adja)
                        {
                            if (adj.hasUnit)
                            {
                                if (adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == ActiveUnit.isPlayerUnit)
                                {
                                    ActiveUnit.Defense += 5;
                                    ActiveUnit.DefenseModifiers.Add(5);
                                }
                            }
                        }
                }
        }

        public void SelectDefend()
        {
            ActiveUnit.DefendMode = true;
            MoveMode = false;
            AttackMode = false;
            ActiveUnit.DefenseModifiers.Add(5);
            ActiveUnit.Defense += 5;
            ActiveUnit.AP = 0;
            GameUI.sfx.PlayDefendSound(ActiveUnit);
        }
                
                
            


            
        

        public bool SelectAttack()
        {
            //artillery use 2 AP to attack
            if (ActiveUnit is Artillery && ActiveUnit.AP - 2 < 0)
            {
                BattleMap.ClearHighlights();
                AttackMode = false;
                return false;
            }
            SelectEnabled = false;
            int range = ActiveUnit.Range;
            int x = ActiveUnit.Location[0];
            int y = ActiveUnit.Location[1];
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();

            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < BattleMap.Size[0] && (j + y) < BattleMap.Size[1] &&(i +x) >= 0 & (j+y) >= 0)
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
            


           
            bool validTargetExists = false; //we'll use this to check for valid targets
            //now we check the tiles in reach for units and highlight them if they exist
            foreach (int[] coords in actualRange)
            {
                BattleMap.AddSpecificBlueHighlight(coords[0], coords[1]);
                if (BattleMap.map[coords[0]][coords[1]].hasUnit)
                {
                    if (BattleMap.map[coords[0]][coords[1]].TileUnit.isPlayerUnit != ActiveUnit.isPlayerUnit)
                    {
                        BattleMap.AddSpecificRedHighlight(coords[0], coords[1]);
                        validTargetExists = true;
                    }
                }
            }

            if (ActiveUnit is Ranger)
            {
                Ranger ranger = (Ranger)ActiveUnit;
                if (ranger.specialTarget != null)
                {
                    BattleMap.AddSpecificRedHighlight(ranger.specialTarget.Location[0], ranger.specialTarget.Location[1]);
                }
            }
                AttackMode = true;
            return validTargetExists;

            



        }

        public void DeselectAttack()
        {
            SelectEnabled = true;
            AttackMode = false;
            BattleMap.ClearHighlights();
            BattleMap.ClearRedHighlights();
        }

        //carry out the attack, once the target is selected
        public int Attack(Unit target)
        {
            //prevents AI artillery from cheating
            if (ActiveUnit is Artillery && GameUI.AILock && ActiveUnit.AP - 2 < 0)
            {
                ActiveUnit.AP = 0;
                return 0;
            }

            //ranger gets to attack his special target once
            if (ActiveUnit is Ranger)
            {
                Ranger dummy = (Ranger)ActiveUnit;
                dummy.specialTarget = null;
                ActiveUnit = dummy;
            }
            SelectEnabled = true;
            BattleMap.ClearHighlights();

            

            int storedDefense = target.Defense; //store the defender's defense temporarily

            //foreach (int mod in target.DefenseModifiers)
            //{
            //    target.Defense += mod; //temporarily add the defense modifiers to the base defense of defender
            //}

            //defenders get a bonus to defense against flying units
            if ((ActiveUnit is Scout || ActiveUnit is Fighter || ActiveUnit is Bomber) && target is Defender)
            {
                target.Defense += 10;
                target.DefenseModifiers.Add(10);
            }
            //Soldiers get a flanking bonus to attack
            if (ActiveUnit is Soldier)
            {
                List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, BattleMap.GetTileAt(target.Location[1], target.Location[1]));
                bool flanked = false;
                foreach (Tile adj in adjacent)
                {
                    if (adj.hasUnit && !flanked)
                    {
                        if (adj.TileUnit.isPlayerUnit != target.isPlayerUnit && adj.TileUnit != ActiveUnit)
                        {
                            flanked = true;
                            ActiveUnit.AttackModifiers.Add(5);
                        }
                    }
                }
            }
            List<int> combatMods = ActiveUnit.AttackModifiers;


            int damage = AttackResolver.Attack(ActiveUnit, target, combatMods); //calculate the attack

            //Rangers get a 10% attack bonus against scouts
            if (ActiveUnit is Ranger && target is Scout)
            {
                damage = (int)(Math.Round(damage + damage * (10.0 / 100)));
            }

            //remove defender bonus vs air
            if ((ActiveUnit is Scout || ActiveUnit is Fighter || ActiveUnit is Bomber) && target is Defender)
            {
                target.DefenseModifiers.Remove(10);
            }


            GameUI.displayDamage = true;

            target.Defense = storedDefense; //restore defender base defense
            target.HP -= damage;

            

            
            //check if the unit is dead. if it is, turf it
            if (target.HP <= 0)
            {
                //if the unit is a ranger's special target, that status must be removed
                Ranger test = new Ranger();
                for (int i = 0; i < BattleQueue.Count; i++)
                {
                    Unit unit = BattleQueue.ElementAt(i);
                    if (unit is Ranger)
                    {
                        test = (Ranger)unit;
                        if (test.specialTarget == target)
                        {
                            test.specialTarget = null;
                            unit = test;
                        }
                    }
                }
                
                GameUI.sfx.PlayDieSound(target);
                //This is where a death animation would go IF WE HAD ONE
                RemoveUnit(target.Location[0], target.Location[1]);
                Units.Remove(target);
                BattleQueue.Remove(target);

                //Soldiers get a chance to attack again
                if (ActiveUnit is Soldier)
                {
                    Random rand = new Random();
                    int cleaveChance = rand.Next(100);
                    if (cleaveChance < 15)
                    {
                        ActiveUnit.AP++;
                    }
                }

                bool player1HasUnits = false;
                bool player2HasUnits = false;

                foreach (Unit unit in BattleQueue)
                {
                    if (unit.isPlayerUnit)
                    {
                        player1HasUnits = true;
                    }
                    else
                    {
                        player2HasUnits = true;
                    }
                }



                if (player1HasUnits && !player2HasUnits)
                {
                    gameOver = true;
                    GameUI.endWait = true;
                    GameUI.p1win = true;
                    GameUI.PlayWinMusic();
                    return damage;
                }
                if (player2HasUnits && !player1HasUnits)
                {
                    GameUI.endWait = true;
                    gameOver = true;
                    GameUI.p2win = true;
                    GameUI.PlayLoseMusic();
                    return damage;
                }
            }
            else
            {
                GameUI.sfx.PlayAttackSound(ActiveUnit);

                if(target is Soldier &&target.DefendMode && !(ActiveUnit is Ranger))
                {
                    Random rand = new Random();

                    int counterRoll = rand.Next(100);
                    if (counterRoll < 25) //25% chance for soldier to counter
                    {
                        List<int> attackModifiers = target.AttackModifiers;
                        int counterDamage = AttackResolver.Attack(target, ActiveUnit, attackModifiers);
                        ActiveUnit.HP -= counterDamage;
                        GameUI.splashDamage.Clear();
                        GameUI.splashDamage.Add(counterDamage);
                        GameUI.splashLocations.Clear();
                        GameUI.splashLocations.Add(BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]));
                    }
                }
            }
            ActiveUnit.AttackModifiers.Clear();
            AttackMode = false;
            if (ActiveUnit is Artillery)
            {
                ActiveUnit.AP -= 2;
            }
            else
            {
                ActiveUnit.AP--;
            }

            

            return damage;

        }

        //make an AI move
        public void AIMove()
        {
            GameUI.SetOffsetValue(ActiveUnit.Location[0] * -55 + 750, ActiveUnit.Location[1] * -55 + 400);
                AI.MakeAIMove(this, ActiveUnit);
                GameUI.wait = true;
                GameUI.timeSinceLastDamageFrame = 0;
        }

        public void ShowAttackRange(Unit selected)
        {
            AttackRangeDisplayed = true;
            int range = selected.Range;
            int x = selected.Location[0];
            int y = selected.Location[1];
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();

            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < BattleMap.Size[0] && (j + y) < BattleMap.Size[1] && (i + x) >= 0 & (j + y) >= 0)
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

            foreach (int[] tile in actualRange)
            {
                BattleMap.AddSpecificBlueHighlight(tile[0], tile[1]);
            }
        }

        public void ShowSpecificAttackRange(Unit selected, int Range)
        {
            AttackRangeDisplayed = true;
            int range = Range;
            int x = selected.Location[0];
            int y = selected.Location[1];
            List<int[]> rangeSquare = new List<int[]>();
            List<int[]> actualRange = new List<int[]>();

            for (int i = (range * -1); i <= range; i++)
            {
                for (int j = (range * -1); j <= range; j++)
                {
                    if ((i + x) < BattleMap.Size[0] && (j + y) < BattleMap.Size[1] && (i + x) >= 0 & (j + y) >= 0)
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

            foreach (int[] tile in actualRange)
            {
                BattleMap.AddSpecificBlueHighlight(tile[0], tile[1]);
            }
        }

        public void ShowMoveRange(Unit selected)
        {
            MoveRangeDisplayed = true;
            List<int[]> moveTiles = BattleMap.GetLegalMoveCoordinates(selected);
            BattleMap.RedHighlightTiles(moveTiles);
        }

        //artillery does splash damage
        public List<int> GetSplashDamage(Tile attacked, int damage)
        {
            List<int> splash = new List<int>();
            if (attacked != null)
            {

                List<Tile> adjacent = AI.GetAllAdjacentTiles(BattleMap, attacked);
                List<Tile> splashTiles = new List<Tile>();

                foreach (Tile adj in adjacent)
                {
                    if (adj.hasUnit)
                    {
                        if (adj.TileUnit.isPlayerUnit != ActiveUnit.isPlayerUnit)
                        {
                            splash.Add(damage / 2);
                            splashTiles.Add(adj);
                        }
                    }
                }

                for (int i = 0; i < splash.Count; i++)
                {
                    splashTiles.ElementAt(i).TileUnit.HP -= splash.ElementAt(i);
                    DeathCheck(splashTiles.ElementAt(i).TileUnit);
                }
                GameUI.splashLocations = splashTiles;
            }
            return splash;
        }
        public void SelectSpecial()
        {
            specialMode = true;
            SelectEnabled = false;
            
        }
        public void DeselectSpecial()
        {
            specialMode = false;
            SelectEnabled = true;
        }

        public void SelectSpecialNumber(int specialNumber)
        {
            switch (specialNumber)
            {
                case 2: specialMode2 = true; break;
                case 3: specialMode3 = true; break;
                case 4: specialMode4 = true; break;
                case 5: specialMode5 = true; break;
                case 6: specialMode6 = true; break;
                default: specialMode1 = true; break;
            }
        }

        public void DeselectSpecialNumber()
        {
            ActiveUnit.DeselectSpecial1(this);
            ActiveUnit.DeselectSpecial2(this);
            ActiveUnit.DeselectSpecial3(this);
            ActiveUnit.DeselectSpecial4(this);
            ActiveUnit.DeselectSpecial5(this);
            ActiveUnit.DeselectSpecial6(this);
            BattleMap.ClearBlueHighlights();
            GameUI.specialAttack = false;
            BattleMap.ClearAllHighlights();
            specialMode1 = false;
            specialMode2 = false;
            specialMode3 = false;
            specialMode4 = false;
            specialMode5 = false;
            specialMode6 = false;
        }

        //Is any special move selected?
        public bool AnySpecialMoveSelected()
        {
            if (specialMode1 || specialMode2 || specialMode3 || specialMode4 || specialMode5 || specialMode6)
            {
                return true;
            }
            else
                return false;
        }

        public void DeathCheck(Unit target)
        {
            if (target.HP <= 0)
            {
                Ranger test = new Ranger();
                for (int i = 0; i < BattleQueue.Count; i++)
                {
                    Unit unit = BattleQueue.ElementAt(i);
                    if (unit is Ranger)
                    {
                        test = (Ranger)unit;
                        if (test.specialTarget == target)
                        {
                            test.specialTarget = null;
                            unit = test;
                        }
                    }
                }
                
                GameUI.sfx.PlayDieSound(target);
                //This is where a death animation would go IF WE HAD ONE
                RemoveUnit(target.Location[0], target.Location[1]);
                Units.Remove(target);
                BattleQueue.Remove(target);

                bool player1HasUnits = false;
                bool player2HasUnits = false;

                foreach (Unit unit in BattleQueue)
                {
                    if (unit.isPlayerUnit)
                    {
                        player1HasUnits = true;
                    }
                    else
                    {
                        player2HasUnits = true;
                    }
                }

                if (rangerTarget.Contains(target))
                {
                    int rangerIndex = rangerTarget.IndexOf(target);
                    rangersWithTargets.RemoveAt(rangerIndex);
                }

                if (player1HasUnits && !player2HasUnits)
                {
                    gameOver = true;
                    GameUI.endWait = true;
                    GameUI.p1win = true;
                    GameUI.PlayWinMusic();
                    return;
                }
                if (player2HasUnits && !player1HasUnits)
                {
                    GameUI.endWait = true;
                    gameOver = true;
                    GameUI.p2win = true;
                    GameUI.PlayLoseMusic();
                    return;
                }
            }
        }
    }
   

}
