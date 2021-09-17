using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator.MeleeCharacters
{
    class Baal:MeleeAttacker
    {
        // constructor for base stats
        public Baal() : base(12907, 337, 789, 152, 5, 50, 0)
        {
            this.normalAttackPercentage = new double[5] { 99.62,99.83,125.33,72.82*2,164.44};
            this.chargeAttackPercentage = new double[1] { 250.23 };
            this.elementalSkillPercentage = new double[2] { 239.05,89.25};
            this.elementalBurstPercentage = new double[7] { 851.7, 20, 93.54, 91.91, 112.54 + 64.58, 64.77, 154.61 + 128.8 };
            this.baseElemental = (EnergyRecharge - 100) * 0.4; // base electro damage is amplified by her energy recharge
        }
        public override double Elemental => base.Elemental + (EnergyRecharge-100)*0.4;
        // calculation for elemental burst
        public override double[] ElementalBurstDamage
        {
            get 
            {
                double[] final = base.ElementalBurstDamage;
                // first stat, bonus changes only based on the ammount of energy * 25%
                if (EnergyRecharge < 300)
                {
                    final[0] += final[0] * (EnergyRecharge * 0.25 / 100);
                }
                else
                {
                    final[0] += final[0] * 0.75;
                }
                
                // each of these is amplified due to the artifact, first stat is only before the passive energy was added to the character due to weapon
                // 40 energy is added
                // 40 energy means attack is added by 0.42*40, which is 16.8%
                // 40 energy also means that elemental damage is increased by 0.4*40 which is 16%
                // have to use custom calculating method for the purpose of real numbers
                int totalAtk = (int)(TotalAtk() * (1 + 0.168));
                for(int i = 1; i < elementalBurstPercentage.Length; i++)
                {
                    if (EnergyRecharge + 40 < 300)
                    {
                        // totalAttack *(1+energyrecharge*0.25/100+totalelemental)
                        final[i] = (double)totalAtk * elementalBurstPercentage[i] / 100 * (1 + (baseElemental + 16) / 100 + EnergyRecharge * 0.25 / 100);
                    }
                    else
                    {
                        final[i] = (double)totalAtk * elementalBurstPercentage[i] / 100 * (1 + (baseElemental + 16) / 100 + 0.75);
                    }
                    
                }
                return final;
            }
        }

        // normal attack and charge attacks are the same
    }
}
