using System;
using AdvancedGenshinCalculator.ArtifactTypes;
using AdvancedGenshinCalculator.MeleeCharacters;
using AdvancedGenshinCalculator.Weapons_Specific_;
using System.Collections;
using System.IO;
using System.Collections.Generic;
namespace AdvancedGenshinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BestArtifactCombination b = new BestArtifactCombination();
            Baal myBaal = new Baal();
            myBaal.AddWeapon(new GrassCuttersLight3());
            Circlet circlet1 = new Circlet("critRate", "critDamage 7.0", "er 10.4", "def2 24.1", "atk2 5.8");
            Circlet circlet2 = new Circlet("critDamage", "def2 12.4", "atk2 18.7", "er 6.5", "em 19");
            Circlet circlet3 = new Circlet("critRate", "er 11.0", "atk1 35", "em 21", "hp1 687");
            List<Circlet> allCirclets = new List<Circlet> { circlet1, circlet2, circlet3 };
            Feather feather1 = new Feather("critRate 9.3", "critDamage 6.2", "er 16.8", "def2 11.7");
            Feather feather2 = new Feather("critDamage 14.0", "def1 42", "er 11.0", "def2 16.8");
            Feather feather3 = new Feather("em 35", "hp2 4.1", "critDamage 15.5", "atk2 20.4");
            Feather feather4 = new Feather("em 16", "atk2 15.2", "er 17.5", "hp2 5.3");
            List<Feather> allFeathers = new List<Feather> { feather1, feather2, feather3, feather4 };
            Flower flower1 = new Flower("er 4.5", "em 40", "hp2 11.7", "critDamage 24.1");
            Flower flower2 = new Flower("er 18.1", "atk1 33", "hp2 4.1", "critDamage 14.8");
            Flower flower3 = new Flower("er 12.3", "def2 7.3", "hp2 4.7", "critRate 13.2");
            List<Flower> allFlowers = new List<Flower> { flower1, flower2, flower3 };
            Goblet goblet1 = new Goblet("ed", "atk2 8.7", "er 5.2", "def2 10.2", "critDamage 21.8");
            Goblet goblet2 = new Goblet("ed", "hp2 5.3", "atk1 31", "critDamage 13.2", "atk2 22.2");
            List<Goblet> allGoblets = new List<Goblet> { goblet1, goblet2 };
            Sands sands1 = new Sands("er", "hp2 9.9", "critRate 6.6", "em 19", "critDamage 27.2");
            List<Sands> allSands = new List<Sands> { sands1 };
            b.unit = myBaal;
            b.allCirclets = allCirclets;
            b.allFeathers = allFeathers;
            b.allFlowers = allFlowers;
            b.allGoblets = allGoblets;
            b.allSands = allSands;
            b.FindBestCombination();
            Baal baal1 = new Baal();
            baal1.AddWeapon(new GrassCuttersLight3());
            baal1.AddAllArtifacts(circlet1, feather3, flower3, goblet2, sands1);
            Baal baal2 = new Baal();
            baal2.AddWeapon(new GrassCuttersLight3());
            baal2.AddAllArtifacts(circlet3, feather1, flower3, goblet1, sands1);
            



        }
    }
}
