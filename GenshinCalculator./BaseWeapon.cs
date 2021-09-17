using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdvancedGenshinCalculator
{
    
    // base attack and individual substat buffs
    public class BaseWeapon
    {
        protected Dictionary<string, Action<double, BaseWeapon>> subStatDict = new Dictionary<string, Action<double, BaseWeapon>>()
        {

            { "hp2",(x,y) => y.hpPercentage += x },
            { "atk1",(x,y) => y.baseAttack +=(int)x },
            { "atk2",(x,y) => y.attackPercentage +=x },
            { "def2",(x,y) => y.defPercentage +=x },
            { "em",(x,y) => y.elementalMastery +=(int)x },
            { "ed",(x,y) => y.elementalDamagePercentage +=x },
            { "er",(x,y) => y.energyRecharge +=x },
            { "critRate",(x,y) => y.critRatePercentage +=x },
            { "critDamage",(x,y) => y.critDamagePercentage +=(int)x },
            { "bonusDamage", (x,y)=>y.damageBonusPercentage += x}
        };
        public int baseAttack;
        public double attackPercentage;
        public double energyRecharge;
        public double damageBonusPercentage;
        public double critRatePercentage;
        public double critDamagePercentage;
        public double elementalDamagePercentage;
        public int elementalMastery;
        public double hpPercentage;
        public double defPercentage;
        protected Character weaponHolder;
        public BaseWeapon(int baseAttack, string secondaryStat, string thirdSubStat, string fourthSubStat)
        {
            this.baseAttack = baseAttack;
            string subStat2Name = secondaryStat.Split(' ')[0];
            double subStat2Num = Convert.ToDouble(secondaryStat.Split(' ')[1]);
            string subStat3Name = thirdSubStat.Split(' ')[0];
            double subStat3Num = Convert.ToDouble(thirdSubStat.Split(' ')[1]);
            if(!(fourthSubStat == null || fourthSubStat.Length == 0))
            {
                string substat4Name = fourthSubStat.Split(' ')[0];
                double substat4Num = Convert.ToDouble(fourthSubStat.Split(' ')[1]);
                subStatDict[substat4Name](substat4Num, this);
            }
            subStatDict[subStat2Name](subStat2Num, this);
            subStatDict[subStat3Name](subStat3Num, this);
        }
        // return void function that calculates special passive for the weapon
        public Action ApplyStartingSpecialPassive()
        {
            return StartingSpecialPassiveFunction;
        }
        public Action ApplyEndingSpecialPassive()
        {
            return EndingSpecialPassiveFunction;
        }
        protected virtual void StartingSpecialPassiveFunction() { } // special passive function that special weapons use based on their special passive, can be applied at anytime
        protected virtual void EndingSpecialPassiveFunction() { } // special passive function that special weapons use based on their special passive, only applies after everything, including artifact special bonuses are added


    }
}
