﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    class Ranger : Unit
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

        //Set all attributes of the unit Ranger
        public Ranger()
        {
            MaxHP = 30;
            Abilities = new List<string>();
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
            Location = new int[2];
            HP = 30;
            MP = 25;
            MaxMP = 25;
            Attack = 30;
            Defense = 10;
            Range = 7;
            Speed = 150;
            Init = 40;
            AP = 2;
            Abilities.Add("Headshot");
            Abilities.Add("Immobilize");
            Abilities.Add("Target");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
        }
        //Set the Headshot ability which adds an attack modifier of 40 and costs 15 MP
        public override void Special1(Battle battle)
        {

            //pre abbility modifiers
            AttackModifiers.Add(40);
            MP -= 15;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(40);
        }
        //Set the Immobilize ability which adds an attack modifier of 15 and costs 10 MP
        public override void Special2(Battle battle)
        {

            //pre abbility modifiers
            AttackModifiers.Add(15);
            MP -= 10;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(15);
        }
        //Set the Target ability which costs 10 MP and....
        public override void Special3(Battle battle)
        {
            //pre abbility modifiers
            MP -= 10;
            //code for calling animation
            //post abbility modifiers 

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



    }
}
