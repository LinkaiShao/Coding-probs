using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDrafting
{
    public class AllChampions
    {
        List<Champion> TopLaneChampions = new List<Champion>();
        List<Champion> JungleChampions = new List<Champion>();
        List<Champion> MidLaneChampions = new List<Champion>();
        List<Champion> AdcChampions = new List<Champion>();
        List<Champion> SupportChampions = new List<Champion>();
        List<List<Champion>> BlindPickHighTiers = new List<List<Champion>>()
        {
            { new List<Champion>()},
            { new List<Champion>()},
            { new List<Champion>()}
        };
        public enum role
        {
            Top,
            Jungle,
            Mid,
            Adc,
            Support
        }
        public Dictionary<string, Champion> allChamps;
        public AllChampions()
        {
            // read from files here
        }
        public void BlindPickTierAssign(Champion currentChampion)
        {
            var rating = currentChampion.BlindPickRating;
            if(rating>=4)
            {
                BlindPickHighTiers[6 - rating].Add(currentChampion);
            }
            return;
        }
        public List<List<Champion>> SortByMeta()
        {
            List<List<Champion>> Result = new List<List<Champion>>();
            for(int i = 0; i < 5; i++)
            {
                Result.Add(new List<Champion>());
            }
            foreach(var champ in allChamps)
            {
                Result[champ.Value.BlindPickRating].Add(champ.Value);
            }
            return Result;
        }
    }
}
