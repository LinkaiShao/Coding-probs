using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedGenshinCalculator.ArtifactTypes
{
    public class Feather:BaseArtifact
    {
        public Feather() : base()
        {

        }
        public Feather(string name, int hp1, double hp2, int def1, double def2, double crit1, double crit2, int em, double er, int a1, double a2) : base(name, hp1, hp2, def1, def2, crit1, crit2, em,er, a1, a2)
        {
            this.attackRaw += 311;
        }
        public Feather(string subStat1, string subStat2, string subStat3, string subStat4) : base(subStat1, subStat2, subStat3, subStat4)
        {
            this.attackRaw += 311;
        }
    }
}
