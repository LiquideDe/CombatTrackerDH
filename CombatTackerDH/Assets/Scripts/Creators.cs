using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Creators
{
    private List<Equipment> equipments = new List<Equipment>();
    private List<PropertyCharacter> skills = new List<PropertyCharacter>();
    private List<PropertyCharacter> talents = new List<PropertyCharacter>();
    private List<PropertyCharacter> features = new List<PropertyCharacter>();
    private List<PropertyCharacter> psyPowers = new List<PropertyCharacter>();
    private List<PropertyCharacter> weaponProp = new List<PropertyCharacter>();
    private List<PropertyCharacter> mechImplants = new List<PropertyCharacter>();
    private List<Character> characters = new List<Character>();

    public Creators()
    {
        skills.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Skills")).ToList()));
        talents.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Talents")).ToList()));
        features.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Features")).ToList()));
        psyPowers.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/PsyPowers")).ToList()));
        weaponProp.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/PropertiesOfWeapon")).ToList()));
        mechImplants.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Implants")).ToList()));

        string[] things = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Things", "*.JSON");
        foreach (string thing in things)
        {
            string[] data = File.ReadAllLines(thing);
            JSONEquipmentReader equipmentReader = JsonUtility.FromJson<JSONEquipmentReader>(data[0]);
            equipments.Add(new Equipment(equipmentReader.name, equipmentReader.description,equipmentReader.rarity, equipmentReader.weight));
        }

        string[] armors = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Armor", "*.JSON");
        foreach (string armor in armors)
        {
            string[] data = File.ReadAllLines(armor);
            JSONArmorReader armortReader = JsonUtility.FromJson<JSONArmorReader>(data[0]);
            equipments.Add(new Armor(armortReader));
        }

        string[] meleeWeapons = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Melee", "*.JSON");
        foreach (string melee in meleeWeapons)
        {
            string[] data = File.ReadAllLines(melee);
            JSONMeleeReader meleeReader = JsonUtility.FromJson<JSONMeleeReader>(data[0]);
            equipments.Add(new Weapon(meleeReader));
        }

        string[] rangeWeapons = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Range", "*.JSON");
        foreach (string range in rangeWeapons)
        {
            string[] data = File.ReadAllLines(range);
            JSONRangeReader rangeReader = JsonUtility.FromJson<JSONRangeReader>(data[0]);
            equipments.Add(new Weapon(rangeReader));
        }

        string[] grenades = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Grenade", "*.JSON");
        foreach (string grenade in grenades)
        {
            string[] data = File.ReadAllLines(grenade);
            JSONGrenadeReader grenadeReader = JsonUtility.FromJson<JSONGrenadeReader>(data[0]);
            equipments.Add(new Weapon(grenadeReader));
        }
        string[] chars = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Characters", "*.JSON");
        foreach (string character in chars)
        {
            string[] data = File.ReadAllLines(character);
            SaveLoadCharacter characterReader = JsonUtility.FromJson<SaveLoadCharacter>(data[0]);
            characters.Add(new Character(characterReader, this));
        }

    }

    private List<PropertyCharacter> GetListProperty(List<string> dirs)
    {
        List<PropertyCharacter> properties = new List<PropertyCharacter>();
        foreach (string path in dirs)
        {
            properties.Add(AddingProp(path));
        }

        return properties;
    }

    private PropertyCharacter AddingProp(string path)
    {
        string name, descr;
        if(File.Exists(path + "/Название.txt"))
        {
            name = ReadText(path + "/Название.txt");
        }
        else if(File.Exists(path + "/Param.JSON"))
        {
            string[] data = File.ReadAllLines(path + "/Param.JSON");
            JSONReader reader = JsonUtility.FromJson<JSONReader>(data[0]);
            name = reader.name;
        }
        else
        {
            string[] data = File.ReadAllLines(path + "/Parameters.JSON");
            JSONReader reader = JsonUtility.FromJson<JSONReader>(data[0]);
            name = reader.name;
        }
        descr = ReadText(path + "/Описание.txt");
        PropertyCharacter property = new PropertyCharacter(name, descr);
        return property;
    }
    private string ReadText(string nameFile)
    {
        string txt;
        using (StreamReader _sw = new StreamReader(nameFile, Encoding.Default))
        {
            txt = (_sw.ReadToEnd());
            _sw.Close();
        }
        return txt;
    }

    public Character GetCharacterByName(string name)
    {
        foreach(Character character in characters)
        {
            if(string.Compare(character.InternalName, name, true) == 0)
            {
                return character;
            }
        }

        Debug.Log($"!!!!! Не смогли найти '{name}' !!!!!!!");
        return null;
    }
    public List<Equipment> Equipments { get => equipments; }
    public List<PropertyCharacter> Skills { get => skills; }
    public List<PropertyCharacter> Talents { get => talents; }
    public List<PropertyCharacter> Features { get => features; }
    public List<PropertyCharacter> PsyPowers { get => psyPowers; }
    public List<Character> Characters { get => characters; }
    public List<PropertyCharacter> WeaponProp { get => weaponProp; }
    public List<PropertyCharacter> MechImplants { get => mechImplants; }
}
