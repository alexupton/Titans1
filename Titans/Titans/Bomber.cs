using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    //Create the bomber unit
    class Bomber : Unit
    {
        public override int HP { get; set; }
        public override int MP { get; set; }
        public override int MaxMP { get; set; }
        public override int Attack { get; set; }
        public override int Defense { get; set; }
        public override int Range { get; set; }
        public override int Speed { get; set; }
        public override int Init { get; set; } //the result of initiative rolls goes here
        public override int AP { get; set; }
        public override List<string> Abilities { get; set; } //just a list of ability names, the actual abilities are methods below
        public override bool isPlayerUnit { get; set; } //flag determines whether the unit belongs to player 1 or player 2
        public override int Price { get; set; } //for custom battles, each unit has a cost to add it to the player roster
        public override int[] Location { get; set; } //Array size 2. First is X location, second is Y location. set to {-1, -1} if the unit is not on the map
        public override int MaxHP { get; set; }
        public override bool DefendMode { get; set; }
        public override bool HasStatusEffects { get; set; }
        public override List<StatusEffect> StatusEffects { get; set; }


        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

        public override int StatusIndex { get; set; } //for status animation purposes

	//Set all attributes of the unit Bomber
        public Bomber()
        {
            MaxHP = 100;
            HP = 100;
            MP = 15;
            MaxMP = 15;
            Attack = 40;
            Defense = 5;
            Range = 4;
            Speed = 125;
            Init = 20;
            AP = 2;
            Abilities.Add("Inferno Bomb");
            Abilities.Add("Heal Bomb");
            Abilities.Add("Machine Gun");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
            StatusEffects = new List<StatusEffect>();
        }
        //Releases a bomb that deals 25 damage over 5 turns for the units hit
        public override void Special1(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            int damage = 5;
            battle.GameUI.unitDamage = damage;
            target.HP -= 5;
            //set damage over time counter to 5
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
            battle.DeathCheck(target);
            List<Tile> enemyTiles = AI.GetAdjacentEnemyTiles(target, battle.BattleMap);
            List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap, battle.BattleMap.GetTileAt(target.Location[0], target.Location[1]));
            foreach (Tile tile in adjacent)
            {
                if (tile.hasUnit)
                {
                    if (tile.TileUnit.isPlayerUnit == this.isPlayerUnit)
                    {
                        enemyTiles.Add(tile);
                    }
                }
            }
            battle.GameUI.splashDamage.Clear();
            battle.GameUI.splashLocations.Clear();
            foreach (Tile tile in enemyTiles)
            {
                battle.GameUI.splashLocations.Add(tile);
                battle.GameUI.splashDamage.Add(5);
                battle.GameUI.displayDamage = true;

                tile.TileUnit.HP -= 5;
                //set damage over time counter to 5
                battle.DeathCheck(tile.TileUnit);
            }
          
            this.MP -= 5;
            this.AP --;
            
            AttackModifiers.Remove(5);
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Set Heal Bomb ability which uses 10 MP and heals targets affected by 10
        public override void Special2(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            
            target.HP += 5;
            
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
            

            List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap,battle.BattleMap.GetTileAt(target.Location[0],target.Location[1]) );
            List<Tile> allyTiles= new List<Tile>();

            foreach (Tile tile in adjacent)
            {
                if (tile.hasUnit)
                {
                    if (tile.TileUnit.isPlayerUnit == this.isPlayerUnit)
                    {
                        allyTiles.Add(tile);
                    }
                }
            }
            //ally splash heal text
            foreach (Tile tile in allyTiles)
            {
                battle.GameUI.splashLocations.Add(tile);
                

                tile.TileUnit.HP += 10;
                
               
            }

            this.MP -= 10;
            this.AP--;

           
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;

        }
        //Set Machine Gun ablity which has a range of 1, attack modifier of 10, and uses 15 MP
        public override void Special3(Battle battle)
        {
           
            
            Unit target = battle.CurrentTarget;
            int damage = 10;
            battle.GameUI.unitDamage = damage;
            target.HP -= 10;
            //animation for damage text
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
            this.MP -= 15;
            this.AP --;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        public override void Special4(Battle battle)
        {
        }
        public override void Special5(Battle battle)
        {
        }
        public override void Special6(Battle battle)
        {
        }

        public override void SelectSpecial1(Battle battle)
        {

        }
        public override void SelectSpecial2(Battle battle)
        {

        }
        public override void SelectSpecial3(Battle battle)
        {

        }
        public override void SelectSpecial4(Battle battle)
        {

        }
        public override void SelectSpecial5(Battle battle)
        {

        }
        public override void SelectSpecial6(Battle battle)
        {

        }
        public override void DeselectSpecial1(Battle battle)
        {

        }
        public override void DeselectSpecial2(Battle battle)
        {

        }
        public override void DeselectSpecial3(Battle battle)
        {

        }
        public override void DeselectSpecial4(Battle battle)
        {

        }
        public override void DeselectSpecial5(Battle battle)
        {

        }
        public override void DeselectSpecial6(Battle battle)
        {

        }


    }
}
