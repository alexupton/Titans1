﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    
    public static class ManaEvaluator
    {
        //determine if a given unit has enough mana to cast a special
        //IF MANA REQUIRED CHANGES, YOU MUST CHANGE THIS METHOD
        //
        //
        //
        //
        //...MUST
        public static bool SpecialAllowed(Unit unit, int specialNumber)
        {
            bool allowed = false;
            if (specialNumber == 1)
            {
                if (unit is Soldier)
                {
                    if (unit.MP >= 5)
                        return true;
                    else
                        return false;
                }
                else if (unit is Defender)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
                else if (unit is Ranger)
                {
                    if (unit.MP >= 15)
                        return true;
                    else
                        return false;
                }
                else if (unit is Mage)
                {
                    if (unit.MP >= 40)
                        return true;
                    else
                        return false;
                }
                else if (unit is Spearman)
                {
                    if (unit.MP >= 4)
                        return true;
                    else
                        return false;
                }
            }
            else if (specialNumber == 2)
            {
                if (unit is Soldier)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
                else if (unit is Defender)
                {
                    if (unit.MP >= 5)
                        return true;
                    else
                        return false;
                }
                else if (unit is Ranger)
                {
                    if (unit.MP >= 15)
                        return true;
                    else
                        return false;
                }
                else if (unit is Mage)
                {
                    if (unit.MP >= 20)
                        return true;
                    else
                        return false;
                }
                else if (unit is Spearman)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
            }
            else if (specialNumber == 3)
            {
                if (unit is Soldier)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
                else if (unit is Defender)
                {
                    if (unit.MP >= 15)
                        return true;
                    else
                        return false;
                }
                else if (unit is Ranger)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
                else if (unit is Mage)
                {
                    if (unit.MP >= 15)
                        return true;
                    else
                        return false;
                }
                else if (unit is Spearman)
                {
                    if (unit.MP >= 10)
                        return true;
                    else
                        return false;
                }
            }
            else if (specialNumber == 4)
            {
                if (unit.MP >= 20)
                    return true;
                else
                    return false;

            }
            else if (specialNumber == 5)
            {
                if (unit.MP >= 20)
                    return true;
                else
                    return false;

            }
            else if (specialNumber == 6)
            {
                if (unit.MP >= 20)
                    return true;
                else
                    return false;

            }
            
            

            return allowed;
        }
        
    }
}
