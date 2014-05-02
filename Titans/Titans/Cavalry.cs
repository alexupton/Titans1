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

        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

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
            Abilities.Add("Pierce");
            Abilities.Add("Charge");
            Abilities.Add("Swap");
            isPlayerUnit = true;
            Price = 100;
            Location[0] = -1;
            Location[1] = -1;
            AttackModifiers.Add(0);
            DefenseModifiers.Add(0);
        }
        //Set Pierce ability which has an attack modifier of 20 and uses 5 MP
        public override void Special1(Battle battle)
        {

            //pre abbility modifiers
            AttackModifiers.Add(20);
            MP -= 5;
            //code for calling animation
            //post abbility modifiers 
            AttackModifiers.Remove(20);
        }
        //Set Charge ability which adds 5 to the speed, not allowing the unit to attack this turn, and uses 10 MP
        public override void Special2(Battle battle)
        {

            //pre abbility modifiers
            Speed += 5;
            Range = 0;
            MP -= 10;
            //code for calling animation
            //post abbility modifiers 
            Speed -= 5;
            Range = 1;
        }
        //Set Swap ability which allows the cavalry unit to swap places with another friendly unit using 10 MP
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
