using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public abstract class StatusEffect
    {
        protected abstract Unit unit { get; set; }
        protected abstract int timeRemaining { get; set; }

        public abstract void DecreaseTime();
        public abstract void Invoke(Battle battle);
        public abstract void UnInvoke(Battle battle);
        public abstract int GetTimeRemaining();
        public abstract void ResetTime();

        public static List<StatusEffect> SortEffects(List<StatusEffect> effects)
        {
            List<StatusEffect> sorted = new List<StatusEffect>();
            return sorted;
        }

        public static bool HasNegativeStatusEffects(List<StatusEffect> effects)
        {
            foreach (StatusEffect effect in effects)
            {
                //the only "good" SE is haste
                if(!(effect is Haste))
                {
                    return true;
                }
            }
            return false;
        }
    }

    
}
