﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    class Soldier : Unit
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

        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

        //Set all attributes of the unit Soldier
        public Soldier()
        {
            MaxHP = 70;
            Abilities = new List<string>();
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
            Location = new int[2];
            HP = 70;
            MP = 20;
            MaxMP = 20;
            Attack = 40;
            Defense = 25;
            Range = 1;
            Speed = 150;
            Init = 50;
            AP = 2;
            Abilities.Add("Whirlwind");
            Abilities.Add("Double Attack");
            Abilities.Add("First Aid");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
        }
        //Set the Whirlwind ability which attacks all units in a splash area around the unit
        public override void Special1(Battle battle)
        {

            List<Tile> enemyTiles = AI.GetAdjacentEnemyTiles(this, battle.BattleMap);
            battle.GameUI.splashDamage.Clear();
            battle.GameUI.splashLocations.Clear();
            foreach (Tile tile in enemyTiles)
            {
                battle.GameUI.splashLocations.Add(tile);
                battle.GameUI.splashDamage.Add(15);
                battle.GameUI.displayDamage = true;

                tile.TileUnit.HP -= 15;

                battle.DeathCheck(tile.TileUnit);
            }
            this.AP--;
            this.MP -= 5;

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
        }
        //Set the Double Attack ability which attacks a unit for 150% basic damage
        public override void Special2(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            int damage = AttackResolver.Attack(this, target, this.AttackModifiers);

            battle.GameUI.unitDamage = damage;
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;

            battle.GameUI.splashDamage.Clear();
            battle.GameUI.splashLocations.Clear();
            battle.GameUI.splashDamage.Add(damage / 2);
            battle.GameUI.splashLocations.Add(battle.BattleMap.GetTileAt(target.Location[0], target.Location[1]));

            this.AP--;
            this.MP -= 10;

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;


        }
        //Set the First Aid ability which adds 5 HP, costs 10 MP, and reduces the range to 0
        public override void Special3(Battle battle)
        {
            //TODO: trigger animation for +5 HP
            this.HP += 5;
            //TODO: remove other statuses
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
            battle.BattleMap.BlueHighlightTiles(AI.GetAllAdjacentTiles(battle.BattleMap, battle.BattleMap.GetTileAt(this.Location[0], this.Location[1])));
            battle.BattleMap.RedHighlightTiles(AI.GetAdjacentEnemyTiles(this, battle.BattleMap));
            battle.GameUI.selfSelect = true;
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