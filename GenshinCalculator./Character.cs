using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedGenshinCalculator.ArtifactTypes;

namespace AdvancedGenshinCalculator
{
   // base class for each specific characters
    public class Character
    {
        // basic stats
        protected int baseHp;
        protected int baseAttack;
        protected int baseDefense;
        protected int baseEnergyRecharge = 100;
        protected int baseCritChance = 5;
        protected int baseCritDamage = 50;
        // bonus stats for the purpose of weapons and artifacts interactions
        public int specBonusHp;
        public int specBonusAtk;
        public int specBonusDef;
        public int specBonusER;
        public int specBonusCritChance;
        public int specBonusCritDamage;
        public int specBonusElemental;
        // the damage bonsu for elemental burst and lemental skill
        public int specBonusElementalBurst;
        public int specBonusElementalSkill;
        protected double baseElemental;
        // extra stats that are caused by weapon special passives, artifact special effects, piece bonuses and character stats effects
        public bool ultimateState = false; // whether if character has used ultimate or not, affects engulfing lighting and etc.
        protected int extraHp;
        protected int extraAtk;
        protected int extraDef;
        protected int extraEnergyRecharge;
        protected int extraCritChance;
        protected int extraCritDamage;
        protected double extraElemental;
        protected double[] elementalSkillPercentage;
        protected double[] elementalBurstPercentage;
        protected double[] normalAttackPercentage;
        protected double[] chargeAttackPercentage;
        public BaseWeapon weapon = new BaseWeapon(0,"atk1 0", "atk1 0", "atk1 0");
        protected Circlet circletArtifact = new Circlet();
        protected Feather featherArtifact = new Feather();
        protected Flower flowerArtifact = new Flower();
        protected Goblet gobletArtifact = new Goblet();
        protected Sands sandArtifact = new Sands();
        public Character(int basehp, int basetAtk, int baseDef, int baseEnergyRecharge,int baseCritChance, int baseCritDamage, double baseElemental)
        {
            this.baseHp = basehp;
            this.baseAttack = basetAtk;
            this.baseDefense = baseDef;
            this.baseEnergyRecharge = baseEnergyRecharge;
            this.baseCritChance = baseCritChance;
            this.baseCritDamage = baseCritDamage;
            this.baseElemental = baseElemental;
        }
        // apply total damage bonus to the specific skill, applied at the end of each calculation
        protected void PercentageBonus(double bonusPercentage, double[] skillDamage) 
        {
            for (int i = 0; i < skillDamage.Length; i++)
            {
                skillDamage[i] *= (1 + bonusPercentage / 100);
            }
        }
        public void ElementalSkillBonus(double bonusPercentage) // for artifacts or weapons that add to elemental skil damage percentage
        {
            PercentageBonus(bonusPercentage, this.elementalSkillPercentage);
        }
        public void ElementalBurstBonus(double bonusPercentage)
        {
            PercentageBonus(bonusPercentage, this.elementalBurstPercentage);
        }
        public void AddCirclet(Circlet circlet)
        {
            this.circletArtifact = circlet;
        }
        public void AddFeather(Feather feather)
        {
            this.featherArtifact = feather;
        }
        public void AddFlower(Flower flower)
        {
            this.flowerArtifact = flower;
        }
        public void AddGoblet(Goblet goblet)
        {
            this.gobletArtifact = goblet;
        }
        public void AddSands(Sands sands)
        {
            this.sandArtifact = sands;
        }
        public Circlet GetCirclet()
        {
            return this.circletArtifact;
        }
        public Feather GetFeather()
        {
            return this.featherArtifact;
        }
        public Flower GetFlower()
        {
            return this.flowerArtifact;
        }
        public Goblet GetGoblet()
        {
            return this.gobletArtifact;
        }
        public Sands GetSands()
        {
            return this.sandArtifact;
        }
        // set all the artifacts all at once
        public void AddAllArtifacts (Circlet circlet, Feather feather, Flower flower, Goblet goblet, Sands sands) 
        {
            this.circletArtifact = circlet;
            this.featherArtifact = feather;
            this.flowerArtifact = flower;
            this.gobletArtifact = goblet;
            this.sandArtifact = sands;
        }
        public void AddWeapon(BaseWeapon weapon)
        {
            this.weapon = weapon;
        }
        
        protected int TotalHp()
        {
            return extraHp+(int)((baseHp + circletArtifact.hpRaw + featherArtifact.hpRaw + flowerArtifact.hpRaw + gobletArtifact.hpRaw + sandArtifact.hpRaw) * (1 + (weapon.hpPercentage + circletArtifact.hpPercentage + featherArtifact.hpPercentage + flowerArtifact.hpPercentage + gobletArtifact.hpPercentage + sandArtifact.hpPercentage+specBonusHp) / 100));
        }
        public int RawHp
        {
            get { return baseHp; }
        }
        public int Hp
        {
            get { return TotalHp(); }
        }
        public int RawAtk
        {
            get { return baseAttack + weapon.baseAttack; }
        }
        protected int TotalAtk()
        {
            return extraAtk+ (int)(RawAtk * (1 + (weapon.attackPercentage + circletArtifact.attackPercentage + featherArtifact.attackPercentage + flowerArtifact.attackPercentage + gobletArtifact.attackPercentage + sandArtifact.attackPercentage+specBonusAtk) / 100)+ circletArtifact.attackRaw + featherArtifact.attackRaw + flowerArtifact.attackRaw + gobletArtifact.attackRaw + sandArtifact.attackRaw);
        }
        public int Attack
        {
            get { return TotalAtk(); }
        }
        public int RawDef
        {
            get { return baseDefense; }
        }
        protected int TotalDefense()
        {
            return extraDef+(int)((baseDefense + circletArtifact.defenseRaw + featherArtifact.defenseRaw + flowerArtifact.defenseRaw + gobletArtifact.defenseRaw + sandArtifact.defenseRaw ) * (1 + (weapon.defPercentage + circletArtifact.defensePercentage + featherArtifact.defensePercentage + flowerArtifact.defensePercentage + gobletArtifact.defensePercentage + sandArtifact.defensePercentage+specBonusDef) / 100));
        }
        public int Defense
        {
            get { return TotalDefense(); }
        }
        protected double TotalEnergyRecharge()
        {
            return extraEnergyRecharge+baseEnergyRecharge+weapon.energyRecharge + circletArtifact.energyRechargePercentage + featherArtifact.energyRechargePercentage + flowerArtifact.energyRechargePercentage + gobletArtifact.energyRechargePercentage + sandArtifact.energyRechargePercentage + specBonusER;
        }
        public double EnergyRecharge
        {
            get { return TotalEnergyRecharge(); }
        }
        protected double TotalCritChance()
        {
            return baseCritChance + weapon.critRatePercentage + circletArtifact.critRatePercentage + featherArtifact.critRatePercentage + flowerArtifact.critRatePercentage + gobletArtifact.critRatePercentage + sandArtifact.critRatePercentage+ specBonusCritChance;
        }
        public double CritChance
        {
            get { return TotalCritChance(); }
        }
        protected double TotalCritDamage()
        {
            return baseCritDamage + weapon.critDamagePercentage + circletArtifact.critDamagePercentage + featherArtifact.critDamagePercentage + flowerArtifact.critDamagePercentage + gobletArtifact.critDamagePercentage + sandArtifact.critDamagePercentage + specBonusCritDamage;
        }
        public double CritDamage 
        {
            get { return TotalCritDamage(); }
        }
        // comes in a percentage
        protected double TotalElementalDamage()
        {
            return baseElemental + weapon.elementalDamagePercentage + circletArtifact.elementalDamagePercentage + featherArtifact.elementalDamagePercentage + flowerArtifact.elementalDamagePercentage + gobletArtifact.elementalDamagePercentage + sandArtifact.elementalDamagePercentage + specBonusElemental;
        }
        public virtual double Elemental
        {
            get { return TotalElementalDamage(); }
        }
        protected double[] DamageCalc(double[] dmgPercentages, double bonusPercentage)
        {
            double[] FinalDamage = new double[dmgPercentages.Length];
            for(int i = 0; i < dmgPercentages.Length; i++)
            {
                FinalDamage[i] = Attack * dmgPercentages[i]/100 * (1+bonusPercentage/100);
            }
            return FinalDamage;
        }
        public double[] AverageCritCalculation(double[] dmg)
        {
            double[] final = dmg;
            for(int i = 0; i < dmg.Length; i++)
            {
                final[i] = final[i] * (1 + CritChance * CritDamage / 10000);
            }
            return final;
        }
        // normal normal attack with no bonus percentages or what not
        public virtual double[] NormalAttack 
        {
            get { return DamageCalc(this.normalAttackPercentage, 0); }
        }
        public virtual double[] ChargeAttack
        {
            get { return DamageCalc(this.chargeAttackPercentage, 0); }
        }
        public virtual double[] ElementalSkillDamage
        {
            get { 
                var Final = DamageCalc(this.elementalSkillPercentage, Elemental+specBonusElementalSkill); 
                for(int i = 0; i < Final.Length;i++)
                {
                    Final[i] *= (1 + (double)specBonusElementalSkill / 100);
                }
                return Final;
            }
        }
        public virtual double[] ElementalBurstDamage
        {
            get
            {
                var Final = DamageCalc(this.elementalBurstPercentage, Elemental+specBonusElementalBurst);
                for (int i = 0; i < Final.Length; i++)
                {
                    Final[i] *= (1 + (double)specBonusElementalBurst / 100);
                }
                return Final;
            }
        }
        // crit damage is outgoing damage * (1+%crit damage)
        public virtual double[] NormalCritAttack
        {
            get { return AverageCritCalculation(NormalAttack); }
        }
        public virtual double[] CritChargeAttack
        {
            get { return AverageCritCalculation(ChargeAttack); }
        }
        public virtual double[] CritElementalSkill
        {
            get { return AverageCritCalculation(ElementalSkillDamage); }
        }
        public virtual double[] CritElementalBurst
        {
            get { return AverageCritCalculation(ElementalBurstDamage); }
        }
        public double CritBurstTotal
        {
            get
            {
                double ans = 0;
                foreach (var item in CritElementalBurst)
                {
                    ans += item;
                }
                return ans;

            }
        }






    }
}
