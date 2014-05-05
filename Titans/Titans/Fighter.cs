using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{

    class Fighter : Unit
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

        //Set all attributes of the unit Fighter
        public Fighter()
        {
            MaxHP = 60;
            HP = 60;
            MP = 20;
            MaxMP = 20;
            Attack = 25;
            Defense = 15;
            Range = 2;
            Speed = 175;
            Init = 50;
            AP = 2;
            Abilities = new List<string>();
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
            Location = new int[2];
            Abilities.Add("Flares");
            Abilities.Add("Heat Seeking Missile");
            Abilities.Add("Sonic Strike");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
        }
        //Set the Flares ability which costs 10 MP and is used on the units current tile
        public override void Special1(Battle battle)
        {
            
            
            MP -= 10;
            
        }
        //Set the Heat Seeking Missile ability which adds an attack modifier of 15, costs 10 MP, and has a range of 6
        public override void Special2(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            int damage = 15;
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;

            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
           
            this.MP -= 10;
            this.AP--;
        }
        //Set the Sonic Strike ability which adds an attack modifier of 10, reduce speed to 100, and costs 15 MP
        public override void Special3(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            int damage = 10;
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;

            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
           
            this.MP -= 15;
            
            this.AP--;
           
          
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
