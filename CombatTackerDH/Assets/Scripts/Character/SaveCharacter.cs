using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveCharacter
{
    public SaveCharacter(Character character)
    {
        var path = Path.Combine($"{Application.dataPath}/StreamingAssets/Characters/", character.Name + ".JSON");
        List<string> data = new List<string>();
        SaveLoadCharacter saveLoad = new SaveLoadCharacter();

        saveLoad.agility = character.Agility;
        saveLoad.agilitySuper = character.AgilitySuper;
        saveLoad.amountsEquipments = PackAmountEquipmentsInText(character.Equipments);
        saveLoad.armorAblBody = character.ArmorAblBody;
        saveLoad.armorAblHead = character.ArmorAblHead;
        saveLoad.armorAblLeftHand = character.ArmorAblLeftHand;
        saveLoad.armorAblLeftLeg = character.ArmorAblLeftLeg;
        saveLoad.armorAblRightHand = character.ArmorAblRightHand;
        saveLoad.armorAblRightLeg = character.ArmorAblRightLeg;
        saveLoad.armorBody = character.ArmorBody;
        saveLoad.armorHead = character.ArmorHead;
        saveLoad.armorLeftHand = character.ArmorLeftHand;
        saveLoad.armorLeftLeg = character.ArmorLeftLeg;
        saveLoad.armorRightHand = character.ArmorRightHand;
        saveLoad.armorRightLeg = character.ArmorRightLeg;
        saveLoad.armorTotalBody = character.ArmorTotalBody;
        saveLoad.armorTotalHead = character.ArmorTotalHead;
        saveLoad.armorTotalLeftHand = character.ArmorTotalLeftHand;
        saveLoad.armorTotalLeftLeg = character.ArmorTotalLeftLeg;
        saveLoad.armorTotalRightHand = character.ArmorTotalRightHand;
        saveLoad.armorTotalRightLeg = character.ArmorTotalRightLeg;
        saveLoad.ballisticSkill = character.BallisticSkill;
        saveLoad.ballisticSkillSuper = character.BallisticSkillSuper;
        saveLoad.equipments = PackListInText(character.Equipments);
        saveLoad.features = PackListInText(character.Features);
        saveLoad.fellowship = character.Fellowship;
        saveLoad.fellowshipSuper = character.FellowshipSuper;
        saveLoad.full = character.Full;
        saveLoad.half = character.Half;
        saveLoad.implants = PackListInText(character.Implants);
        saveLoad.influence = character.Influence;
        saveLoad.influenceSuper = character.InfluenceSuper;
        saveLoad.intelligence = character.Intelligence;
        saveLoad.intelligenceSuper = character.IntelligenceSuper;
        saveLoad.isTurned = character.IsTurned;
        saveLoad.lvlsOfFeatures = PackLvlFeaturesInText(character.Features);
        saveLoad.lvlsOfSkills = PackLvlFeaturesInText(character.Skills);
        saveLoad.name = character.Name;
        saveLoad.natisk = character.Natisk;
        saveLoad.perception = character.Perception;
        saveLoad.perceptionSuper = character.PerceptionSuper;
        saveLoad.psypowers = PackListInText(character.PsyPowers);
        saveLoad.run = character.Run;
        saveLoad.shelterBody = character.ShelterBody;
        saveLoad.shelterHead = character.ShelterHead;
        saveLoad.shelterLeftHand = character.ShelterLeftHand;
        saveLoad.shelterLeftLeg = character.ShelterLeftLeg;
        saveLoad.shelterRightHand = character.ShelterRightHand;
        saveLoad.shelterRightLeg = character.ShelterRightLeg;
        saveLoad.showingName = character.ShowingName;
        saveLoad.skills = PackListInText(character.Skills);
        saveLoad.strength = character.Strength;
        saveLoad.strengthSuper = character.StrengthSuper;
        saveLoad.talents = PackListInText(character.Talents);
        saveLoad.toughness = character.Toughness;
        saveLoad.toughnessSuper = character.ToughnessSuper;
        saveLoad.weaponSkill = character.WeaponSkill;
        saveLoad.weaponSkillSuper = character.WeaponSkillSuper;
        saveLoad.willpower = character.Willpower;
        saveLoad.willpowerSuper = character.WillpowerSuper;
        saveLoad.wounds = character.Wounds;
        saveLoad.shelterPoint = character.ShelterPoint;

        data.Add(JsonUtility.ToJson(saveLoad));

        File.WriteAllLines(path, data);
    }

    private string PackListInText<T>(List<T> list) where T : IName
    {
        string text = "";
        foreach (IName feature in list)
        {
            text += $"{feature.Name}/";
        }
        text = DeleteLastChar(text);

        return text;
    }

    private string PackAmountEquipmentsInText(List<Equipment> equipment)
    {
        string text = "";
        foreach (Equipment eq in equipment)
        {
            text += $"{eq.Amount}/";
        }
        text = DeleteLastChar(text);

        return text;
    }

    private string PackLvlFeaturesInText(List<Feature> features)
    {
        string text = "";
        foreach (Feature feature in features)
        {
            text += $"{feature.Lvl}/";
        }
        text = DeleteLastChar(text);

        return text;
    }

    private string DeleteLastChar(string text)
    {
        if (text.Length > 0)
        {
            string tex = text.TrimEnd('/');
            return tex;
        }
        else
        {
            return text;
        }

    }
}
