using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    //Created the artillery unit
    class Artillery : Unit
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
        

        public Artillery()
        {
            MaxHP = 50;
            HP = 50;
            MP = 0;
            Attack = 35;
            Defense = 0;
            Range = 10;
            Speed = 100;
            Init = 15;
            AP = 2;
            Abilities.Add("");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0); 
        
        }

        public override void Special1() { }
        public override void Special2() { }
        public override void Special3() { }
        public override void Special4() { }
        public override void Special5() { }
        public override void Special6() { }
		
		//yo no hablo ingles
    }
}