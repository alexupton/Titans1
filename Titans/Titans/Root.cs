using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Root : StatusEffect
    {
        protected override Unit unit { get; set; }
        protected override int timeRemaining { get; set; }
        public Root(Unit target)
        {
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
            battle.GameUI.rooted = true;
            battle.GameUI.move = battle.GameUI.move_grey;
        }

        public override void UnInvoke(Battle battle)
        {
            battle.GameUI.rooted = false;
            battle.GameUI.move = battle.GameUI.movetrue;
        }
    }
}
