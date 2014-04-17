using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public static class AttackResolver
    {

        //basic attack method
        //return the amount of damage done to the defending unit
        public static int Attack(Unit attacker, Unit defender, List<int> combatModifiers)
        {
            Random rand = new Random();
            int damage = attacker.Attack + (rand.Next(-6, 6));

            //sum over all combat modifiers, such as flanking or status effects
            int mods = 0;
            foreach (int mod in combatModifiers)
            {
                mods += mod;
            }
            double defenseMod = (100.0 - (double)(defender.Defense)) / 100;
            damage += mods;
            double dDamage = (double)damage;
            double rawDamage = dDamage * defenseMod; //add the mods and reduce the damage by the defender's percentage
            if (rawDamage < 0)
            {
                rawDamage = 0;
            }
            damage = (int)Math.Round(rawDamage); //round and assign the final damage value
            return damage;
        }
    }
}
