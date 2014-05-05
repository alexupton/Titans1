using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    //Create the Defender unit
    class Defender : Unit
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
        public override bool HasStatusEffects { get; set; }
        public override List<StatusEffect> StatusEffects { get; set; }

        public override List<int> AttackModifiers { get; set; } //list of modifiers applied to attacks
        public override List<int> DefenseModifiers { get; set; } //list of modifiers applied to defense, represent percentages of damage reduction
        public override bool DefendMode { get; set; }

        public override int StatusIndex { get; set; } //for status animation purposes

	//Set all the attributes of the unit Defender
        public Defender()
        {
            Abilities = new List<string>();
            AttackModifiers = new List<int>();
            DefenseModifiers = new List<int>();
            Location = new int[2];
            HP = 100;
            MaxHP = 100;
            MP = 30;
            MaxMP = 30;
            Attack = 20;
            Defense = 50;
            Range = 1;
            Speed = 150;
            Init = 25;
            AP = 2;
            Abilities.Add("Shield Bash");
            Abilities.Add("Def. Strike");
            Abilities.Add("Taunt");
            isPlayerUnit=true;
            Price = 100;
            Location[0]=-1;
            Location[1] = -1;
            DefendMode = false;
        }
        //Set Shield Bash ability with an attack modifier of 10, will stun the enemy unit, and uses 10 MP
        public override void Special1(Battle battle)
        {
            
            //pre abbility modifiers
            Unit target = battle.CurrentTarget;
            int damage = 10 ;
            battle.GameUI.unitDamage = damage;
            target.HP -= 10;
            Stun stun = new Stun(target);
            //animation for damage text
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;
            MP-=10;
            this.AP--;
            battle.DeathCheck(target);

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
        }
        //Attacks enemy for half damage and puts defender in defensive position
        public override void Special2(Battle battle)
        {
             
            //pre abbility modifiers
            
            Unit target = battle.CurrentTarget;
            int damage = AttackResolver.Attack(this, target, this.AttackModifiers)/2;
            battle.GameUI.unitDamage = damage;
            target.HP -= damage;
            //animation for damage text
            battle.GameUI.displayDamage = true;
            battle.GameUI.attackedUnitTrueX = target.Location[0] * 55 - 13;
            battle.GameUI.attackedUnitTrueY = target.Location[1] * 55 - 20;


            this.DefenseModifiers.Add(5);
            this.DefendMode = true;
            this.MP-=10;
            this.AP -= 2;

            battle.DeathCheck(target);

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
            
        }
        //Set Taunt ability which uses 15 MP and increases the unit's range to 5 for one turn
        public override void Special3(Battle battle)
        {
             
            Unit target = battle.CurrentTarget;
            Taunt taunt = new Taunt(this,target);
            
            MP -= 15;
            this.AP--;

            battle.GameUI.timeSinceLastDamageFrame = 0;
            battle.GameUI.frameCount = 0;
            battle.GameUI.wait = true;
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
            battle.DeselectSpecialNumber();
            battle.SelectSpecialNumber(1);
        }
        public override void SelectSpecial2(Battle battle)
        {

        }
        public override void SelectSpecial3(Battle battle)
        {
            battle.DeselectSpecialNumber();
            this.Range = 5;
            
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
            this.Range = 1;
            List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap,battle.BattleMap.GetTileAt( this.Location[0], this.Location[1]));
            foreach (Tile adj in adjacent)
            {
                adj.ClearBlueHighlight();
            }
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
