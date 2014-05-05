using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Haste : StatusEffect
    {
        
        protected override Unit unit { get; set; }
        protected override int timeRemaining { get; set; }
        private int originalSpeed;
        public Haste(Unit target)
        {
            unit = target;
            target.StatusEffects.Add(this);
            timeRemaining = 4;
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
        public override int GetTimeRemaining()
        {
            return timeRemaining;
        }
        public override void ResetTime()
        {
            timeRemaining = 4;
        }
    }
}
