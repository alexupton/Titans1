﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public abstract class StatusEffect
    {
        protected abstract Unit unit;
        protected abstract int timeRemaining;

        public abstract void DecreaseTime();
        public abstract void Invoke(Battle battle);
        public abstract void UnInvoke(Battle battle);

        public static List<StatusEffect> SortEffects(List<StatusEffect> effects)
        {
            List<StatusEffect> sorted = new List<StatusEffect>();
            return sorted;
        }
    }

    
}