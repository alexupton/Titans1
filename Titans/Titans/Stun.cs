using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Stun : StatusEffect
    {
        public Stun(Unit target)
        {
            unit = target;
            timeRemaining = 1;
            unit.StatusEffects.Add(this);
        }
        public override void DecreaseTime()
        {
            timeRemaining--;
        }

        public override void Invoke(Battle battle)
        {
            unit.AP = 0;
        }

        public override void UnInvoke(Battle battle)
        {

        }
    }
}
