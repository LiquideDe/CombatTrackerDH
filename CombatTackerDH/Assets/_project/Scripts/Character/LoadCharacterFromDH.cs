using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


namespace CombarTracker
{
    public class LoadCharacterFromDH
    {
        public Character LoadCharacter(string path, Creators creators)
        {
            SaveLoadCharacter character = new SaveLoadCharacter();
            string allText = File.ReadAllText(path);
            allText = allText.Replace(Environment.NewLine, string.Empty);

            string[] notFormatData = allText.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> data = new List<string>();


            for (int i = 0; i < notFormatData.Length; i++)
                if (notFormatData[i].Length > 5)
                    data.Add($"{{{notFormatData[i]}}}");

            SaveLoadCharacterDH loadCharacter = JsonUtility.FromJson<SaveLoadCharacterDH>(data[0]);
            character.name = loadCharacter.name;
            character.showingName = loadCharacter.name;
            character.wounds = loadCharacter.wounds;

            int min = 1;
            int max = 10 + min;
            for (int i = min; i < max; i++)
            {
                SaveLoadCharacteristic characteristic = JsonUtility.FromJson<SaveLoadCharacteristic>(data[i]);
                SetCharacteristic(characteristic, character);
            }


            min = max;
            max = min + loadCharacter.amountSkills;
            List<Trait> skills = new List<Trait>();
            for (int i = min; i < max; i++)
            {
                SaveLoadSkill skill = JsonUtility.FromJson<SaveLoadSkill>(data[i]);
                skills.Add(new Trait(skill.name, skill.lvlLearned * 10 - 10));
            }

            List<MechImplant> implants = new List<MechImplant>();
            if (loadCharacter.amountImplants > 0)
            {

                min = max;
                max = min + loadCharacter.amountImplants;
                for (int i = min; i < max; i++)
                {
                    SaveLoadImplant implant = JsonUtility.FromJson<SaveLoadImplant>(data[i]);
                    implants.Add(new MechImplant(implant));
                    bool isFinded = false;
                    foreach (var item in creators.Implants)
                    {
                        if(string.Compare(implant.name, item.Name,true) == 0)
                        {
                            isFinded = true;
                            break;
                        }                            
                    }
                    if (isFinded == false)
                        creators.Implants.Add(implants[^1]);
                }
            }

            List<Equipment> equipments = new List<Equipment>();
            for (int i = max; i < data.Count; i++)
            {
                JSONTypeReader typeReader = JsonUtility.FromJson<JSONTypeReader>(data[i]);
                if (typeReader.typeEquipment == Equipment.TypeEquipment.Thing.ToString())
                {
                    JSONEquipmentReader jSONSmall = JsonUtility.FromJson<JSONEquipmentReader>(data[i]);
                    equipments.Add(new Equipment(jSONSmall.name, jSONSmall.description, jSONSmall.rarity, jSONSmall.amount, jSONSmall.weight));
                    if(!TryFindEquipmentByName(jSONSmall.name, creators))
                    {
                        creators.Equipments.Add(new Equipment(equipments[^1]));
                    }
                }
                else if (typeReader.typeEquipment == Equipment.TypeEquipment.Melee.ToString())
                {
                    JSONMeleeReader meleeReader = JsonUtility.FromJson<JSONMeleeReader>(data[i]);
                    equipments.Add(new Weapon(meleeReader));
                    if (!TryFindEquipmentByName(meleeReader.name, creators))
                    {
                        creators.Equipments.Add(new Weapon(meleeReader));
                    }
                }
                else if (typeReader.typeEquipment == Equipment.TypeEquipment.Range.ToString())
                {
                    JSONRangeReader rangeReader = JsonUtility.FromJson<JSONRangeReader>(data[i]);
                    equipments.Add(new Weapon(rangeReader));
                    if (!TryFindEquipmentByName(rangeReader.name, creators))
                    {
                        creators.Equipments.Add(new Weapon(rangeReader));
                    }
                }
                else if (typeReader.typeEquipment == Equipment.TypeEquipment.Grenade.ToString())
                {
                    JSONGrenadeReader grenadeReader = JsonUtility.FromJson<JSONGrenadeReader>(data[i]);
                    equipments.Add(new Weapon(grenadeReader));
                    if (!TryFindEquipmentByName(grenadeReader.name, creators))
                    {
                        creators.Equipments.Add(new Weapon(grenadeReader));
                    }
                }
                else if (typeReader.typeEquipment == Equipment.TypeEquipment.Armor.ToString())
                {
                    JSONArmorReader armorReader = JsonUtility.FromJson<JSONArmorReader>(data[i]);
                    equipments.Add(new Armor(armorReader));
                    if (!TryFindEquipmentByName(armorReader.name, creators))
                    {
                        creators.Equipments.Add(new Armor(armorReader));
                    }
                }
                else if (typeReader.typeEquipment == Equipment.TypeEquipment.Shield.ToString())
                {
                    JSONArmorReader armorReader = JsonUtility.FromJson<JSONArmorReader>(data[i]);
                    equipments.Add(new Armor(armorReader));
                    if (!TryFindEquipmentByName(armorReader.name, creators))
                    {
                        creators.Equipments.Add(new Armor(armorReader));
                    }
                }
            }

            List<Trait> traits = new List<Trait>();
            traits.AddRange(ReadPropertiesFromString(loadCharacter.traits));
            SetLvlsToTraits(loadCharacter.traitsLvl, traits);

            List<Trait> talents = new List<Trait>();
            talents.AddRange(ReadPropertiesFromString(loadCharacter.talents));

            List<Trait> psypowers = new List<Trait>();
            psypowers.AddRange(ReadPropertiesFromString(loadCharacter.psyPowers));

            AddSuperNatural(traits, character);
            CalculateArmor(traits, equipments, implants, character);

            Character adaptCharacter = new Character(character, implants, equipments, traits, skills, talents, psypowers);
            return adaptCharacter;
        }

        private void SetCharacteristic(SaveLoadCharacteristic characteristic, SaveLoadCharacter character)
        {
            switch (characteristic.name)
            {
                case "Навык Рукопашной":
                    character.weaponSkill = characteristic.amount;
                    break;

                case "Навык Стрельбы":
                    character.ballisticSkill = characteristic.amount;
                    break;

                case "Сила":
                    character.strength = characteristic.amount;
                    break;

                case "Выносливость":
                    character.toughness = characteristic.amount;
                    break;

                case "Ловкость":
                    character.agility = characteristic.amount;
                    break;

                case "Интеллект":
                    character.intelligence = characteristic.amount;
                    break;

                case "Восприятие":
                    character.perception = characteristic.amount;
                    break;

                case "Сила Воли":
                    character.willpower = characteristic.amount;
                    break;

                case "Общительность":
                    character.fellowship = characteristic.amount;
                    break;

                case "Влияние":
                    character.influence = characteristic.amount;
                    break;

            }
        }

        private bool TryFindEquipmentByName(string name, Creators creators)
        {
            foreach (var item in creators.Equipments)
            {
                if(string.Compare(name, item.Name) == 0)
                    return true;
            }

            return false;
        }

        private List<Trait> ReadPropertiesFromString(string text)
        {
            List<string> list = new List<string>();
            list = text.Split(new char[] { '/' }).ToList();

            List<Trait> properties = new List<Trait>();
            if (TryStringLength(list))
                for (int i = 0; i < list.Count; i++)
                    properties.Add(new Trait(list[i]));

            return properties;
        }

        private bool TryStringLength(List<string> list)
        {
            if (list.Count > 0)
            {
                if (list[0].Length > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private void SetLvlsToTraits(string lvls, List<Trait> traits)
        {
            List<string> listWithLvls = new List<string>();
            listWithLvls = lvls.Split(new char[] { '/' }).ToList();

            for (int i = 0; i < traits.Count; i++)
            {
                int.TryParse(listWithLvls[i], out int lvl);
                traits[i].Lvl = lvl;
            }
        }

        private void AddSuperNatural(List<Trait> traits, SaveLoadCharacter character)
        {
            foreach (var item in traits)
            {
                switch (item.Name)
                {
                    case "Сверхъестественная Навык Рукопашной":
                        character.weaponSkillSuper = item.Lvl + character.weaponSkill/10;
                        break;

                    case "Сверхъестественная Навык Стрельбы":
                        character.ballisticSkillSuper = item.Lvl + character.ballisticSkill/10;
                        break;

                    case "Сверхъестественная Сила":
                        character.strengthSuper = item.Lvl + character.strength/10;
                        break;

                    case "Сверхъестественная Выносливость":
                        character.toughnessSuper += item.Lvl + character.toughness/10;
                        break;

                    case "Сверхъестественная Ловкость":
                        character.agilitySuper = item.Lvl + character.agility/10;
                        break;

                    case "Сверхъестественная Интеллект":
                        character.intelligenceSuper = item.Lvl + character.intelligence/10;
                        break;

                    case "Сверхъестественная Восприятие":
                        character.perceptionSuper = item.Lvl + character.perception/10;
                        break;

                    case "Сверхъестественная Сила Воли":
                        character.willpowerSuper = item.Lvl + character.willpower/10;
                        break;

                    case "Сверхъестественная Общительность":
                        character.fellowshipSuper = item.Lvl + character.fellowship/10;
                        break;

                    case "Демонический":
                        character.toughnessSuper += item.Lvl;
                        break;
                }
            }
        }

        private void CalculateArmor(List<Trait> traits, List<Equipment> equipments,List<MechImplant> implants, SaveLoadCharacter character)
        {
            int bonusToughness = 0;
            int bonusArmor = 0;
            int bestArmorHead = 0;
            int bestArmorBody = 0;
            int bestArmorLeftHand = 0;
            int bestArmorRightHand = 0;
            int bestArmorLeftLeg = 0;
            int bestArmorRightLeg = 0;

            int bestShieldHead = 0;
            int bestShieldBody = 0;
            int bestShieldLeftHand = 0;
            int bestShieldLeftLeg = 0;
            int bestShieldRightLeg = 0;

            if (character.toughnessSuper > 0)
                bonusToughness = character.toughnessSuper;
            else
                bonusToughness = character.toughness / 10;

            foreach (Trait trait in traits)
            {
                if(string.Compare(trait.Name, "Природная броня") == 0 || string.Compare(trait.Name, "Машина") == 0)
                {
                    bonusArmor += trait.Lvl;
                }
            }

            foreach (var item in equipments)
            {
                if (item.TypeEq == Equipment.TypeEquipment.Shield)
                {
                    Armor armor = (Armor)item;
                    if (armor.DefBody > bestShieldBody)
                    {
                        bestShieldHead = armor.DefHead;
                        bestShieldBody = armor.DefBody;
                        bestShieldLeftHand = armor.DefHands;
                        bestShieldLeftLeg = armor.DefLegs;
                        bestShieldRightLeg = armor.DefLegs;
                    }
                }

                if (item.TypeEq == Equipment.TypeEquipment.Armor)
                {
                    Armor armor = (Armor)item;
                    if (armor.DefHead > bestArmorHead)
                        bestArmorHead = armor.DefHead;
                    if (armor.DefBody > bestArmorBody)
                        bestArmorBody = armor.DefBody;
                    if (armor.DefHands > bestArmorLeftHand || armor.DefHands > bestArmorRightHand)
                    {
                        bestArmorLeftHand = armor.DefHands;
                        bestArmorRightHand = armor.DefHands;
                    }
                    if (armor.DefLegs > bestArmorLeftLeg || armor.DefLegs > bestArmorRightLeg)
                    {
                        bestArmorLeftLeg = armor.DefLegs;
                        bestArmorRightLeg = armor.DefLegs;
                    }
                }

            }

            character.armorBody = bestArmorBody + bonusArmor + bestShieldHead;
            character.armorHead = bestArmorHead + bonusArmor + bestShieldBody;
            character.armorRightHand = bestArmorRightHand + bonusArmor;
            character.armorLeftHand = bestArmorLeftHand + bonusArmor + bestShieldLeftHand;
            character.armorRightLeg = bestArmorRightLeg + bonusArmor + bestShieldLeftLeg;
            character.armorLeftLeg = bestArmorLeftLeg + bonusArmor + bestShieldRightLeg;

            foreach(MechImplant implant in implants)
            {
                if(implant.Place == MechImplant.PartsOfBody.Head && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorHead += implant.Armor;
                    character.armorTotalHead += implant.BonusToughness;
                }
                else if(implant.Place == MechImplant.PartsOfBody.Body && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorBody += implant.Armor;
                    character.armorTotalBody += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.RightHand && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorRightHand += implant.Armor;
                    character.armorTotalRightHand += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.LeftHand && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorLeftHand += implant.Armor;
                    character.armorTotalLeftHand += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.RightLeg && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorRightLeg += implant.Armor;
                    character.armorTotalRightLeg += implant.BonusToughness;
                }
                else if (implant.Place == MechImplant.PartsOfBody.LeftLeg && (implant.Armor > 0 || implant.BonusToughness > 0))
                {
                    character.armorLeftLeg += implant.Armor;
                    character.armorTotalLeftLeg += implant.BonusToughness;
                }
            }

            character.armorTotalHead += character.armorHead + bonusToughness;
            character.armorTotalBody += character.armorBody + bonusToughness;
            character.armorTotalRightHand += character.armorRightHand + bonusToughness;
            character.armorTotalLeftHand += character.armorLeftHand + bonusToughness;
            character.armorTotalRightLeg += character.armorRightLeg + bonusToughness;
            character.armorTotalLeftLeg += character.armorLeftLeg + bonusToughness;
        }
    }
}

