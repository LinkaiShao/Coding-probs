using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedGenshinCalculator.ArtifactTypes;

namespace AdvancedGenshinCalculator
{
    // finds the best combination of the artifacts for a given character
    public class BestArtifactCombination
    {
        
        public Character unit;
        public List<Circlet> allCirclets;
        public List<Feather> allFeathers;
        public List<Flower> allFlowers;
        public List<Goblet> allGoblets;
        public List<Sands> allSands;
        // allow the user to enter the artifacts through console
        
        private object CreateObject(Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters);
        }
        // using generic
        private object ReadInput4Stat(Type artifactType, string artifactName)
        {
            string stat1, stat2, stat3, stat4;
            Console.WriteLine("Enter sub stats: ");
            stat1 = Console.ReadLine();
            stat2 = Console.ReadLine();
            stat3 = Console.ReadLine();
            stat4 = Console.ReadLine();
            return CreateObject(artifactType, artifactName, stat1, stat2, stat3, stat4);
          
        }
        // using generic
        private object ReadInput5Stat(Type artifactType, string artifactName) 
        {
            Console.WriteLine("Enter main stat: ");
            BaseArtifact x = new BaseArtifact();
            x.artifactName = artifactName;
            if (artifactType == typeof(Circlet))
            {
                x = new Circlet();
            }
            else if(artifactType == typeof(Goblet))
            {
                x = new Goblet();
            }
            else
            {
                x = new Sands();
            }
            foreach (var item in x.allTypeStats())
            {
                Console.Write($"{item}, ");
            }
            string mainStat = Console.ReadLine();
            string stat1, stat2, stat3, stat4;
            Console.WriteLine("Enter sub stats: ");
            stat1 = Console.ReadLine();
            stat2 = Console.ReadLine();
            stat3 = Console.ReadLine();
            stat4 = Console.ReadLine();
            object final = CreateObject(artifactType, mainStat, stat1, stat2, stat3, stat4);
            
            return final;
        }
        public void EnterArtifacts()
        {
            Console.WriteLine("Enter Circlet Artifacts");

            string ans = "";
            string artifactClass;
            while(ans != "done")
            {
                Console.WriteLine("Enter Artifact Name: ");
                artifactClass = Console.ReadLine();
                allCirclets.Add((Circlet)ReadInput5Stat(typeof(Circlet), artifactClass));
                ans = Console.ReadLine();
            }
            Console.WriteLine("Enter Feather Artifacts");
            while(ans != "done")
            {
                Console.WriteLine("Enter Artifact Name: ");
                artifactClass = Console.ReadLine();
                allFeathers.Add((Feather)ReadInput4Stat(typeof(Feather),artifactClass));
                ans = Console.ReadLine();
            }
            Console.WriteLine("Enter Flower Artifacts");
            while (ans != "done")
            {
                Console.WriteLine("Enter Artifact Name: ");
                artifactClass = Console.ReadLine();
                allFlowers.Add((Flower)ReadInput4Stat(typeof(Flower),artifactClass));
                ans = Console.ReadLine();
            }
            Console.WriteLine("Enter Goblet Artifacts");
            while (ans != "done")
            {
                Console.WriteLine("Enter Artifact Name: ");
                artifactClass = Console.ReadLine();
                allGoblets.Add((Goblet)ReadInput5Stat(typeof(Goblet),artifactClass));
                ans = Console.ReadLine();
            }
            Console.WriteLine("Enter Sands Artifacts");
            while (ans != "done")
            {
                Console.WriteLine("Enter Artifact Name: ");
                artifactClass = Console.ReadLine();
                allSands.Add((Sands)ReadInput5Stat(typeof(Sands),artifactClass));
                ans = Console.ReadLine();
            }




        }
        public void FindBestCombination()
        {
            double maxDps = 0;
            int[] bestArtifactsPlaces= new int[5];
            unit.AddAllArtifacts(allCirclets[0], allFeathers[0], allFlowers[0], allGoblets[0], allSands[0]);
            for(int i = 0; i < allCirclets.Count;i++)
            {
                unit.AddCirclet(allCirclets[i]);
                for(int j = 0; j < allFeathers.Count; j++)
                {
                    unit.AddFeather(allFeathers[j]);
                    for (int k = 0; k < allFlowers.Count;k++)
                    {
                        unit.AddFlower(allFlowers[k]);
                        for(int l = 0; l < allGoblets.Count; l++)
                        {
                            unit.AddGoblet(allGoblets[l]);
                            for(int m = 0; m < allSands.Count; m++)
                            {
                                unit.AddSands(allSands[m]);
                                // calculate dps
                                double currentDps = unit.CritBurstTotal;
                                if (currentDps > maxDps)
                                {
                                    maxDps = currentDps;
                                    bestArtifactsPlaces[0] = i;
                                    bestArtifactsPlaces[1] = j;
                                    bestArtifactsPlaces[2] = k;
                                    bestArtifactsPlaces[3] = l;
                                    bestArtifactsPlaces[4] = m;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Highest final damage is {maxDps}");
            Console.WriteLine($"Circlet Artifact: {bestArtifactsPlaces[0]+1}");
            Console.WriteLine($"Feather Artifact: {bestArtifactsPlaces[1]+1}");
            Console.WriteLine($"Flower Artifact: {bestArtifactsPlaces[2]+1}");
            Console.WriteLine($"Goblet Artifact: {bestArtifactsPlaces[3]+1}");
            Console.WriteLine($"Sands Artifact: {bestArtifactsPlaces[4]+1}");

        }
        
    }

}
