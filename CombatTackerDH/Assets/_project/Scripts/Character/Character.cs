using System.Collections.Generic;

namespace CombarTracker
{
    public class Character : IName
    {
        private List<MechImplant> _implants = new List<MechImplant>();
        private List<Equipment> _equipments = new List<Equipment>();
        private List<Trait> _traits = new List<Trait>();
        private List<Trait> _skills = new List<Trait>();
        private List<Trait> _talents = new List<Trait>();
        private List<Trait> _psyPowers = new List<Trait>();

        public Character(SaveLoadCharacter saveLoad, List<MechImplant> implants, List<Equipment> equipments, List<Trait> traits, List<Trait> skills, List<Trait> talents, List<Trait> psyPowers)
        {
            _implants.AddRange(implants);
            _equipments.AddRange(equipments);
            _traits.AddRange(traits);
            _skills.AddRange(skills);
            _talents.AddRange(talents);
            _psyPowers.AddRange(psyPowers);

            Name = saveLoad.name;
            ShowingName = saveLoad.showingName;
            Wounds = saveLoad.wounds;
            WeaponSkill = saveLoad.weaponSkill;
            BallisticSkill = saveLoad.ballisticSkill;
            Strength = saveLoad.strength;
            Toughness = saveLoad.toughness;
            Agility = saveLoad.agility;
            Intelligence = saveLoad.intelligence;
            Perception = saveLoad.perception;
            Willpower = saveLoad.willpower;
            Fellowship = saveLoad.fellowship;
            Influence = saveLoad.influence;

            WeaponSkillSuper = saveLoad.weaponSkillSuper;
            BallisticSkillSuper = saveLoad.ballisticSkillSuper;
            StrengthSuper = saveLoad.strengthSuper;
            ToughnessSuper = saveLoad.toughnessSuper;
            AgilitySuper = saveLoad.agilitySuper;
            IntelligenceSuper = saveLoad.intelligenceSuper;
            PerceptionSuper = saveLoad.perceptionSuper;
            WillpowerSuper = saveLoad.willpowerSuper;
            FellowshipSuper = saveLoad.fellowshipSuper;
            InfluenceSuper = saveLoad.influenceSuper;

            Half = saveLoad.half;
            Full = saveLoad.full;
            Natisk = saveLoad.natisk;
            Run = saveLoad.run;

            ArmorHead = saveLoad.armorHead;
            ArmorAblHead = saveLoad.armorAblHead;
            ArmorTotalHead = saveLoad.armorTotalHead;

            ArmorRightHand = saveLoad.armorRightHand;
            ArmorAblRightHand = saveLoad.armorAblRightHand;
            ArmorTotalRightHand = saveLoad.armorTotalRightHand;

            ArmorLeftHand = saveLoad.armorLeftHand;
            ArmorAblLeftHand = saveLoad.armorAblLeftHand;
            ArmorTotalLeftHand = saveLoad.armorTotalLeftHand;

            ArmorBody = saveLoad.armorBody;
            ArmorAblBody = saveLoad.armorAblBody;
            ArmorTotalBody = saveLoad.armorTotalBody;

            ArmorRightLeg = saveLoad.armorRightLeg;
            ArmorAblRightLeg = saveLoad.armorAblRightLeg;
            ArmorTotalRightLeg = saveLoad.armorTotalRightLeg;

            ArmorLeftLeg = saveLoad.armorLeftLeg;
            ArmorAblLeftLeg = saveLoad.armorAblLeftLeg;
            ArmorTotalLeftLeg = saveLoad.armorTotalLeftLeg;

            IsTurned = saveLoad.isTurned;

            ShelterHead = saveLoad.shelterHead;
            ShelterBody = saveLoad.shelterBody;
            ShelterLeftHand = saveLoad.shelterLeftHand;
            ShelterLeftLeg = saveLoad.shelterLeftHand;
            ShelterRightHand = saveLoad.shelterRightHand;
            ShelterRightLeg = saveLoad.shelterRightLeg;
            ShelterPoint = saveLoad.shelterPoint;
        }

        public Character(Character character)
        {
            Name = character.Name;
            ShowingName = character.ShowingName;
            Wounds = character.Wounds;
            WeaponSkill = character.WeaponSkill;
            BallisticSkill = character.BallisticSkill;
            Strength = character.Strength;
            Toughness = character.Toughness;
            Agility = character.Agility;
            Intelligence = character.Intelligence;
            Perception = character.Perception;
            Willpower = character.Willpower;
            Fellowship = character.Fellowship;
            Influence = character.Influence;

            WeaponSkillSuper = character.WeaponSkillSuper;
            BallisticSkillSuper = character.BallisticSkillSuper;
            StrengthSuper = character.StrengthSuper;
            ToughnessSuper = character.ToughnessSuper;
            AgilitySuper = character.AgilitySuper;
            IntelligenceSuper = character.IntelligenceSuper;
            PerceptionSuper = character.PerceptionSuper;
            WillpowerSuper = character.WillpowerSuper;
            FellowshipSuper = character.FellowshipSuper;
            InfluenceSuper = character.InfluenceSuper;

            Half = character.Half;
            Full = character.Full;
            Natisk = character.Natisk;
            Run = character.Run;

            ArmorHead = character.ArmorHead;
            ArmorAblHead = character.ArmorAblHead;
            ArmorTotalHead = character.ArmorTotalHead;

            ArmorRightHand = character.ArmorRightHand;
            ArmorAblRightHand = character.ArmorAblRightHand;
            ArmorTotalRightHand = character.ArmorTotalRightHand;

            ArmorLeftHand = character.ArmorLeftHand;
            ArmorAblLeftHand = character.ArmorAblLeftHand;
            ArmorTotalLeftHand = character.ArmorTotalLeftHand;

            ArmorBody = character.ArmorBody;
            ArmorAblBody = character.ArmorAblBody;
            ArmorTotalBody = character.ArmorTotalBody;

            ArmorRightLeg = character.ArmorRightLeg;
            ArmorAblRightLeg = character.ArmorAblRightLeg;
            ArmorTotalRightLeg = character.ArmorTotalRightLeg;

            ArmorLeftLeg = character.ArmorLeftLeg;
            ArmorAblLeftLeg = character.ArmorAblLeftLeg;
            ArmorTotalLeftLeg = character.ArmorTotalLeftLeg;

            IsTurned = character.IsTurned;

            ShelterHead = character.ShelterHead;
            ShelterBody = character.ShelterBody;
            ShelterLeftHand = character.ShelterLeftHand;
            ShelterLeftLeg = character.ShelterLeftHand;
            ShelterRightHand = character.ShelterRightHand;
            ShelterRightLeg = character.ShelterRightLeg;

            _implants.AddRange(character.Implants);
            _equipments.AddRange(character.Equipments);
            _traits.AddRange(character.Features);
            _skills.AddRange(character.Skills);
            _talents.AddRange(character.Talents);
            _psyPowers.AddRange(character.PsyPowers);

            System.Random rnd = new System.Random();

            if (AgilitySuper > Agility / 10)
                Initiative = rnd.Next(1, 10) + AgilitySuper;
            else
                Initiative = rnd.Next(1, 10) + Agility / 10;
        }

        public string Name { get; set; }
        public string ShowingName { get; set; }
        public int Wounds { get; set; }
        public int WeaponSkill { get; set; }
        public int BallisticSkill { get; set; }
        public int Strength { get; set; }
        public int Toughness { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Perception { get; set; }
        public int Willpower { get; set; }
        public int Fellowship { get; set; }
        public int Influence { get; set; }

        public int WeaponSkillSuper { get; set; }
        public int BallisticSkillSuper { get; set; }
        public int StrengthSuper { get; set; }
        public int ToughnessSuper { get; set; }
        public int AgilitySuper { get; set; }
        public int IntelligenceSuper { get; set; }
        public int PerceptionSuper { get; set; }
        public int WillpowerSuper { get; set; }
        public int FellowshipSuper { get; set; }
        public int InfluenceSuper { get; set; }

        public int Half { get; set; }
        public int Full { get; set; }
        public int Natisk { get; set; }
        public int Run { get; set; }

        public int ArmorHead { get; set; }
        public int ArmorAblHead { get; set; }
        public int ArmorTotalHead { get; set; }

        public int ArmorRightHand { get; set; }
        public int ArmorAblRightHand { get; set; }
        public int ArmorTotalRightHand { get; set; }

        public int ArmorLeftHand { get; set; }
        public int ArmorAblLeftHand { get; set; }
        public int ArmorTotalLeftHand { get; set; }

        public int ArmorBody { get; set; }
        public int ArmorAblBody { get; set; }
        public int ArmorTotalBody { get; set; }

        public int ArmorRightLeg { get; set; }
        public int ArmorAblRightLeg { get; set; }
        public int ArmorTotalRightLeg { get; set; }

        public int ArmorLeftLeg { get; set; }
        public int ArmorAblLeftLeg { get; set; }
        public int ArmorTotalLeftLeg { get; set; }

        public bool IsTurned { get; set; }
        public bool IsHorde { get; set; }

        public int ShelterHead { get; set; }
        public int ShelterRightHand { get; set; }
        public int ShelterLeftHand { get; set; }
        public int ShelterBody { get; set; }
        public int ShelterRightLeg { get; set; }
        public int ShelterLeftLeg { get; set; }

        public int ShelterPoint { get; set; }

        public int Initiative { get; set; }

        public List<MechImplant> Implants => _implants;
        public List<Equipment> Equipments => _equipments;
        public List<Trait> Features => _traits;
        public List<Trait> Skills => _skills;
        public List<Trait> Talents => _talents;
        public List<Trait> PsyPowers => _psyPowers;
    }
}

