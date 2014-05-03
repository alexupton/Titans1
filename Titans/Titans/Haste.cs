using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Haste : StatusEffect
    {
        private int originalSpeed;
        public Haste(Unit target)
        {
            unit = target;
            target.StatusEffects.Add(this);
            timeRemaining = 3;
            originalSpeed = target.Speed;
        }

        public override void DecreaseTime()
        {
            timeRemaining--;
        }

        public override void Invoke(Battle battle)
        {
            unit.Speed += 50;
        }

        public override void UnInvoke(Battle battle)
        {
            unit.Speed = originalSpeed;
        }
    }
}
