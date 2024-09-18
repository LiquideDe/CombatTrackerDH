using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Creators
{
    private List<Equipment> _equipments = new List<Equipment>();
    private List<Feature> _skills = new List<Feature>();
    private List<Feature> _talents = new List<Feature>();
    private List<Feature> _features = new List<Feature>();
    private List<Feature> _psyPowers = new List<Feature>();
    private List<Feature> _weaponProp = new List<Feature>();
    private List<Character> _characters = new List<Character>();
    private List<Feature> _allFeatures = new List<Feature>();
    private List<MechImplant> _implants = new List<MechImplant>();
    private List<BattleScene> _scenes = new List<BattleScene>();

    public Creators()
    {
        LoadFeatures();
        LoadEquipments();
        LoadImplants();
        LoadCharacters();        
        LoadScenes();
    }

    public List<Equipment> Equipments => _equipments;
    public List<Feature> Skills => _skills;
    public List<Feature> Talents => _talents;
    public List<Feature> Features => _features;
    public List<Feature> PsyPowers => _psyPowers;
    public List<Character> Characters => _characters; 
    public List<Feature> WeaponProp => _weaponProp;
    public List<Feature> AllFeatures => _allFeatures;
    public List<MechImplant> Implants => _implants;
    public List<BattleScene> Scenes => _scenes;

    public Character GetCharacterByName(string name)
    {
        foreach (Character character in _characters)
        {
            if (string.Compare(character.Name, name, true) == 0)
            {
                return character;
            }
        }

        Debug.Log($"!!!!! Не смогли найти '{name}' !!!!!!!");
        return null;
    }

    public Feature GetFeatureByName(string name) => GetFeatureFromListByName(name, _features);

    public Feature GetSkillByName(string name) => GetFeatureFromListByName(name, _skills);

    public Feature GetTalentByName(string name) => GetFeatureFromListByName(name, _talents);

    public Feature GetPsypowerByName(string name) => GetFeatureFromListByName(name, _psyPowers);

    public Equipment GetEquipmentByName(string name)
    {
        foreach (Equipment equipment in _equipments)
        {
            if (string.Compare(name, equipment.Name, true) == 0)
            {
                if (equipment is Weapon weapon)
                {
                    Weapon newWeapon = new Weapon(weapon);
                    return newWeapon;
                }
                else if (equipment is Armor armor)
                {
                    Armor newArmor = new Armor(armor);
                    return newArmor;
                }
                else
                    return new Equipment(equipment);
            }
        }

        throw new System.Exception($"Не нашли экипировку под названием {name}");
    }

    public MechImplant GetMechImplantByName(string name)
    {
        foreach (MechImplant implant in _implants)
            if (string.Compare(name, implant.Name, true) == 0)
                return implant;


        throw new System.Exception($"Нет такого импланта под названием {name}");
    }

    public BattleScene GetBattleSceneByName(string name)
    {
        foreach (BattleScene battleScene in _scenes)
            if (string.Compare(battleScene.Name, name, true) == 0)
                return battleScene;

        throw new System.Exception($"Не нашли сцену {name}");
    }

    public string GetDescription(string name)
    {
        string text = "";
        foreach(Feature feature in _allFeatures)
            if(string.Compare(name, feature.Name, true) == 0)
            {
                text = $"{name} \n {feature.Description}";
            }

        return text;
    }

    private Feature GetFeatureFromListByName(string name, List<Feature> features)
    {
        foreach (Feature feature in features)
        {
            if (string.Compare(name, feature.Name, true) == 0)
                return feature;
        }

        throw new System.Exception($"Нет такого трейта под названием {name}");
    }

    private void LoadFeatures()
    {
        _skills.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Skills")).ToList()));
        _talents.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Talents")).ToList()));
        _talents.AddRange(GetEnemyTalents());
        _features.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Features")).ToList()));
        _psyPowers.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/PsyPowers")).ToList()));
        _weaponProp.AddRange(GetListProperty((Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/PropertiesOfWeapon")).ToList()));

        _allFeatures.AddRange(_skills);
        _allFeatures.AddRange(_talents);
        _allFeatures.AddRange(_features);
        _allFeatures.AddRange(_psyPowers);
        _allFeatures.AddRange(_weaponProp);
    }

    private void LoadEquipments()
    {
        string[] things = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Things", "*.JSON");
        foreach (string thing in things)
        {
            string[] data = File.ReadAllLines(thing);
            JSONEquipmentReader equipmentReader = JsonUtility.FromJson<JSONEquipmentReader>(data[0]);
            _equipments.Add(new Equipment(equipmentReader.name, equipmentReader.description, equipmentReader.rarity, equipmentReader.amount, equipmentReader.weight));
        }

        string[] armors = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Armor", "*.JSON");
        foreach (string armor in armors)
        {
            string[] data = File.ReadAllLines(armor);
            JSONArmorReader armortReader = JsonUtility.FromJson<JSONArmorReader>(data[0]);
            _equipments.Add(new Armor(armortReader));
        }

        string[] meleeWeapons = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Melee", "*.JSON");
        foreach (string melee in meleeWeapons)
        {
            string[] data = File.ReadAllLines(melee);
            JSONMeleeReader meleeReader = JsonUtility.FromJson<JSONMeleeReader>(data[0]);
            _equipments.Add(new Weapon(meleeReader));
        }

        string[] rangeWeapons = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Range", "*.JSON");
        foreach (string range in rangeWeapons)
        {
            string[] data = File.ReadAllLines(range);
            JSONRangeReader rangeReader = JsonUtility.FromJson<JSONRangeReader>(data[0]);
            _equipments.Add(new Weapon(rangeReader));
        }

        string[] grenades = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Equipments" + "/Weapons/Grenade", "*.JSON");
        foreach (string grenade in grenades)
        {
            string[] data = File.ReadAllLines(grenade);
            JSONGrenadeReader grenadeReader = JsonUtility.FromJson<JSONGrenadeReader>(data[0]);
            _equipments.Add(new Weapon(grenadeReader));
        }
    }

    private void LoadCharacters()
    {
        string[] characters = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Characters", "*.JSON");
        foreach (string character in characters)
        {
            string[] data = File.ReadAllLines(character);
            SaveLoadCharacter characterReader = JsonUtility.FromJson<SaveLoadCharacter>(data[0]);
            List<Feature> skills = new List<Feature>();
            skills.AddRange(ReadPropertiesFromString(characterReader.skills));
            SetLvlsToFeatures(characterReader.lvlsOfSkills, skills);

            List<Feature> features = new List<Feature>();
            features.AddRange(ReadPropertiesFromString(characterReader.features));
            SetLvlsToFeatures(characterReader.lvlsOfFeatures, features);

            List<Feature> talents = new List<Feature>();
            talents.AddRange(ReadPropertiesFromString(characterReader.talents));

            List<Feature> psypowers = new List<Feature>();
            psypowers.AddRange(ReadPropertiesFromString(characterReader.psypowers));

            List<string> list = new List<string>();
            list = characterReader.implants.Split(new char[] { '/' }).ToList();
            List<MechImplant> implants = new List<MechImplant>();
            if(characterReader.implants.Length>0)
            foreach (string str in list)
                implants.Add(GetMechImplantByName(str));

            List<Equipment> equipments = new List<Equipment>();
            list.Clear();
            list = characterReader.equipments.Split(new char[] { '/' }).ToList();
            foreach (string str in list)
                equipments.Add(GetEquipmentByName(str));

            list.Clear();
            list = characterReader.amountsEquipments.Split(new char[] { '/' }).ToList();
            for(int i = 0; i < equipments.Count; i++)
            {
                int.TryParse(list[i], out int amount);
                equipments[i].Amount = amount;
            }

            _characters.Add(new Character(characterReader, implants, equipments, features, skills, talents, psypowers));
        }
    }

    private void LoadImplants()
    {
        string[] implantsJson = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/Implants", "*.JSON");

        foreach (string implant in implantsJson)
        {
            string[] data = File.ReadAllLines(implant);
            SaveLoadImplant implantSaveLoad = JsonUtility.FromJson<SaveLoadImplant>(data[0]);
            _implants.Add(new MechImplant(implantSaveLoad));
        }
    }

    private void LoadScenes()
    {
        string[] scenesJson = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/BattleScenes", "*.JSON");
        foreach(string sceneJson in scenesJson)
        {
            string[] data = File.ReadAllLines(sceneJson);
            SaveLoadScene loadScene = JsonUtility.FromJson<SaveLoadScene>(data[0]);
            List<string> charactersNames = new List<string>();
            charactersNames = loadScene.charactersNames.Split(new char[] { '/' }).ToList();

            List<string> showingNames = new List<string>();
            showingNames = loadScene.showingCharactersNames.Split(new char[] { '/' }).ToList();

            List<Character> characters = new List<Character>();
            for(int i = 0; i < charactersNames.Count; i++)
            {
                characters.Add(new Character(GetCharacterByName(charactersNames[i])));
                characters[^1].ShowingName = showingNames[i];
            }
            _scenes.Add(new BattleScene(loadScene, characters));
        }
    }

    private List<Feature> GetListProperty(List<string> dirs)
    {
        List<Feature> properties = new List<Feature>();
        foreach (string path in dirs)
        {
            properties.Add(GetPropertyFromPath(path));
        }

        return properties;
    }

    private List<Feature> GetEnemyTalents()
    {
        List<Feature> talents = new List<Feature>();
        string[] talentsJson = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/EnemyTalent", "*.JSON");
        foreach (string talentJson in talentsJson)
        {
            string[] data = File.ReadAllLines(talentJson);
            SaveLoadEnemyTalent loadEnemyTalent = JsonUtility.FromJson<SaveLoadEnemyTalent>(data[0]);
            talents.Add(new Feature(loadEnemyTalent.name, loadEnemyTalent.description));
        }

        return talents;
    }

    private Feature GetPropertyFromPath(string path)
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
        Feature property = new Feature(name, descr);
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

    private List<Feature> ReadPropertiesFromString(string text)
    {
        List<string> list = new List<string>();
        list = text.Split(new char[] { '/' }).ToList();        

        List<Feature> properties = new List<Feature>();
        if (TryStringLength(list))
            for(int i = 0; i < list.Count; i++)                          
                properties.Add(new Feature(list[i]));            

        return properties;
    }

    private void SetLvlsToFeatures(string lvls, List<Feature> features)
    {
        List<string> listWithLvls = new List<string>();
        listWithLvls = lvls.Split(new char[] { '/' }).ToList();

        for(int i = 0; i < features.Count; i++)
        {
            int.TryParse(listWithLvls[i], out int lvl);
            features[i].Lvl = lvl;
        }
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
    
    
}
