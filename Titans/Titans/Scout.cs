using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    class Scout : Unit
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

        //Set all attributes of the unit Scout
        public Scout()
        {
            MaxHP = 30;
            HP = 30;
            MP = 30;
            MaxMP = 30;
            Attack = 30;
            Defense = 10;
            Range = 0;
            Speed = 375;
            Init = 100;
            AP = 2;
            Abilities.Add("Bridge");
            Abilities.Add("Sikes");
            Abilities.Add("Remove");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
        }
        //Change a water tile into a bridge tile
        public override void Special1(Battle battle)
        {
            Tile tile = battle.SelectedTile;

            battle.BattleMap.map[tile.X][tile.Y] = new Tile();
            tile.MoveCost = 10;
            tile.type = "scoutBridge";
            tile.AssignFileName();
            tile.IsRedHighlighted = false;
            tile.IsRoot = false;
            tile.Searched = false;
            tile.FScore = 0;
            tile.GScore = 0;
            tile.HScore = 0;
            tile.HasTrap = false;
            tile.hasUnit = false;
            tile.TileUnit = null;
            tile.IsImpassible = false;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;


            this.AP--;
            this.MP -= 10;



        }
        //Set spikes on a specific tile
        public override void Special2(Battle battle)
        {
            Tile selected = battle.SelectedTile;

            selected.HasTrap = true;

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;


            this.AP--;
            this.MP -= 10;
        }
        //Remove spikes or scout bridges
        public override void Special3(Battle battle)
        {
            Tile tile = battle.SelectedTile;

            tile.HasTrap = false;

            if (tile.type == "scoutBridge")
            {
                battle.BattleMap.map[tile.X][tile.Y] = new Tile();
                tile.MoveCost = 10;
                tile.type = "water";
                tile.AssignFileName();
                tile.IsRedHighlighted = false;
                tile.IsRoot = false;
                tile.Searched = false;
                tile.FScore = 0;
                tile.GScore = 0;
                tile.HScore = 0;
                tile.HasTrap = false;
                tile.hasUnit = false;
                tile.TileUnit = null;
                tile.IsImpassible = true;
            }


            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;


            this.AP--;
            this.MP -= 10;

            
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
