using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDrafting
{
    public static class Drafting
    {
        public static AllChampions allChamps;
        private static List<Champion> PickedorBannedChampions;
        private static List<Champion> BlueTeamDraft;
        private static List<Champion> RedTeamDraft;
        // when process is above 0.9, it means that the lane is no longer considered in the drafting recommendation list
        private static double topLaneDraftProgress;
        private static double jungleLaneDraftProgress;
        private static double midLaneDraftProgress;
        private static double adcLaneDraftProgress;
        private static double supportLaneDraftProgress;
        private static List<string> AllBans;
        
        private static Dictionary<int, string> TierListNotation = new Dictionary<int, string>()
        {
            {12,"S+" },
            { 11,"S"},
            { 10,"A+"},
            { 9,"A"},
            { 8,"B+"},
            { 7,"B"},
            { 6,"C+"},
            {5,"C" },
            { 4,"D+"},
            { 3,"D"},
            { 2,"E+"},
            { 1,"E"},
            { 0,"F"},
        };
        private static Dictionary<string, AllChampions.role> stringToRole = new Dictionary<string, AllChampions.role>()
        {
            { "Top", AllChampions.role.Top},
            { "Jungle", AllChampions.role.Jungle},
            { "Mid", AllChampions.role.Mid},
            { "Adc", AllChampions.role.Adc},
            { "Support", AllChampions.role.Support}
        };
        
        /// <summary>
        /// Check whether if the champion was already drafted
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool AlreadyDrafted(string input)
        {
            foreach(var item in BlueTeamDraft)
            {
                if(input == item.championName)
                {
                    return true;
                }
            }
            foreach(var item in RedTeamDraft)
            {
                if (input == item.championName)
                {
                    return true;
                }
            }
            foreach(var item in AllBans)
            {
                if(input == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static void StartDrafting()
        {
            string teamSide;
            Console.WriteLine("Blue side or red side?");
            teamSide = Console.ReadLine();
            if(teamSide == "blue")
            {

            }
            else
            {

            }
        }
        /// <summary>
        /// Tells you about all the ops
        /// </summary>
        private static void PickBansStage1()
        {
            FirstPicks();
            for(int i = 0; i < 2; i++)
            {
                Console.WriteLine("Blue ban: ");
                var BlueTeamBan = Console.ReadLine();
                Console.WriteLine("Red ban: ");
                var RedTeamBan = Console.ReadLine();
                AllBans.Add(BlueTeamBan);
                AllBans.Add(RedTeamBan);
            }
            return;
        }
        /// <summary>
        /// Print the meta picks by their tiers
        /// </summary>
        private static void FirstPicks()
        {
            var metas = allChamps.SortByMeta();
            for (int i = 12; i > 0; i--)
            {
                Console.WriteLine($"Tier {i} Meta Champs:");
                Console.Write($" {metas[i]},");
            }
            Console.WriteLine();
            return;
        }
        private static void PrintPicksTier(List<List<Champion>> input)
        {
            for(int i = 0; i < input.Count;i++)
            {
                Console.WriteLine($"{TierListNotation[i]} tier: \n");
                for(int j= 0; j < input[i].Count; j++)
                {
                    Console.Write($"{input[i][j].championName}  ");
                }
            }
            Console.WriteLine();
        }
        private static void PickAChampion(List<Champion> picker)
        {
            Console.WriteLine("What is the pick?");
            var ans = Console.ReadLine();
            picker.Add(allChamps.allChamps[ans]);
            return;
        }
        /// <summary>
        /// Shows the role, if role is completed, then don't show the role, this function does nothing
        /// </summary>
        private static void ShowRole(string laneName, List<Champion> yourTeam, Dictionary<AllChampions.role, List<List<Champion>>> RoleToTable)
        {
            if(!RoleComplete(yourTeam, stringToRole[laneName]))
            {
                Console.WriteLine($"{laneName} Champs:");
                PrintPicksTier(RoleToTable[stringToRole[laneName]]);
            }
            return;
        }
        /// <summary>
        /// Check whether if the role has been completed or not
        /// </summary>
        /// <param name="yourPicks"></param>
        /// <param name="roleToCheck"></param>
        private static bool RoleComplete(List<Champion> yourPicks, AllChampions.role roleToCheck)
        {
            switch(roleToCheck)
            {
                case AllChampions.role.Top:
                    if (topLaneDraftProgress >= 0.9)
                    {
                        return true;
                    }
                    return false;
                case AllChampions.role.Jungle:
                    if (jungleLaneDraftProgress >= 0.9)
                    {
                        return true;
                    }
                    return false;
                case AllChampions.role.Mid:
                    if (midLaneDraftProgress >= 0.9)
                    {
                        return true;
                    }
                    return false;
                case AllChampions.role.Adc:
                    if (adcLaneDraftProgress >= 0.9)
                    {
                        return true;
                    }
                    return false;
                case AllChampions.role.Support:
                    if (supportLaneDraftProgress >= 0.9)
                    {
                        return true;
                    }
                    return false;

            }
            return false;
        }
        /// <summary>
        /// Gives you the list of all champions available and put them into tier lists, applies for one pick for your team
        /// </summary>
        /// <param name="yourTeam"></param>
        /// <param name="enemyTeam"></param>
        private static void PickForSpecific(List<Champion> yourTeam, List<Champion> enemyTeam)
        {
            List<List<Champion>> TopTierTable = new List<List<Champion>>();
            List<List<Champion>> JungleTierTable = new List<List<Champion>>();
            List<List<Champion>> MidTierTable = new List<List<Champion>>();
            List<List<Champion>> AdcTierTable = new List<List<Champion>>();
            List<List<Champion>> SupportTierTable = new List<List<Champion>>();
            Dictionary<AllChampions.role, List<List<Champion>>> RoleToTable = new Dictionary<AllChampions.role, List<List<Champion>>>()
            {
                { AllChampions.role.Top,TopTierTable},
                { AllChampions.role.Jungle,JungleTierTable},
                { AllChampions.role.Mid,MidTierTable},
                { AllChampions.role.Adc,AdcTierTable},
                { AllChampions.role.Support,SupportTierTable}
            };
            List<List<Champion>> DraftTierTable = new List<List<Champion>>(); // tier table for all champs
            for (int i = 0; i < 12; i++)
            {
                DraftTierTable.Add(new List<Champion>());
            }
            int totalScore = 0;
            foreach (var champion in allChamps.allChamps) // make a list for every champ
            {
                if (!AlreadyDrafted(champion.Value.championName))
                {
                    int counter = 0;
                    totalScore = 0;
                    // calculate scores for every champ according to picks on each side
                    foreach (var items in yourTeam)
                    {
                        totalScore += champion.Value.FindSynergyScore(items);
                        counter++;
                    }
                    foreach (var items in enemyTeam)
                    {
                        totalScore += champion.Value.FindMatchUpScore(items);
                        counter++;
                    }
                    var finalScore = totalScore*2 / counter; // because we have 12 levels
                    //round down and add
                    DraftTierTable[finalScore].Add(champion.Value);
                    // add to the tier table for roles
                    foreach(var item in champion.Value.AvailableRoles)
                    {
                        RoleToTable[item][finalScore].Add(champion.Value);
                    }
                }
                
            }
            // need a way to know which role has been picked already
            Console.WriteLine("All champs:");
            string[] allRoles = new string[5] { "Top", "Jungle", "Mid", "Adc", "Support" };
            PrintPicksTier(DraftTierTable);
            foreach(var item in allRoles)
            {
                ShowRole(item, yourTeam, RoleToTable);
            }
            PickAChampion(yourTeam);
            return;
        }
        private static void PickForATeam(string team) 
        {
            
            
            
            if (team == "blue" || team == "Blue")
            {

                PickForSpecific(BlueTeamDraft, RedTeamDraft);
                
            }
            else
            {
                PickForSpecific(RedTeamDraft, BlueTeamDraft);
            }
            return;
        }
        private static void PicksStage1Blue()
        {

        }
        private static void PickStage1Red()
        {

        }
        // only show your available picks for blue team
        private static void BlueSideDrafting()
        {
            PickBansStage1();
            PicksStage1Blue();
        }
        // only show available picks for red team
        private static void RedSideDrafting()
        {
            PickBansStage1();
        }
    }
}
