using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Taunt : StatusEffect
    {
        protected override Unit unit { get; set; }
        protected override int timeRemaining { get; set; }
        public Unit TauntTarget { get; private set; }
        public Taunt(Unit sender, Unit target)
        {
            TauntTarget = sender;
            unit = target;
            timeRemaining = 3;
            unit.StatusEffects.Add(this);
        }

        public override void DecreaseTime()
        {
            timeRemaining--;
        }

        public override void Invoke(Battle battle)
        {
            battle.GameUI.LockButtons();
            battle.GameUI.taunted = true;
            AI.MakeTauntedMove(battle, unit, TauntTarget);
        }

        public override void UnInvoke(Battle battle)
        {
            battle.GameUI.taunted = false;
        }
    }
}
