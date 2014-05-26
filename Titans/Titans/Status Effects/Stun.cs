using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Stun : StatusEffect
    {
        protected override Unit unit { get; set; }
        protected override int timeRemaining { get; set; }
        public Stun(Unit target)
        {
            unit = target;
            timeRemaining = 2;
            unit.StatusEffects.Add(this);
        }
        public override void DecreaseTime()
        {
            timeRemaining--;
        }

        public override void Invoke(Battle battle)
        {
            unit.AP = 0;
            battle.GameUI.sfx.PlayPassSound(battle.ActiveUnit);
        }

        public override void UnInvoke(Battle battle)
        {

        }
        public override int GetTimeRemaining()
        {
            return timeRemaining;
        }

        public override void ResetTime()
        {
            timeRemaining = 2;
        }
    }
}
