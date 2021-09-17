using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedGenshinCalculator;
using AdvancedGenshinCalculator.ArtifactTypes;

namespace AdvancedGenshinCalculator
{
    class SetToPieces
    {
        public string name;
        public int occurance;
        public SetToPieces(string name)
        {
            this.name = name;
            this.occurance = 1;
        }
    }
    // build your character with character class, artifacts class and weapon class
    // as of right now, character class has the percentages, raw stats and character stats are base character stats + base weapon stats 
    // however, some of the numbers cannot be done due to unique weapons calculations, set bonus calculations
    // remember to switch character ultimate state to true when calculating ultimate damage
    // weapon raw stats have already been added to the unit
    // the only things left over are to calculate weapon special bonus and artifact special bonus
    class Build
    {
        protected string twoPieceArtifactSet1;
        protected string twoPieceArtifactSet2;
        protected string fourPieceArtifactSet;
        protected Character unit;
        public Build(Character unit)
        {
            this.unit = unit;
            // take in consideration of artifact set and weapon
            // check for the artifacts
            string arti1Name = unit.GetCirclet().artifactName;
            string arti2Name = unit.GetFeather().artifactName;
            string arti3Name = unit.GetFlower().artifactName;
            string arti4Name = unit.GetGoblet().artifactName;
            string arti5Name = unit.GetSands().artifactName;
            // 1 comapre with 2345, 2 compare with 345, 3 compare with 45, 4 compare with 5
            string[] allNames = new string[5] { arti1Name, arti2Name, arti3Name, arti4Name, arti5Name };
            List<SetToPieces> SetTracker = new List<SetToPieces>();
            for(int i = 0; i < allNames.Length;i++)
            {
                SetTrackerHandling(SetTracker, allNames[i]);
            }
            // check for 4 piece or 2 piece sets from set tracker
            for(int i = 0;i < SetTracker.Count;i++)
            {
                if((SetTracker[i].occurance ==2 || SetTracker[i].occurance == 3))
                {
                    if(twoPieceArtifactSet1 == null)
                    {
                        twoPieceArtifactSet1 = SetTracker[i].name;
                    }
                    else
                    {
                        twoPieceArtifactSet2 = SetTracker[i].name;
                        break;
                    }
                }
                if(SetTracker[i].occurance >= 4)
                {
                    fourPieceArtifactSet = SetTracker[i].name;
                    break;
                }
            }
            // since two piece buffs are all stats based and are not special, they are treated as raw stats and thus can be done here
            Action<Character> twoPieceBuff1;
            Action<Character> twoPieceBuff2;
            if (twoPieceArtifactSet1 != null)
            {
                twoPieceBuff1 = TwoPieceArtifactsAction[twoPieceArtifactSet1];
            }
            if(twoPieceArtifactSet2 != null)
            {
                twoPieceBuff2 = TwoPieceArtifactsAction[twoPieceArtifactSet2];
            }
            
        }
        // non crit version
        // adds to the weapon special passives and the four piece artifact special passive
        public double[] CalculateFinalUltDamage ()
        {
            unit.ultimateState = true;
            Action weaponSpecialBonus1 = unit.weapon.ApplyStartingSpecialPassive();
            Action weaponSpecialBonu2 = unit.weapon.ApplyEndingSpecialPassive();
            Action<Character> fourPieceArtifactBuff = null;
            if (fourPieceArtifactSet!=null)
            {
                fourPieceArtifactBuff = FourPieceArtifactsAction[fourPieceArtifactSet];
            }
            // execute all actions
            weaponSpecialBonus1();
            weaponSpecialBonu2();
            fourPieceArtifactBuff(this.unit);
            var final = unit.ElementalBurstDamage;
            unit.ultimateState = false;
            return final;
        }
        // takes in consideration of all factors plus critical
        public double[] CalculateFinalUltDamageCrit()
        {
            return unit.AverageCritCalculation(CalculateFinalUltDamage());
        }
        private Dictionary<string, Action<Character>> TwoPieceArtifactsAction = new Dictionary<string, Action<Character>>()
        {
            { "SeveredFate", SeveredFateTwoPiece}
        };
        private Dictionary<string, Action<Character>> FourPieceArtifactsAction = new Dictionary<string, Action<Character>>()
        {
            { "SeveredFate", SeveredFateFourPiece}
        };
        private void SetTrackerHandling(List<SetToPieces> SetTracker, string artifactName)
        {
            foreach(var item in SetTracker)
            {
                if(item.name == artifactName)
                {
                    item.occurance += 1;
                    return;
                }
            }
            SetTracker.Add(new SetToPieces(artifactName));
            return;
        }
        // artifact set handling two piece 
        // simply adds 20% to total er
        private static void SeveredFateTwoPiece(Character unit)
        {
            unit.specBonusER += 20;
        }
        // four piece
        private static void SeveredFateFourPiece(Character unit)
        {
            SeveredFateTwoPiece(unit);
            if(unit.EnergyRecharge <= 300)
            {
                unit.specBonusElementalBurst += (int)(unit.EnergyRecharge * 0.25);
            }
            else
            {
                unit.specBonusElementalBurst += 75;
            }
        }
    }
}
