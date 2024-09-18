using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.IO;

public class CreationCharacterPresenter : IPresenter
{
    public event Action Close;
    private CreationCharacterView _view;
    private Creators _creators;
    private LvlFactory _lvlFactory;
    private AudioManager _audioManager;
    private CanDestroyView _tempView;
    private CanDestroyView _propertyView;
    private List<MechImplant> _implants = new List<MechImplant>();
    private List<Equipment> _equipments = new List<Equipment>();
    private List<Feature> _features = new List<Feature>();
    private List<Feature> _skills = new List<Feature>();
    private List<Feature> _talents = new List<Feature>();
    private List<Feature> _psyPowers = new List<Feature>();
    private delegate void MethodForListCreateNew();
    private delegate void MethodForListChooseOne(string name);

    [Inject]
    private void Construct(Creators creators, AudioManager audioManager, LvlFactory lvlFactory)
    {
        _creators = creators;
        _audioManager = audioManager;
        _lvlFactory = lvlFactory;
    }

    public void Initialize(CreationCharacterView view)
    {
        _view = view;
        Subscribe();
    }

    private void Subscribe()
    {
        _view.AddArmor += ShowArmors;
        _view.AddBallistic += ShowRange;
        _view.AddFeature += ShowFeatures;
        _view.AddGrenade += ShowGrenades;
        _view.AddImplant += ShowImplants;
        _view.AddPsypower += Showpsypowers;
        _view.AddSkill += ShowSkills;
        _view.AddTalent += ShowTalents;
        _view.AddThing += ShowThings;
        _view.AddWeapon += ShowWeapons;
        _view.Close += CancelPressed;
        _view.ChangeLvl += ChangeLvl;
        _view.Done += Done;
        _view.RemoveFeature += RemoveFeature;
        _view.Warning += _audioManager.PlayWarning;
        _view.CalculateAll += CalculateArmors;
    }

    private void Unscribe()
    {
        _view.AddArmor -= ShowArmors;
        _view.AddBallistic -= ShowRange;
        _view.AddFeature -= ShowFeatures;
        _view.AddGrenade -= ShowGrenades;
        _view.AddImplant -= ShowImplants;
        _view.AddPsypower -= Showpsypowers;
        _view.AddSkill -= ShowSkills;
        _view.AddTalent -= ShowTalents;
        _view.AddThing -= ShowThings;
        _view.AddWeapon -= ShowWeapons;
        _view.Close -= CancelPressed;
        _view.ChangeLvl -= ChangeLvl;
        _view.Done -= Done;
        _view.RemoveFeature -= RemoveFeature;
        _view.Warning -= _audioManager.PlayWarning;
        _view.CalculateAll -= CalculateArmors;
    }

    private void ShowArmors()
    {
        _audioManager.PlayClick();
        List<Armor> armors = new List<Armor>();
        foreach(Equipment equipment in _creators.Equipments)        
            if(equipment.TypeEq == Equipment.TypeEquipment.Armor)
            {
                Armor armor = (Armor)equipment;
                armors.Add(armor);
            }

        ShowList(armors, ShowNewArmorForm, ChooseEquipment,"Выберите броню");
    }

    private void ChooseEquipment(string name)
    {
        _audioManager.PlayDone();
        Debug.Log($"Добавили новый {name}");
        _equipments.Add(_creators.GetEquipmentByName(name));
        CloseAllListAndForms();
        UpdateFeaturesView();
        UpdateWeapons();
    }

    private void ShowNewArmorForm()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        NewArmor newArmor = _lvlFactory.Get(TypeScene.CreationArmor).GetComponent<NewArmor>();
        newArmor.Close += CloseCreationForm;
        newArmor.ReturnNewEquipment += AddNewEquipment;
        newArmor.WrongInput += _audioManager.PlayWarning;
        _tempView = newArmor;
        newArmor.Initialize();
    }

    private void CloseCreationForm()
    {
        _audioManager.PlayCancel();
        _tempView.DestroyView();
    }
    private void AddNewEquipment(Equipment equipment)
    {
        Debug.Log($"Добавили новый {equipment.Name}");
        CloseAllListAndForms();
        _audioManager.PlayDone();
        _equipments.Add(equipment);
        _creators.Equipments.Add(equipment);
        //_tempView.DestroyView();
        UpdateFeaturesView();
        UpdateWeapons();
    }

    private void ShowRange() => ShowList(ShowWeapons(Equipment.TypeEquipment.Range), ShowNewRangeForm, ChooseEquipment, "Выберите стрелковое");
    
    private void ShowNewRangeForm()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        NewRange newRange = _lvlFactory.Get(TypeScene.CreationRange).GetComponent<NewRange>();
        newRange.Close += CloseCreationForm;
        newRange.NeedInProperties += ShowPropertiesList;
        newRange.ReturnNewEquipment += AddNewEquipment;
        newRange.WrongInput += _audioManager.PlayWarning;
        _tempView = newRange;
        newRange.Initialize();
    }

    private void ShowPropertiesList()
    {
        _audioManager.PlayClick();
        ShowList(_creators.WeaponProp, AddProperty, "Выберите особенность", true);
    }

    private void AddProperty(string name)
    {        
        _audioManager.PlayDone();
        //CloseAllListAndForms();
        _propertyView.DestroyView();

        CreatorNewEquipment creatorNewEquipment = (CreatorNewEquipment)_tempView;
        creatorNewEquipment.AddProperty(name);
    }

    private List<Weapon> ShowWeapons(Equipment.TypeEquipment typeEquipment)
    {
        _audioManager.PlayClick();
        List<Weapon> weapons = new List<Weapon>();
        foreach (Equipment equipment in _creators.Equipments)
            if (equipment.TypeEq == typeEquipment)
            {
                Weapon weapon = (Weapon)equipment;
                weapons.Add(weapon);
            }

        return weapons;
    }

    private void ShowGrenades() => ShowList(ShowWeapons(Equipment.TypeEquipment.Grenade), ShowGrenadeNewForm, ChooseEquipment, "Выберите гранату");

    private void ShowGrenadeNewForm()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        NewGrenade newGrenade = _lvlFactory.Get(TypeScene.CreationGrenade).GetComponent<NewGrenade>();
        newGrenade.Close += CloseCreationForm;
        newGrenade.NeedInProperties += ShowPropertiesList;
        newGrenade.ReturnNewEquipment += AddNewEquipment;
        newGrenade.WrongInput += _audioManager.PlayWarning;
        _tempView = newGrenade;
        newGrenade.Initialize();
    }

    private void ShowWeapons() => ShowList(ShowWeapons(Equipment.TypeEquipment.Melee), ShowMeleeNewForm, ChooseEquipment, "Выберите оружие ближнего боя");

    private void ShowMeleeNewForm()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        NewMelee newMelee = _lvlFactory.Get(TypeScene.CreationMelee).GetComponent<NewMelee>();
        newMelee.Close += CloseCreationForm;
        newMelee.NeedInProperties += ShowPropertiesList;
        newMelee.ReturnNewEquipment += AddNewEquipment;
        newMelee.WrongInput += _audioManager.PlayWarning;
        _tempView = newMelee;
        newMelee.Initialize();
    }

    private void ShowThings()
    {
        List<Equipment> equipments = new List<Equipment>();
        foreach (Equipment equipment in _creators.Equipments)
            if (equipment.TypeEq == Equipment.TypeEquipment.Thing)
                equipments.Add(equipment);
        ShowList(equipments, ShowNewEquipment ,ChooseEquipment, "Выберите предмет");
    }

    private void ShowNewEquipment()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        CreatorNewEquipment newEquipment = _lvlFactory.Get(TypeScene.CreationThing).GetComponent<CreatorNewEquipment>();
        newEquipment.Close += CloseCreationForm;
        newEquipment.ReturnNewEquipment += AddNewEquipment;
        newEquipment.WrongInput += _audioManager.PlayWarning;
        _tempView = newEquipment;
        newEquipment.Initialize();
    }

    private void ShowSkills()
    {
        _audioManager.PlayClick();
        ShowList(_creators.Skills, AddSkill, "Выберите навык");
    }

    private void AddSkill(string name) => AddFeatureToList(_creators.GetSkillByName(name), _skills);

    private void ShowFeatures()
    {
        _audioManager.PlayClick();
        ShowList(_creators.Features, AddFeature, "Выберите черту");
    }

    private void AddFeature(string name) => AddFeatureToList(_creators.GetFeatureByName(name), _features);

    private void ShowTalents()
    {
        _audioManager.PlayClick();
        ShowList(_creators.Talents, ShowCreationNewTalent,AddTalent, "Выберите талант");
    }

    private void ShowCreationNewTalent()
    {
        _audioManager.PlayClick();
        CloseAllListAndForms();
        CreationTalentView creationTalent = _lvlFactory.Get( TypeScene.CreationTalent).GetComponent<CreationTalentView>();
        creationTalent.Close += CloseCreationForm;
        creationTalent.ReturnNewTalent += AddNewTalent;
        _tempView = creationTalent;
    }

    private void AddNewTalent(Feature talent)
    {
        _audioManager.PlayDone();
        SaveLoadEnemyTalent saveLoadEnemy = new SaveLoadEnemyTalent();
        saveLoadEnemy.name = talent.Name;
        saveLoadEnemy.description = talent.Description;
        var path = Path.Combine($"{Application.dataPath}/StreamingAssets/EnemyTalent/", saveLoadEnemy.name + ".JSON");
        List<string> data = new List<string>();
        data.Add(JsonUtility.ToJson(saveLoadEnemy));
        File.WriteAllLines(path, data);

        _talents.Add(talent);
        _creators.Talents.Add(talent);
        CloseAllListAndForms();
        UpdateFeaturesView();
    }

    private void AddTalent(string name) => AddFeatureToList(_creators.GetTalentByName(name), _talents);

    private void Showpsypowers()
    {
        _audioManager.PlayClick();
        ShowList(_creators.PsyPowers, AddPsypower, "Выберите пси силу");
    }

    private void AddPsypower(string name) => AddFeatureToList(_creators.GetPsypowerByName(name), _psyPowers);
       
    private void AddFeatureToList(Feature feature, List<Feature> features)
    {
        _audioManager.PlayDone();
        features.Add(new Feature(feature));
        CloseAllListAndForms();
        UpdateFeaturesView();
    }

    private void ShowImplants()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        ShowList(_creators.Implants, ShowImplantForm, AddImplant, "Выберите имплант");
    }

    private void AddImplant(string name)
    {
        _audioManager.PlayDone();
        _implants.Add(new MechImplant(_creators.GetMechImplantByName(name)));
        CloseAllListAndForms();
        UpdateFeaturesView();
    }

    private void ShowImplantForm()
    {
        CloseAllListAndForms();
        _audioManager.PlayClick();
        NewImplant newImplant = _lvlFactory.Get(TypeScene.CreationImplant).GetComponent<NewImplant>();
        newImplant.Close += CloseCreationForm;
        newImplant.ReturnImplant += AddNewImplant;
        newImplant.WrongInput += _audioManager.PlayWarning;
        _tempView = newImplant;
        newImplant.Initialize();
    }

    private void AddNewImplant(MechImplant implant)
    {
        _audioManager.PlayDone();
        _implants.Add(implant);
        _creators.Implants.Add(implant);
        UpdateFeaturesView();
        _tempView.DestroyView();

    }

    private void ShowList<T>(List<T> listWithItems, MethodForListCreateNew methodForCreate,MethodForListChooseOne methodForChoose, string nameOfList) where T : IName
    {
        CloseAllListAndForms();

        ListWithNewItemsAndNewButton listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewButton).GetComponent<ListWithNewItemsAndNewButton>();
        listWithNewItems.Close += CloseAllListAndForms;
        listWithNewItems.Close += _audioManager.PlayCancel;
        listWithNewItems.AddNewItem += methodForCreate.Invoke;
        listWithNewItems.ChooseThis += methodForChoose.Invoke;
        listWithNewItems.Initialize(listWithItems, nameOfList);
        _tempView = listWithNewItems;
    }

    private void ShowList<T>(List<T> listWithItems, MethodForListChooseOne methodForChoose, string nameOfList, bool isProperty = false) where T : IName
    {
        if(isProperty == false)
            CloseAllListAndForms();

        ListWithNewItems listWithNewItems = _lvlFactory.Get(TypeScene.ListWithNewButton).GetComponent<ListWithNewItems>();
        if(isProperty)
            listWithNewItems.Close += CloseProperty;
        else
            listWithNewItems.Close += CloseAllListAndForms;
        listWithNewItems.Close += _audioManager.PlayCancel;
        listWithNewItems.ChooseThis += methodForChoose.Invoke;
        listWithNewItems.Initialize(listWithItems, nameOfList);

        if (isProperty == false)
            _tempView = listWithNewItems;
        else
            _propertyView = listWithNewItems;
    }

    private void CancelPressed()
    {
        _audioManager.PlayCancel();
        Unscribe();
        _view.DestroyView();
        Close?.Invoke();

    }

    private void CloseProperty()
    {
        _propertyView.DestroyView();
        _propertyView = null;
    }

    private void CloseAllListAndForms()
    {
        if (_tempView != null)
            _tempView.DestroyView();
        _tempView = null;
    }

    private void Done(SaveLoadCharacter save)
    {
        _audioManager.PlayDone();
        Unscribe();
        Character character = new Character(save, _implants, _equipments, _features, _skills, _talents, _psyPowers);
        new SaveCharacter(character);
        _creators.Characters.Add(character);
        _view.DestroyView();
        Close?.Invoke();
    }

    private void RemoveFeature(string name)
    {
        _audioManager.PlayCancel();
        if (RemoveFeatureFromlist(name, _skills)) { }
        else if (RemoveFeatureFromlist(name, _talents)) { }
        else if (RemoveFeatureFromlist(name, _features)) { }
        else if (RemoveFeatureFromlist(name, _psyPowers)) { }
        else if (RemoveFeatureFromlist(name, _equipments)) { }
        else if (RemoveFeatureFromlist(name, _implants)) { }

        UpdateFeaturesView();
        UpdateWeapons();
    }

    private void ChangeLvl(string name, int lvl)
    {
        if (ChangeLvlInList(name, lvl, _skills)) { }
        else if (ChangeLvlInList(name, lvl, _features)) { }
        else
        {
            foreach (Equipment equipment in _equipments)
            {
                if (string.Compare(name, equipment.Name) == 0)
                    equipment.Amount = lvl;
            }
        }

        UpdateFeaturesView();
    }

    private bool ChangeLvlInList(string name, int lvl, List<Feature> list) 
    {
        foreach (Feature feature in list)
            if (string.Compare(name, feature.Name) == 0)
            {
                feature.Lvl = lvl;
                return true;
            }

        return false;
    }

    private bool RemoveFeatureFromlist<T>(string name, List<T> list) where T : IName
    {
        foreach(T item in list)
            if(string.Compare(name, item.Name, true) == 0)
            {
                list.Remove(item);
                return true;
            }

        return false;
    }

    private void UpdateFeaturesView()
    {
        List<Feature> features = new List<Feature>();
        features.AddRange(_skills);
        features.AddRange(_features);
        features.AddRange(_talents);
        features.AddRange(_psyPowers);

        foreach (Equipment equipment in _equipments)
            features.Add(new Feature(equipment.Name, equipment.Amount));

        foreach (MechImplant implant in _implants)
            features.Add(new Feature(implant.Name));

        _view.UpdateListFeatures(features);
    }

    private void UpdateWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();
        foreach (Equipment equipment in _equipments)
            if (equipment is Weapon)
                weapons.Add((Weapon)equipment);

        _view.UpdateGuns(weapons);
    }

    private void CalculateArmors()
    {
        _audioManager.PlayClick();
        int toughness = _view.Toughness() / 10;
        int agility = _view.Agility() / 10;
        SaveLoadCharacter save = new SaveLoadCharacter();

        foreach (Feature feature in _features)
            if (string.Compare(feature.Name, "Демонический") == 0)
                save.toughnessSuper += feature.Lvl;
            else if (string.Compare(feature.Name, "Сверхъестественная Выносливость") == 0)
                save.toughnessSuper += feature.Lvl;
            else if (string.Compare(feature.Name, "Машина") == 0)
            {
                save.armorAblBody += feature.Lvl;
                save.armorAblHead += feature.Lvl;
                save.armorAblLeftHand += feature.Lvl;
                save.armorAblLeftLeg += feature.Lvl;
                save.armorAblRightHand += feature.Lvl;
                save.armorAblRightLeg += feature.Lvl;
            }
            else if (string.Compare(feature.Name, "Сверхъестественная Ловкость") == 0)
                save.agilitySuper += feature.Lvl;

        foreach(MechImplant implant in _implants)
        {
            if(implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.Head)
            {
                save.armorHead += implant.Armor;
                save.armorTotalHead += implant.BonusToughness;
            }
            else if(implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.RightHand)
            {
                save.armorRightHand += implant.Armor;
                save.armorTotalRightHand += implant.BonusToughness;
            }
            else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.LeftHand)
            {
                save.armorLeftHand += implant.Armor;
                save.armorTotalLeftHand += implant.BonusToughness;
            }
            else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.Body)
            {
                save.armorBody += implant.Armor;
                save.armorTotalBody += implant.BonusToughness;
            }
            else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.RightLeg)
            {
                save.armorRightLeg += implant.Armor;
                save.armorRightLeg += implant.BonusToughness;
            }
            else if (implant.Place == MechImplant.PartsOfBody.All || implant.Place == MechImplant.PartsOfBody.LeftLeg)
            {
                save.armorLeftLeg += implant.Armor;
                save.armorLeftLeg += implant.BonusToughness;
            }
        }
                

        List<Armor> armors = new List<Armor>();
        foreach (Equipment equipment in _equipments)
            if (equipment.TypeEq == Equipment.TypeEquipment.Armor)
                armors.Add((Armor)equipment);

        if(armors.Count > 1)
        {
            armors.Sort(
            delegate (Armor armor1, Armor armor2)
            {
                return armor2.DefHead.CompareTo(armor1.DefHead);
            }
            );
            save.armorHead += armors[0].DefHead;

            armors.Sort(
            delegate (Armor armor1, Armor armor2)
            {
                return armor2.DefHands.CompareTo(armor1.DefHands);
            }
            );
            save.armorLeftHand += armors[0].DefHands;
            save.armorRightHand += armors[0].DefHands;

            armors.Sort(
            delegate (Armor armor1, Armor armor2)
            {
                return armor2.DefBody.CompareTo(armor1.DefBody);
            }
            );
            save.armorBody += armors[0].DefBody;

            armors.Sort(
            delegate (Armor armor1, Armor armor2)
            {
                return armor2.DefLegs.CompareTo(armor1.DefLegs);
            }
            );
            save.armorLeftLeg += armors[0].DefLegs;
            save.armorRightLeg += armors[0].DefLegs;
        }
        else if(armors.Count == 1)
        {
            save.armorHead += armors[0].DefHead;
            save.armorLeftHand += armors[0].DefHands;
            save.armorRightHand += armors[0].DefHands;
            save.armorBody += armors[0].DefBody;
            save.armorLeftLeg += armors[0].DefLegs;
            save.armorRightLeg += armors[0].DefLegs;
        }

        save.armorTotalHead += save.armorHead + save.armorAblHead + toughness + save.toughnessSuper;
        save.armorTotalRightHand += save.armorRightHand + save.armorAblRightHand + toughness + save.toughnessSuper;
        save.armorTotalLeftHand += save.armorLeftHand + save.armorAblLeftHand + toughness + save.toughnessSuper;
        save.armorTotalBody += save.armorBody + save.armorAblBody + toughness + save.toughnessSuper;
        save.armorTotalRightLeg += save.armorRightLeg + save.armorAblRightLeg + toughness + save.toughnessSuper;
        save.armorTotalLeftLeg += save.armorLeftLeg + save.armorAblLeftLeg + toughness + save.toughnessSuper;

        agility += save.agilitySuper;

        save.half = agility;
        save.full = save.half * 2;
        save.natisk = save.half * 3;
        save.run = save.half * 6;

        _view.SetArmors(save);
    }

    
}
