using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    //Create the Cavalry unit
    class Cavalry : Unit
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


	//Set all attributes of the unit Cavalry
        public Cavalry()
        {
            MaxHP = 50;
            HP = 50;
            MP = 30;
            MaxMP = 30;
            Attack = 20;
            Defense = 15;
            Range = 1;
            Speed = 475;
            Init = 75;
            AP = 2;
            Abilities = new List<string>();
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
            Location = new int[2];
            Abilities.Add("Pierce");
            Abilities.Add("Charge");
            Abilities.Add("Swap");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
            StatusEffects = new List<StatusEffect>();
        }
        //Set Pierce ability which has an attack modifier of 20 and uses 5 MP
        public override void Special1(Battle battle)
        {
            Unit target = battle.CurrentTarget;

            //0 - left
            //1 - right
            //2 - up
            //3 - down
            int direction = 4;

            if (target.Location[0] == this.Location[0] - 1)
            {
                direction = 0;
            }
            else if (target.Location[0] == this.Location[0] + 1)
            {
                direction = 1;
            }
            else if (target.Location[1] == this.Location[1] - 1)
            {
                direction = 2;
            }
            else if (target.Location[1] == this.Location[1] + 1)
            {
                direction = 3;
            }

            Unit secondary = new Soldier();
            if (direction == 0)
            {
                if (battle.BattleMap.GetTileAt(target.Location[0] - 1, target.Location[1]).hasUnit)
                {
                    if (battle.BattleMap.GetTileAt(target.Location[0] - 1, target.Location[1]).TileUnit.isPlayerUnit == target.isPlayerUnit)
                    {
                        secondary = battle.BattleMap.GetTileAt(target.Location[0] - 1, target.Location[1]).TileUnit;
                    }
                }
            }
            else if (direction == 1)
            {
                if (battle.BattleMap.GetTileAt(target.Location[0] + 1, target.Location[1]).hasUnit)
                {
                    if (battle.BattleMap.GetTileAt(target.Location[0] +1, target.Location[1]).TileUnit.isPlayerUnit == target.isPlayerUnit)
                    {
                        secondary = battle.BattleMap.GetTileAt(target.Location[0] + 1, target.Location[1]).TileUnit;
                    }
                }
            }
            else if (direction == 2)
            {
                if (battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] - 1).hasUnit)
                {
                    if (battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] - 1).TileUnit.isPlayerUnit == target.isPlayerUnit)
                    {
                        secondary = battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] - 1).TileUnit;
                    }
                }
            }
            else if (direction == 3)
            {
                if (battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] + 1).hasUnit)
                {
                    if (battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] + 1).TileUnit.isPlayerUnit == target.isPlayerUnit)
                    {
                        secondary = battle.BattleMap.GetTileAt(target.Location[0], target.Location[1] + 1).TileUnit;
                    }
                }
            }

            int damage = AttackResolver.Attack(this, target, this.AttackModifiers);
            battle.GameUI.unitDamage = damage;
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
            if (direction < 4)
            {
                battle.GameUI.splashDamage.Clear();
                battle.GameUI.splashLocations.Clear();
                battle.GameUI.splashDamage.Add(damage / 2);
                battle.GameUI.splashLocations.Add(battle.BattleMap.GetTileAt(secondary.Location[0], secondary.Location[1]));
            }

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;

            battle.DeathCheck(target);
            battle.DeathCheck(secondary);

            this.AP--;
            this.MP -= 5;
        }
        //Set Charge ability which adds 5 to the speed, not allowing the unit to attack this turn, and uses 10 MP
        public override void Special2(Battle battle)
        {

            //NOT YET DONE
            //pre abbility modifiers
            Speed += 5;
            Range = 0;
            MP -= 10;
            this.AP--;
            //code for calling animation
            //post abbility modifiers 
            Speed -= 5;
            Range = 1;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Set Swap ability which allows the cavalry unit to swap places with another friendly unit using 10 MP
        public override void Special3(Battle battle)
        {

            Unit target = battle.CurrentTarget;

            battle.RemoveUnit(this.Location[0], this.Location[1]);
            battle.RemoveUnit(target.Location[0], target.Location[1]);

            

            //calculate defender bonuses on the new locations
            List<Tile> adja = AI.GetAllAdjacentTiles(battle.BattleMap, battle.BattleMap.GetTileAt(this.Location[0], this.Location[1]));
            foreach (Tile adj in adja)
            {
                if (adj.hasUnit)
                {
                    if (adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == this.isPlayerUnit)
                    {
                        this.Defense += 5;
                        this.DefenseModifiers.Add(5);
                    }
                }
            }

            adja = AI.GetAllAdjacentTiles(battle.BattleMap, battle.BattleMap.GetTileAt(target.Location[0], target.Location[1]));
            foreach (Tile adj in adja)
            {
                if (adj.hasUnit)
                {
                    if (adj.TileUnit is Defender && adj.TileUnit.isPlayerUnit == target.isPlayerUnit)
                    {
                        target.Defense += 5;
                        target.DefenseModifiers.Add(5);
                    }
                }
            }

            this.AP--;
            this.MP -= 10;
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
