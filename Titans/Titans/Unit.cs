﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public abstract class Unit
    {
        public abstract int HP { get; set; }
        public abstract int MP { get; set; }
        public abstract int Attack { get; set; }
        public abstract int Defense { get; set; }
        public abstract int Range { get; set; }
        public abstract int Speed { get; set; }
        public abstract int Init { get; set; } //the result of initiative rolls goes here
        public abstract int AP { get; set; }
        public abstract List<string> Abilities { get; set; } //just a list of ability names, the actual abilities are methods below
        public abstract bool isPlayerUnit { get; set; } //flag determines whether the unit belongs to player 1 or player 2
        public abstract int Price { get; set; }//for custom battles, each unit has a cost to add it to the player roster
        public abstract int[] Location { get; set; } //Array size 2. First is X location, second is Y location. set to {-1, -1} if the unit is not on the map
        public abstract int MaxHP { get; set; }

        public abstract List<int> AttackModifiers{get; set;} //list of modifiers applied to attacks
        public abstract List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

        //Add any additional properties you may need to this class AND NOT SUBCLASSES
 
        //special ability methods
        public abstract void Special1();
        public abstract void Special2();
        public abstract void Special3();

        //additional methods for mage spells only
        //leave blank for other classes
        public abstract void Special4();
        public abstract void Special5();
        public abstract void Special6();

        public string GetType()
        {
            if (this is Artillery)
            {
                return "Artillery";
            }
            else if (this is Bomber)
            {
                return "Bomber";
            }
            else if (this is Cavalry)
            {
                return "Cavalry";
            }
            else if (this is Defender)
            {
                return "Defender";
            }
            else if (this is Fighter)
            {
                return "Fighter";
            }
            else if (this is Mage)
            {
                return "Mage";
            }
            else if (this is Ranger)
            {
                return "Ranger";
            }
            else if (this is Scout)
            {
                return "Scout";
            }
            else
            {
                return "Soldier";
            }
        }

    }
}
