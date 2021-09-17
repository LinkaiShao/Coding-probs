using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator.ArtifactTypes
{
    public class Sands:BaseArtifact
    {
        private void TypeStats(string type)
        {
            switch (type)
            {
                case "hp":
                    this.hpPercentage += 46.6;
                    break;
                case "atk":
                    this.attackPercentage += 46.6;
                    break;
                case "def":
                    this.defensePercentage += 58.3;
                    break;
                case "em":
                    this.elementMasteryRaw += 187;
                    break;
                case "er":
                    this.energyRechargePercentage += 51.8;
                    break;

            }
        }
        public override string[] allTypeStats()
        {
            return new string[5] { "hp", "atk", "def", "em", "er" };
        }
        public Sands() : base()
        {

        }
        public Sands(string name, int hp1, double hp2, int def1, double def2, double crit1, double crit2, int em,double er, int a1, double a2,string type) : base(name, hp1, hp2, def1, def2, crit1, crit2, em, er, a1, a2)
        {
            TypeStats(type);   
        }
        public Sands(string type, string subStat1, string subStat2, string subStat3, string subStat4) : base(subStat1, subStat2, subStat3, subStat4)
        {
            TypeStats(type);
        }
        public Sands(string name, string type, string subStat1, string subStat2, string subStat3, string subStat4) : base(name, subStat1, subStat2, subStat3, subStat4)
        {
            TypeStats(type);
        }
    }
}
