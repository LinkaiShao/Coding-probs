using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator
{
    class CatalystUser:Character
    {
        public CatalystUser(int basehp, int basetAtk, int baseDef, int baseEnergyRecharge, int baseCritChance, int baseCritDamage, int baseElemental) : base(basehp, basetAtk, baseDef, baseEnergyRecharge, baseCritChance, baseCritDamage, baseElemental) { }
        public override double[] NormalAttack
        {
            get { return DamageCalc(normalAttackPercentage, 0+weapon.damageBonusPercentage+Elemental); }
        }
        public override double[] ChargeAttack
        {
            get { return DamageCalc(chargeAttackPercentage, 0+weapon.damageBonusPercentage+Elemental); }
        }
        // skills calculations remain the same
        
    }
}
