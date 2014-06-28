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
        public override bool HasStatusEffects { get; set; }
        public override List<StatusEffect> StatusEffects { get; set; }

        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction

        public override int StatusIndex { get; set; } //for status animation purposes

        //Set all attributes of the unit Mage
        public Mage()
        {
            MaxHP = 25;
            HP = 25;
            MP = 100;
            MaxMP = 100;
            Attack = 15;
            Defense = 10;
            Range = 5;
            Speed = 160;
            Init = 30;
            AP = 2;
            Abilities = new List<string>();
            Abilities.Add("Firebolt");
            Abilities.Add("Magic Strike");
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
        //Set the Firebolt ability which deals 50 damage to one target and half that to adjecent and costs 40 MP
        public override void Special1(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            this.Attack = 50;
            int damage = 50;
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;
            battle.DeathCheck(target);
            //splash damage
            List<Tile> splashTiles = AI.GetSplashDamageTiles(target, battle.BattleMap);
            foreach (Tile tile in splashTiles)
            {
                battle.GameUI.splashLocations.Add(tile);
                battle.GameUI.splashDamage.Add(25);
                battle.GameUI.displayDamage = true;

                tile.TileUnit.HP -= damage / 2;

                battle.DeathCheck(tile.TileUnit);
            }
         
            MP -= 40;
            this.AP-=2;
            this.Attack = 15;

            battle.GameUI.sfx.PlaySpecialSound(this, 1);
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Set the Magic Strike ability which deals 25  damage and costs 20 MP
        public override void Special2(Battle battle)
        {
           
            Unit target = battle.CurrentTarget;
            this.Attack = 25;
            int damage = AttackResolver.Attack(this, target, this.AttackModifiers);
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;

            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;
            battle.DeathCheck(target);

            this.MP -= 20;
            this.AP--;
            this.Attack = 15;
           
            
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
            battle.GameUI.sfx.PlaySpecialSound(this, 2);
        }
        //Set the Heal ability which costs 15 MP and heals target ally for 20 hp
        public override void Special3(Battle battle)
        {
            Unit target = battle.CurrentTarget;
            if (target.HP != target.MaxHP)
            {
                int heal = 20;
                target.HP += heal;
                battle.GameUI.displayHeal = true;
                battle.GameUI.unitHeal = heal;

                if (target.HP > target.MaxHP)
                {
                    target.HP = target.MaxHP;
                }
                this.MP -= 15;
                this.AP--;
                battle.GameUI.sfx.PlaySpecialSound(this, 3);
            }
            else
            {
                battle.GameUI.sfx.PlayBuzzer();
            }
                battle.GameUI.timeSinceLastDamageFrame = 0;
                battle.GameUI.frameCount = 0;
                battle.GameUI.wait = true;
            
          

        }
        //Set the Refresh ability which costs 20 MP and removes all status effects
        public override void Special4(Battle battle)
        {
           
            Unit target = battle.CurrentTarget;
            foreach (StatusEffect status in target.StatusEffects)
            {
                if (!(status is Haste))
                {
                    target.StatusEffects.Remove(status);
                }
            }
            this.MP -= 20;
            this.AP--;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
            battle.GameUI.sfx.PlaySpecialSound(this, 4);
            
        }
        //Set the Haste ability which costs 20 MP and gives target ally a 50 bounus to movement for three rounds
        public override void Special5(Battle battle)
        {
            Unit target = battle.CurrentTarget;
           
            //set targets status
            //text animation
            if (AI.HasStatusEffect(target, "Haste"))
            {
                foreach (StatusEffect effect in target.StatusEffects)
                {
                    if (effect is Haste)
                    {
                        effect.ResetTime();
                    }
                }
            }
            else
            {
                Haste haste = new Haste(target);
            }
            this.MP -= 20;
            this.AP--;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
            battle.GameUI.sfx.PlaySpecialSound(this, 5);
         

        }
        //Set the Slow ability which costs 20 MP and makes target unit go last
        public override void Special6(Battle battle)
        {
            
            Unit target = battle.CurrentTarget;
            //sets target status
            //text animation
            Slow slow = new Slow(target);
            this.MP -= 20;
            this.AP--;
            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
            battle.GameUI.sfx.PlaySpecialSound(this, 6);
            

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
