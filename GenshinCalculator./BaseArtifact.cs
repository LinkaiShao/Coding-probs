using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator
{
    /*
     *list of stats for the artifacts remain the same, the type of artifact will be done by interface
     *hp raw
     *hp percentage
     *attack raw
     *attack percentage
     *defense raw
     *defense percentage
     *crit rate percentage
     *crit damage percentage
     *elemental mastery raw
     *elemental damage percentage
     *energy recharge percentage
     */
    public class BaseArtifact
    {
        // delegate for easier adding substats to the artifacts when creating
        //ex: new feather("critDamage 20"...)
        protected Dictionary<string, Action<double,BaseArtifact>> subStatDict = new Dictionary<string, Action<double,BaseArtifact>>()
        {
            { "hp1",(x,y) => y.hpRaw +=(int)x },
            { "hp2",(x,y) => y.hpPercentage += x },
            { "atk1",(x,y) => y.attackRaw +=(int)x },
            { "atk2",(x,y) => y.attackPercentage +=x },
            { "def1",(x,y) => y.defenseRaw +=(int)x },
            { "def2",(x,y) => y.defensePercentage +=x },
            { "em",(x,y) => y.elementMasteryRaw +=(int)x },
            { "ed",(x,y) => y.elementalDamagePercentage +=x },
            { "er",(x,y) => y.energyRechargePercentage +=x },
            { "critRate",(x,y) => y.critRatePercentage +=x },
            { "critDamage",(x,y) => y.critDamagePercentage +=(int)x }
        };
        public string artifactName; // name of the artifact, for example fire 1
        public int hpRaw=0;
        public double hpPercentage=0;
        public int defenseRaw=0;
        public double defensePercentage=0;
        public double critRatePercentage=0;
        public double critDamagePercentage=0;
        public int elementMasteryRaw=0;
        public double elementalDamagePercentage=0;
        public double energyRechargePercentage=0;
        public int attackRaw=0;
        public double attackPercentage=0;
        public virtual string[] allTypeStats()
        {
            return null;
        }
        //constructor
        public BaseArtifact()
        {

        }
        public BaseArtifact(string name, int hp1, double hp2, int def1, double def2, double crit1, double crit2, int em, double er,int a1, double a2)
        {
            this.hpRaw = hp1;
            this.hpPercentage = hp2;
            this.defenseRaw = def1;
            this.defensePercentage = def2;
            this.critRatePercentage = crit1;
            this.critDamagePercentage = crit2;
            this.elementMasteryRaw = em;
            this.energyRechargePercentage = er;
            this.attackRaw = a1;
            this.attackPercentage = a2;
        }
        public BaseArtifact(string subStat1, string subStat2, string subStat3, string subStat4)
        {
            // parse each item based on the space in between
            string subStat1Name = subStat1.Split(' ')[0];
            double subStat1Num = Convert.ToDouble(subStat1.Split(' ')[1]);
            string subStat2Name = subStat2.Split(' ')[0];
            double subStat2Num = Convert.ToDouble(subStat2.Split(' ')[1]);
            string subStat3Name = subStat3.Split(' ')[0];
            double subStat3Num = Convert.ToDouble(subStat3.Split(' ')[1]);
            string subStat4Name = subStat4.Split(' ')[0];
            double subStat4Num = Convert.ToDouble(subStat4.Split(' ')[1]);
            subStatDict[subStat1Name](subStat1Num, this);
            subStatDict[subStat2Name](subStat2Num, this);
            subStatDict[subStat3Name](subStat3Num, this);
            subStatDict[subStat4Name](subStat4Num, this);
        }
        public BaseArtifact(string artifactName, string subStat1, string subStat2, string subStat3, string subStat4) : this(subStat1, subStat2, subStat3, subStat4)
        {
            
            this.artifactName = artifactName;
            
        }
    }
}
