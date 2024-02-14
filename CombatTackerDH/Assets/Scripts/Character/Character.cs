using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Character
{
    private string name, internalName;
    private int[] armorPoints = new int[6]; //0-голова, 1 - Л. Рука, 2 - П. Рука, 3 - Тело, 4 - Л. Нога, 5 - П. Нога
    private int[] armorPointsAbl = new int[6];
    private int[] characteristics = new int[10];
    private int[] unnaturualChar = new int[10];
    private List<PropertyCharacter> skills = new List<PropertyCharacter>();
    private List<PropertyCharacter> talents = new List<PropertyCharacter>();
    private List<PropertyCharacter> features = new List<PropertyCharacter>();
    private List<PropertyCharacter> psyPowers = new List<PropertyCharacter>();
    private List<PropertyCharacter> mechImplants = new List<PropertyCharacter>();
    private List<Armor> armors = new List<Armor>();
    private List<Weapon> weapons = new List<Weapon>();
    private List<Equipment> equipments = new List<Equipment>();
    private int wounds, treate;

    public string Name { get => name; set => name = value; }
    public string InternalName { get => internalName; set => internalName = value; }
    public int[] ArmorPoints { get => armorPoints; set => armorPoints = value; }
    public int[] ArmorPointsAbl { get => armorPointsAbl; set => armorPointsAbl = value; }
    public int[] Characteristics { get => characteristics; set => characteristics = value; }
    public int[] UnnaturualChar { get => unnaturualChar; set => unnaturualChar = value; }
    public List<PropertyCharacter> Skills { get => skills; set => skills = value; }
    public List<PropertyCharacter> Talents { get => talents; set => talents = value; }
    public List<PropertyCharacter> Features { get => features; set => features = value; }
    public List<PropertyCharacter> PsyPowers { get => psyPowers; set => psyPowers = value; }
    public List<PropertyCharacter> MechImplants { get => mechImplants; }
    public List<Armor> Armors { get => armors; set => armors = value; }
    public List<Weapon> Weapons { get => weapons; set => weapons = value; }
    public List<Equipment> Equipments { get => equipments; set => equipments = value; }
    public int Wounds { get => wounds; set => wounds = value; }
    public int Treate { get => treate; set => treate = value; }    

    public Character()
    {

    }
    public Character(SaveLoadCharacter loadCharacter, Creators creators)
    {
        name = loadCharacter.name;
        internalName = loadCharacter.internalName;
        wounds = loadCharacter.wounds;
        treate = loadCharacter.treate;

        LoadFromString(loadCharacter.armorPoints, armorPoints);
        LoadFromString(loadCharacter.armorPointsAbl, armorPointsAbl);
        LoadFromString(loadCharacter.characteristics, characteristics);
        LoadFromString(loadCharacter.unnaturualChar, unnaturualChar);

        skills.AddRange(LoadPropertyFromString(loadCharacter.skills, creators.Skills));
        talents.AddRange(LoadPropertyFromString(loadCharacter.talents, creators.Talents));
        features.AddRange(LoadPropertyFromString(loadCharacter.features, creators.Features));
        psyPowers.AddRange(LoadPropertyFromString(loadCharacter.psyPowers, creators.PsyPowers));
        mechImplants.AddRange(LoadPropertyFromString(loadCharacter.implants, creators.MechImplants));

        List<Equipment> futureArmors = new List<Equipment>();
        futureArmors.AddRange(LoadEquipmentFromString(loadCharacter.armors, creators.Equipments));
        foreach(Equipment equipment in futureArmors)
        {
            armors.Add((Armor)equipment);
        }

        List<Equipment> futureWeapons = new List<Equipment>();
        futureWeapons.AddRange(LoadEquipmentFromString(loadCharacter.weapons, creators.Equipments));
        foreach (Equipment equipment in futureWeapons)
        {
            weapons.Add((Weapon)equipment);
        }

        equipments.AddRange(LoadEquipmentFromString(loadCharacter.equipments, creators.Equipments));

        List<string> list = new List<string>();
        list = loadCharacter.skillsLvl.Split(new char[] { '/' }).ToList();

        if(CheckString(list))
        {
            for(int i = 0; i < skills.Count; i++)
            {
                skills[i].Lvl = int.Parse(list[i]);
            }
        }

        list.Clear();
        list = loadCharacter.featuresLvl.Split(new char[] { '/' }).ToList();
        if (CheckString(list))
        {
            for (int i = 0; i < features.Count; i++)
            {
                features[i].Lvl = int.Parse(list[i]);
            }
        }
    }
    private void LoadFromString(string text, int[] mas)
    {
        List<string> list = new List<string>();
        list = text.Split(new char[] { '/' }).ToList();
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i] = int.Parse(list[i]);
        }
    }

    private List<PropertyCharacter> LoadPropertyFromString(string text, List<PropertyCharacter> encyclop)
    {
        List<string> list = new List<string>();
        list = text.Split(new char[] { '/' }).ToList();

        List<PropertyCharacter> properties = new List<PropertyCharacter>();
        if(CheckString(list))
        foreach (string str in list)
        {
            foreach(PropertyCharacter property in encyclop)
            {
                if(string.Compare(str, property.Name, true) == 0)
                {
                    properties.Add(property);
                    break;
                }
            }
        }

        return properties;
    }

    private List<Equipment> LoadEquipmentFromString(string text, List<Equipment> encyclop)
    {
        List<string> list = new List<string>();
        list = text.Split(new char[] { '/' }).ToList();

        List<Equipment> equips = new List<Equipment>();
        if (CheckString(list))
            foreach (string str in list)
            {
                foreach (Equipment equip in encyclop)
                {
                    if (string.Compare(str, equip.Name, true) == 0)
                    {
                        equips.Add(equip);
                        break;
                    }
                }
            }

        return equips;
    }

    private bool CheckString(List<string> list)
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
    public void SaveCharacter()
    {
        var path = Path.Combine($"{Application.dataPath}/StreamingAssets/Characters/", internalName + ".JSON");
        List<string> data = new List<string>();

        SaveLoadCharacter saveLoad = new SaveLoadCharacter();

        saveLoad.internalName = internalName;
        saveLoad.wounds = wounds;
        saveLoad.treate = treate;

        saveLoad.armorPoints = "";
        saveLoad.armorPoints = PackingIntInText(armorPoints);

        saveLoad.armorPointsAbl = "";
        saveLoad.armorPointsAbl = PackingIntInText(armorPointsAbl);

        saveLoad.characteristics = "";
        saveLoad.characteristics = PackingIntInText(characteristics);

        saveLoad.unnaturualChar = "";
        saveLoad.unnaturualChar = PackingIntInText(unnaturualChar);

        saveLoad.equipments = "";
        saveLoad.equipments = PackEquipmentInText(equipments);

        saveLoad.armors = "";
        List<Equipment> tempArmor = new List<Equipment>();
        tempArmor.AddRange(armors);
        saveLoad.armors = PackEquipmentInText(tempArmor);

        saveLoad.weapons = "";
        List<Equipment> tempWeapons = new List<Equipment>();
        tempWeapons.AddRange(weapons);
        saveLoad.weapons = PackEquipmentInText(tempWeapons);

        saveLoad.features = "";
        saveLoad.features = PackingPropInText(features);

        saveLoad.skills = "";
        saveLoad.skills = PackingPropInText(skills);

        saveLoad.talents = "";
        saveLoad.talents = PackingPropInText(talents);

        saveLoad.psyPowers = "";
        saveLoad.psyPowers = PackingPropInText(psyPowers);

        saveLoad.implants = "";
        saveLoad.implants = PackingPropInText(mechImplants);

        int[] tempSkills = new int[skills.Count];
        for(int i = 0; i < skills.Count; i++)
        {
            tempSkills[i] = skills[i].Lvl;
        }
        saveLoad.skillsLvl = "";
        saveLoad.skillsLvl = PackingIntInText(tempSkills);

        int[] tempFeatures = new int[features.Count];
        for(int i = 0; i < features.Count; i++)
        {
            tempFeatures[i] = features[i].Lvl;
        }
        saveLoad.featuresLvl = "";
        saveLoad.featuresLvl = PackingIntInText(tempFeatures);
        data.Add(JsonUtility.ToJson(saveLoad));

        File.WriteAllLines(path, data);
    }

    private string PackingIntInText(int[] mas)
    {
        string text = "";
        foreach(int i in mas)
        {
            text += $"{i}/";
        }
        text = DeleteLastChar(text);

        return text;
    }

    private string PackEquipmentInText(List<Equipment> equipment)
    {
        string text = "";
        foreach (Equipment eq in equipment)
        {
            text += $"{eq.Name}/";
        }
        text = DeleteLastChar(text);

        return text;
    }

    private string PackingPropInText(List<PropertyCharacter> property)
    {
        string text = "";
        foreach (PropertyCharacter prop in property)
        {
            text += $"{prop.Name}/";
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
    /*
    public int Init { get => iniciative; }
    public void SetParams(int head, int leftHand, int rightHand, int body, int leftLeg, int rightLeg, int health, int bonusAgility, int bonusFatigue, string name, int clip, DeleteThis deleteThis)
    {
        this.head = head;
        this.leftHand = leftHand;
        this.rightHand = rightHand;
        this.body = body;
        this.leftLeg = leftLeg;
        this.rightLeg = rightLeg;
        this.health = health;
        this.bonusAgility = bonusAgility;
        this.bonusFatigue = bonusFatigue;
        nameCharacter = name;
        textName.text = name;
        this.clip = clip;
        maxClip = clip;
        ammo = clip * 2;
        UpdateText();
        this.deleteThis = deleteThis;

    }

    private void Start()
    {
        Game.BattleStarted += StartBattle;
        Game.BattleFinished += FinishBattle;

    }

    private void GenerateIniciative()
    {
        if(iniciativeInput.text == "")
        {
            var rand = new System.Random();
            iniciative = rand.Next(1, 10) + bonusAgility;
        }
        else
        {
            iniciative = int.Parse(iniciativeInput.text);
        }
        
    }

    private void UpdateText()
    {
        textHealth.text = health.ToString();
        textClip.text = clip.ToString();
    }

    public void GetDamage()
    {
        panelDamage.RegDelegateFromNPC(GetDamageWithParams);
    }

    private void GetDamageWithParams(int place, int damage, int prob)
    {
        if(place < 10)
        {
            Damage(head - prob, damage);
        }
        else if(place > 10 && place < 21)
        {
            Damage(rightHand - prob, damage);
        }
        else if (place > 20 && place < 31)
        {
            Damage(leftHand - prob, damage);
        }
        else if (place > 30 && place < 71)
        {
            Damage(body - prob, damage);
        }
        else if (place > 70 && place < 86)
        {
            Damage(rightLeg - prob, damage);
        }
        else if (place > 85 && place < 101)
        {
            Damage(leftLeg - prob, damage);
        }
    }

    private void Damage(int armor, int damage)
    {
        if (armor < 0)
            armor = 0;
        damage -= (armor + bonusFatigue);
        if (damage < 0)
        {
            damage = 0;
        }
        health -= damage;
        UpdateText(); 
    }

    public void Shoot()
    {
        if(clip > 0)
            clip--;
        UpdateText();
    }

    public void Reload()
    {
        if(ammo >= maxClip)
        {
            clip = maxClip;
            ammo -= maxClip;
        }
        else if(ammo > 0)
        {
            clip = ammo;
            ammo = 0;
        }
        UpdateText();
    }

    private void StartBattle()
    {
        GenerateIniciative();
    }

    private void FinishBattle()
    {
        image.color = Color.white;
    }

    public void DeleteThisCharacter()
    {
        deleteThis?.Invoke(this);
        Game.BattleStarted -= StartBattle;
        Game.BattleFinished -= FinishBattle;
        Destroy(gameObject);
    }

    public void CharacterTurn()
    {
        image.color = Color.green;
    }

    public void CharacterEndTurn()
    {
        image.color = Color.white;
    }*/
}
