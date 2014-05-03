using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Slow : StatusEffect
    {
        public Slow(Unit target)
        {
            unit = target;
            timeRemaining = 3;
            unit.StatusEffects.Add(this);
        }

        public override void Invoke(Battle battle)
        {
            battle.BattleQueue.Remove(unit);
            battle.BattleQueue.Add(unit);
        }

        public override void DecreaseTime()
        {
            timeRemaining--;
        }

        public override void UnInvoke(Battle battle)
        {
            
        }
    }
}
