using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        //any more custom rule options go here

        public Battle()
        {
            Units = new List<Unit>();
            Points = 1000; //suggested amount, can be changed
            TurnCount = 1;
            MoveMode = false;
            AttackMode = false;
            MoveEnabled = true;
            AttackEnabled = true;
            SelectEnabled = true;
        }

        public Battle(Map newMap)
        {
            BattleMap = newMap;
            Units = new List<Unit>();
            TurnCount = 1;
            MoveMode = false;
            AttackMode = false;
            MoveEnabled = true;
            AttackEnabled = true;
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

        //Remove unit FROM THE MAP at the specified coodinates
        public void RemoveUnit(int X, int Y)
        {
            BattleMap.map[X][Y].TileUnit = null;
            BattleMap.map[X][Y].hasUnit = false;
            BattleMap.map[X][Y].IsImpassible = false;
        }

        public List<Unit> RollInitiative()
        {
            //release the wait hold, other cleanup
            if (GameUI != null)
            {
                GameUI.wait = false;
                GameUI.ResetButtons();
                GameUI.moveWait = false;
                GameUI.tickWait = false;
            }
            SelectEnabled = true;



            QueuePosition = 0;
            List<Unit> turnOrder = new List<Unit>();
            Random rand = new Random();

            List<Unit> player1Units = new List<Unit>();
            List<Unit> player2Units = new List<Unit>();
            

            foreach (Unit unit in Units)
            {
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



            //foreach (Unit unit in Units)
            //{
            //    unit.Init = unit.Speed + rand.Next(51); //add a random roll to the speed to get initiative
            //    unit.AP = 2;
            //}

            //turnOrder = Units.OrderByDescending(u => u.Init).ToList(); //using LINQ to order by initiative value
            BattleQueue = turnOrder; //save the turn order
            ActiveUnit = BattleQueue.ElementAt(0); //set the ActiveUnit to the first in the turn order

            //clear defense buffs
            int defMod = 0;
            if (ActiveUnit.DefenseModifiers.Count > 0)
            {
                foreach (int mod in ActiveUnit.DefenseModifiers)
                {
                    defMod += mod;
                }
                ActiveUnit.DefenseModifiers.Clear();
            }
            ActiveUnit.Defense -= defMod;
            return turnOrder;
        }
        
        //after a unit moves, call this method to access the next unit in the battle queue
        public Unit NextPlayer()
        {
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
                
            }
            else
            {
                QueuePosition = 0;
                TurnCount++;
                RollInitiative();
            }

            DeselectAttack();
            GameUI.SetOffsetValue(ActiveUnit.Location[0] * -55 + 750, ActiveUnit.Location[1] * -55 + 400);

            GameUI.sfx.PlaySelectSound(ActiveUnit);

            //clear defense buffs
            int defMod = 0;
            if (ActiveUnit.DefenseModifiers.Count > 0)
            {
                foreach (int mod in ActiveUnit.DefenseModifiers)
                {
                    defMod += mod;
                }
                ActiveUnit.DefenseModifiers.Clear();
            }
            ActiveUnit.Defense -= defMod;
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
            List<Tile> movePath = AI.GetPath(BattleMap.GetTileAt(ActiveUnit.Location[0], ActiveUnit.Location[1]), move, BattleMap);
            pendingMoves = movePath.ToArray();
            pendingIndex = pendingMoves.Length - 1;
            GameUI.sfx.PlayMoveSound(ActiveUnit);
        }
        //advance to the next tile in the move list
        //if at the end, the move is finished
        public void ContinueMove()
        {
                
                BattleMap.Move(ActiveUnit, pendingMoves[pendingIndex].X, pendingMoves[pendingIndex].Y);
                pendingIndex--;
                if (pendingIndex == -1)
                {
                    GameUI.moveWait = false;
                    ActiveUnit.AP--;
                    BattleMap.ClearHighlights();
                    MoveMode = false;
                    pendingIndex = 0;
                    pendingMoves = new Tile[0];

                }
        }

        public void SelectDefend()
        {
            MoveMode = false;
            AttackMode = false;
            ActiveUnit.DefenseModifiers.Add(5);
            ActiveUnit.Defense += 5;
            ActiveUnit.AP = 0;
        }
                
                
            


            
        

        public bool SelectAttack()
        {
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
                if (BattleMap.map[coords[0]][coords[1]].hasUnit)
                {
                    if (BattleMap.map[coords[0]][coords[1]].TileUnit.isPlayerUnit != ActiveUnit.isPlayerUnit)
                    {
                        BattleMap.AddSpecificRedHighlight(coords[0], coords[1]);
                        validTargetExists = true;
                    }
                }
            }

            if (!validTargetExists)
            {
                BattleMap.ClearHighlights();
                AttackMode = false;
            }
            else
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
            SelectEnabled = true;
            BattleMap.ClearHighlights();
            List<int> combatMods = ActiveUnit.AttackModifiers;

            int storedDefense = target.Defense; //store the defender's defense temporarily

            foreach (int mod in target.DefenseModifiers)
            {
                target.Defense += mod; //temporarily add the defense modifiers to the base defense of defender
            }

            int damage = AttackResolver.Attack(ActiveUnit, target, combatMods);

            target.Defense = storedDefense; //restore defender base defense
            target.HP -= damage;

            
            //check if the unit is dead. if it is, turf it
            if (target.HP <= 0)
            {
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

                if (player1HasUnits && !player2HasUnits)
                {
                    gameOver = true;
                    GameUI.endWait = true;
                    GameUI.p1win = true;
                    GameUI.PlayWinMusic();
                }
                if (player2HasUnits && !player1HasUnits)
                {
                    GameUI.endWait = true;
                    gameOver = true;
                    GameUI.p2win = true;
                    GameUI.PlayWinMusic();
                }
            }
            else
            {
                GameUI.sfx.PlayAttackSound(ActiveUnit);
            }
            AttackMode = false;
            ActiveUnit.AP--;
            if (ActiveUnit.AP == 0 && !GameUI.wait)
                NextPlayer();

            return damage;

        }
    }
}
