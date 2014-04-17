using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    class Bomber : Unit
    {
        public override int HP { get; set; }
        public override int MP { get; set; }
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


        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

        public Bomber()
        {
            MaxHP = 100;
            HP = 100;
            MP = 15;
            Attack = 40;
            Defense = 5;
            Range = 4;
            Speed = 125;
            Init = 20;
            AP = 2;
            Abilities.Add("Inferno Bmomb");
            Abilities.Add("Heal Bomb");
            Abilities.Add("Machine Gun");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
        }
        public override void Special1()
        {
            //pre abbility modifiers
            AttackModifiers.Add(5);
            MP -= 5;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(5);
        }
        public override void Special2()
        {
            //pre abbility modifiers

            MP -= 10;
            //code for calling animation
            //post abbility modifiers 

        }
        public override void Special3()
        {
            //pre abbility modifiers
            AttackModifiers.Add(10);
            Range = 1;
            MP -= 15;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(10);
            Range = 4;
        }
        public override void Special4()
        {
        }
        public override void Special5()
        {
        }
        public override void Special6()
        {
        }


    }
}
