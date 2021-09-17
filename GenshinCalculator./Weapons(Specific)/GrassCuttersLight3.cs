using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator.Weapons_Specific_
{
    class GrassCuttersLight3:BaseWeapon
    {
        // give the base stats
        public GrassCuttersLight3() : base(608, "er 55.1", "atk2 0", "er 0") { }
        // adds attack based on the amount of eneryg recharge
        // adds energy recharge once ultimate is used
        protected override void StartingSpecialPassiveFunction()
        {
            if(weaponHolder.ultimateState == true)
            {
                weaponHolder.specBonusER += 50;
            }
            return;
        }
        protected override void EndingSpecialPassiveFunction()
        {
            weaponHolder.specBonusAtk += (int)((weaponHolder.EnergyRecharge - 100) * 0.42);
            return;
        }

    }
}
