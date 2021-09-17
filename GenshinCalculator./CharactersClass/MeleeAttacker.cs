using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator
{
    class MeleeAttacker : Character
    {
        // nothing is changed from the most basic character
        public MeleeAttacker(int basehp, int basetAtk, int baseDef, int baseEnergyRecharge, int baseCritChance, int baseCritDamage, int baseElemental) : base(basehp, basetAtk, baseDef, baseEnergyRecharge, baseCritChance, baseCritDamage, baseElemental) { }
    }
}
