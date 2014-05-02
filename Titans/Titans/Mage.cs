using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    class Mage : Unit
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

        //Set all attributes of the unit Mage
        public Mage()
        {
            MaxHP = 25;
            HP = 25;
            MP = 100;
            MaxMP = 100;
            Attack = 15;
            Defense = 10;
            Range = 3;
            Speed = 125;
            Init = 30;
            AP = 2;
            Abilities = new List<string>();
            Abilities.Add("Firebolt");
            Abilities.Add("Magic Slice");
            Abilities.Add("Heal");
            Abilities.Add("Refresh");
            Abilities.Add("Haste");
            Abilities.Add("Slow");
            isPlayerUnit = true;
            Price = 100;
            Location = new int[2];
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
        }
        //Set the Firebolt ability which adds an attack modifier of 50 and costs 30 MP
        public override void Special1(Battle battle)
        {
            //pre abbility modifiers
            AttackModifiers.Add(50);
            MP -= 30;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(50);
        }
        //Set the Poison ability which adds an attack modifier of 5 and costs 20 MP
        public override void Special2(Battle battle)
        {
            //pre abbility modifiers
            AttackModifiers.Add(5);
            MP -= 20;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(5);
        }
        //Set the Heal ability which costs 15 MP and.....
        public override void Special3(Battle battle)
        {
            //pre abbility modifiers

            MP -= 15;
            //code for calling animation
            //post abbility modifiers 

        }
        //Set the Refresh ability which costs 20 MP and.....
        public override void Special4(Battle battle)
        {
            //pre abbility modifiers

            MP -= 20;
            //code for calling animation
            //post abbility modifiers 
        }
        //Set the Haste ability which costs 20 MP and.....
        public override void Special5(Battle battle)
        {
            //pre abbility modifiers

            MP -= 20;
            //code for calling animation
            //post abbility modifiers 

        }
        //Set the Slow ability which costs 20 MP and....
        public override void Special6(Battle battle)
        {
            //pre abbility modifiers

            MP -= 20;
            //code for calling animation
            //post abbility modifiers 

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
