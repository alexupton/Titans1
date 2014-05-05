using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Ranger : Unit
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

        public Unit specialTarget { get; set; }
        public int targetTimeRemaining { get; set; }

        public override int StatusIndex { get; set; } //for status animation purposes

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
            Unit target = battle.CurrentTarget;
            int damage = AttackResolver.Attack(this, target, this.AttackModifiers) +10;
            battle.GameUI.unitDamage = damage;
            target.HP -= 40;
            //animation for damage text
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;
            if (!AI.HasStatusEffect(target, "Stun"))
            {
                Stun stun = new Stun(target);
            }
            else
            {
                foreach (StatusEffect effect in target.StatusEffects)
                {
                    if (effect is Stun)
                    {
                        effect.ResetTime();
                    }
                }
            }
            this.MP -= 15;
            this.AP--;

            battle.DeathCheck(target);
            
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Set the Immobilize ability which adds an attack modifier of 15 and costs 10 MP and has chance to imobilize target
        public override void Special2(Battle battle)
        {

          
            Unit target = battle.CurrentTarget;
            int damage = 15;
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;
            if (!AI.HasStatusEffect(target, "Root"))
            {
                Root root = new Root(target);
            }
            else
            {
                foreach (StatusEffect status in target.StatusEffects)
                {
                    if (status is Root)
                    {
                        status.ResetTime();
                    }
                }
            }
            

            //animation for damage text
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 + battle.GameUI.offsetX - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 + battle.GameUI.offsetY - 20;
            
            MP -= 10;
            this.AP--;

            battle.DeathCheck(target);
            
            
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Target one unit, even if he moves out of range
        public override void Special3(Battle battle)
        {
            if (battle.rangersWithTargets.Contains(this))
            {
                battle.GameUI.sfx.PlayBuzzer();
            }
            else
            {
                this.specialTarget = battle.CurrentTarget;
                this.MP -= 10;
                this.AP--;

                battle.GameUI.timeSinceLastDamageFrame = 0;
                battle.GameUI.frameCount = 0;
                battle.GameUI.wait = true;
            }

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
