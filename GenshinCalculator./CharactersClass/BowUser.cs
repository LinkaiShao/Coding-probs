using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator
{
    class BowUser:Character
    {
        public BowUser(int basehp, int basetAtk, int baseDef, int baseEnergyRecharge, int baseCritChance, int baseCritDamage, int baseElemental) : base(basehp, basetAtk, baseDef, baseEnergyRecharge, baseCritChance, baseCritDamage, baseElemental) { }
        public override double[] ChargeAttack
        {
            get { return DamageCalc(chargeAttackPercentage, 0+weapon.damageBonusPercentage+Elemental); }
        }
        // everything else remains the same
    }
}
