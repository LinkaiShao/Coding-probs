using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LeagueDrafting
{
   
    public class Champion
    {
        
        public string championName;
        public List<AllChampions.role> AvailableRoles;
        public int BlindPickRating;
        List<List<Champion>> MatchUps; // represents good to bad match ups, first row of list represents tier 0, worst matchups, all the way up to 5
        List<List<Champion>> ChampionSynergies; // represents good to bad team comp, first row is really bad synergy, all the way up to 5
        public Champion(string name, List<AllChampions.role> Roles, int BlindPickRating, List<List<Champion>> Matchups1, List<List<Champion>> Synergies1)
        {
            this.championName = name;
            this.AvailableRoles = Roles;
            this.BlindPickRating = BlindPickRating;
            this.MatchUps = Matchups1;
            this.ChampionSynergies = Synergies1;
        }
        private int FindScore(Champion alreadyPicked, List<List<Champion>> champScoreList)
        {
            for (int i = 0; i < champScoreList.Count; i++)
            {
                for (int j = 0; j < champScoreList[i].Count; j++)
                {
                    if (alreadyPicked.championName == MatchUps[i][j].championName)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// Find The score for this chamion and the one already picked
        /// </summary>
        /// <param name="pickToCheck"></param>
        /// <param name="alreadyPicked"></param>
        /// <returns></returns>
        public int FindMatchUpScore(Champion alreadyPicked)
        {
            return FindScore(alreadyPicked, this.MatchUps);
        }
        public int FindSynergyScore(Champion alreadyPicked)
        {
            return FindScore(alreadyPicked, this.ChampionSynergies);
        }

    }
}
