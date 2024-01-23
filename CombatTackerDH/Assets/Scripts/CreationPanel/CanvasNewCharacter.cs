using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CanvasNewCharacter : MonoBehaviour
{
    public delegate void ReturnCharacter(Character character);
    ReturnCharacter returnCharacter;
    [SerializeField] PropList propListExample;
    [SerializeField] PanelWithInfo panelInfoExample;
    [SerializeField] Transform contentProp;
    [SerializeField] PanelCreation panelCreationProp, panelCreationEquipment, panelCreationArmor, panelCreationGun, panelCreationMelee, panelCreationGrenade;
    [SerializeField] PanelCreationCharacter creationCharacter;
    Creators creators;
    private List<PanelWithInfo> propPanels = new List<PanelWithInfo>();
    public void SetParams(Creators creators, ReturnCharacter returnCharacter)
    {
        this.returnCharacter = returnCharacter;
        this.creators = creators;
        creationCharacter.SetParams();
    }
    public void ShowSkills()
    {
        PropList list = Instantiate(propListExample, transform);
        list.SetParams(creators.Skills, AddSkill, panelCreationProp);
    }
    private void AddSkill(PropertyCharacter property)
    {
        creationCharacter.Character.Skills.Add(property);
        AddProp(property);        
    }

    public void ShowTalents()
    {
        PropList list = Instantiate(propListExample, transform);
        list.SetParams(creators.Talents, AddTalent, panelCreationProp);
    }
    private void AddTalent(PropertyCharacter property)
    {
        creationCharacter.Character.Talents.Add(property);
        AddProp(property);
        if (!CheckPropForExisting(creators.Talents, property))
        {
            SaveProperty($"{Application.dataPath}/StreamingAssets/Talents/{property.Name}", property);
        }
    }
    public void ShowFeature()
    {
        PropList list = Instantiate(propListExample, transform);
        list.SetParams(creators.Features, AddFeature, panelCreationProp);
    }
    private void AddFeature(PropertyCharacter property)
    {
        creationCharacter.Character.Features.Add(property);
        AddProp(property);
        if (!CheckPropForExisting(creators.Features, property))
        {
            SaveProperty($"{Application.dataPath}/StreamingAssets/Features/{property.Name}", property);
        }
    }
    public void ShowPsy()
    {
        PropList list = Instantiate(propListExample, transform);
        list.SetParams(creators.PsyPowers, AddPsy, panelCreationProp);
    }
    private void AddPsy(PropertyCharacter property)
    {
        creationCharacter.Character.PsyPowers.Add(property);
        AddProp(property);
        if (!CheckPropForExisting(creators.PsyPowers, property))
        {
            SaveProperty($"{Application.dataPath}/StreamingAssets/PsyPowers/{property.Name}", property);
        }
    }
    public void ShowEquipment()
    {
        PropList list = Instantiate(propListExample, transform);
        List<PropertyCharacter> propEquipments = new List<PropertyCharacter>();
        foreach(Equipment equipment in creators.Equipments)
        {
            if(equipment.TypeEq == Equipment.TypeEquipment.Thing)
            {
                propEquipments.Add(new PropertyEquipment(equipment.Name, "", equipment.Weight, equipment.Rarity));
            }            
        }
        list.SetParams(propEquipments, AddEquipment, panelCreationProp);
    }
    private void AddEquipment(PropertyCharacter property)
    {
        if (CheckEquipmentForExisting(creators.Equipments, property.Name))
        {
            creationCharacter.Character.Equipments.Add(FindEquipment(creators.Equipments,property.Name));            
        }
        else
        {
            PropertyEquipment propEquipment = (PropertyEquipment)property;
            Equipment equipment = new Equipment(propEquipment.Name, propEquipment.Description, propEquipment.Rarity, propEquipment.Weight);
            creationCharacter.Character.Equipments.Add(equipment);
            JSONEquipmentReader jSONEquipment = new JSONEquipmentReader();
            jSONEquipment.description = equipment.Description;
            jSONEquipment.name = equipment.Name;
            jSONEquipment.rarity = equipment.Rarity;
            jSONEquipment.weight = equipment.Weight;
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(jSONEquipment));
            File.WriteAllLines($"{Application.dataPath}/StreamingAssets/Equipments/Things/{equipment.Name}.JSON", data);
        }
        AddProp(property);
    }

    public void ShowArmor()
    {
        PropList list = Instantiate(propListExample, transform);
        List<PropertyCharacter> propArmor = new List<PropertyCharacter>();
        foreach (Equipment equipment in creators.Equipments)
        {
            if(equipment.TypeEq == Equipment.TypeEquipment.Armor)
            {
                Armor armor = (Armor)equipment;
                propArmor.Add(new PropertyArmor(armor.Name, "", armor.Weight, armor.Rarity, armor.DefHead, armor.DefHands, armor.DefBody, armor.DefLegs));
            }            
        }
        list.SetParams(propArmor, AddArmor, panelCreationArmor);
    }

    private void AddArmor(PropertyCharacter property)
    {
        if (CheckEquipmentForExisting(creators.Equipments, property.Name))
        {
            Armor armor = (Armor)FindEquipment(creators.Equipments, property.Name);
            creationCharacter.AddArmor(armor);
        }
        else
        {
            PropertyArmor propArmor = (PropertyArmor)property;  
            JSONArmorReader jSONArmor = new JSONArmorReader();
            jSONArmor.description = propArmor.Description;
            jSONArmor.name = propArmor.Name;
            jSONArmor.rarity = propArmor.Rarity;
            jSONArmor.weight = propArmor.Weight;
            jSONArmor.head = propArmor.Head;
            jSONArmor.hands = propArmor.Hands;
            jSONArmor.body = propArmor.Body;
            jSONArmor.legs = propArmor.Legs;
            Armor armor = new Armor(jSONArmor);
            creationCharacter.AddArmor(armor);
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(jSONArmor));
            File.WriteAllLines($"{Application.dataPath}/StreamingAssets/Equipments/Armor/{propArmor.Name}.JSON", data);
        }
        AddProp(property);
    }

    public void ShowMelee()
    {
        PropList list = Instantiate(propListExample, transform);
        List<PropertyCharacter> propMelee = new List<PropertyCharacter>();
        foreach (Equipment equipment in creators.Equipments)
        {
            if (equipment.TypeEq == Equipment.TypeEquipment.Melee)
            {
                Weapon weapon = (Weapon)equipment;
                propMelee.Add(new PropertyMelee(weapon.Name, "", weapon.Weight, weapon.Rarity, weapon.ClassWeapon, weapon.Damage, weapon.Penetration, weapon.Properties));
            }
        }
        list.SetParams(propMelee, AddMelee, panelCreationMelee);
    }

    private void AddMelee(PropertyCharacter property)
    {
        Weapon weapon;
        if (CheckEquipmentForExisting(creators.Equipments, property.Name))
        {
            weapon = (Weapon)FindEquipment(creators.Equipments, property.Name);
        }
        else
        {
            PropertyMelee propArmor = (PropertyMelee)property;
            JSONMeleeReader jSONMelee = new JSONMeleeReader();
            jSONMelee.description = propArmor.Description;
            jSONMelee.name = propArmor.Name;
            jSONMelee.rarity = propArmor.Rarity;
            jSONMelee.weight = propArmor.Weight;
            jSONMelee.weaponClass = propArmor.ClassWeapon;
            jSONMelee.damage = propArmor.Damage;
            jSONMelee.penetration = propArmor.Penetration;
            jSONMelee.properties = propArmor.Propetries;

            weapon = new Weapon(jSONMelee);
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(jSONMelee));
            File.WriteAllLines($"{Application.dataPath}/StreamingAssets/Equipments/Weapons/Melee/{propArmor.Name}.JSON", data);
        }
        creationCharacter.AddWeapon(weapon);
    }

    public void ShowGun()
    {
        PropList list = Instantiate(propListExample, transform);
        List<PropertyCharacter> propRange = new List<PropertyCharacter>();
        foreach (Equipment equipment in creators.Equipments)
        {
            if (equipment.TypeEq == Equipment.TypeEquipment.Range)
            {
                Weapon weapon = (Weapon)equipment;
                propRange.Add(new PropertyGun(weapon.Name, "", weapon.Weight, weapon.Rarity, weapon.ClassWeapon, weapon.Damage, weapon.Penetration, weapon.Properties, weapon.Range, weapon.Rof, weapon.Clip, weapon.Reload));
            }
        }
        list.SetParams(propRange, AddGun, panelCreationGun);
    }

    public void AddGun(PropertyCharacter property)
    {
        Weapon weapon;
        if (CheckEquipmentForExisting(creators.Equipments, property.Name))
        {
            weapon = (Weapon)FindEquipment(creators.Equipments, property.Name);
        }
        else
        {
            PropertyGun propGun = (PropertyGun)property;
            JSONRangeReader jSONGun = new JSONRangeReader();
            jSONGun.description = propGun.Description;
            jSONGun.name = propGun.Name;
            jSONGun.rarity = propGun.Rarity;
            jSONGun.weight = propGun.Weight;
            jSONGun.weaponClass = propGun.ClassWeapon;
            jSONGun.damage = propGun.Damage;
            jSONGun.penetration = propGun.Penetration;
            jSONGun.properties = propGun.Propetries;
            jSONGun.range = propGun.Range;
            jSONGun.rof = propGun.Rof;
            jSONGun.clip = propGun.Clip;
            jSONGun.reload = propGun.Reload;

            weapon = new Weapon(jSONGun);
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(jSONGun));
            File.WriteAllLines($"{Application.dataPath}/StreamingAssets/Equipments/Weapons/Range/{propGun.Name}.JSON", data);
        }
        creationCharacter.AddWeapon(weapon);
    }
    public void ShowGrenade()
    {
        PropList list = Instantiate(propListExample, transform);
        List<PropertyCharacter> propGrenade = new List<PropertyCharacter>();
        foreach (Equipment equipment in creators.Equipments)
        {
            if (equipment.TypeEq == Equipment.TypeEquipment.Grenade)
            {
                Weapon weapon = (Weapon)equipment;
                propGrenade.Add(new PropertyGrenade(weapon.Name, "", weapon.Weight, weapon.Rarity, weapon.ClassWeapon, weapon.Damage, weapon.Penetration, weapon.Properties));

            }
        }
        list.SetParams(propGrenade, AddGrenade, panelCreationGrenade);
    }

    private void AddGrenade(PropertyCharacter property)
    {
        Weapon weapon;
        if (CheckEquipmentForExisting(creators.Equipments, property.Name))
        {
            weapon = (Weapon)FindEquipment(creators.Equipments, property.Name);
        }
        else
        {
            PropertyGrenade propArmor = (PropertyGrenade)property;
            JSONGrenadeReader jSONGrenade = new JSONGrenadeReader();
            jSONGrenade.description = propArmor.Description;
            jSONGrenade.name = propArmor.Name;
            jSONGrenade.rarity = propArmor.Rarity;
            jSONGrenade.weight = propArmor.Weight;
            jSONGrenade.weaponClass = propArmor.ClassWeapon;
            jSONGrenade.damage = propArmor.Damage;
            jSONGrenade.penetration = propArmor.Penetration;
            jSONGrenade.properties = propArmor.Propetries;

            weapon = new Weapon(jSONGrenade);
            List<string> data = new List<string>();
            data.Add(JsonUtility.ToJson(jSONGrenade));
            File.WriteAllLines($"{Application.dataPath}/StreamingAssets/Equipments/Weapons/Grenade/{propArmor.Name}.JSON", data);
        }
        creationCharacter.AddWeapon(weapon);
    }
    private void AddProp(PropertyCharacter property)
    {
        propPanels.Add(Instantiate(panelInfoExample, contentProp));
        propPanels[^1].SetParams(property, RemoveProperty);
    }

    private bool CheckPropForExisting(List<PropertyCharacter> properties, PropertyCharacter property)
    {
        foreach(PropertyCharacter prop in properties)
        {
            if(string.Compare(prop.Name, property.Name) == 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckEquipmentForExisting(List<Equipment> equipments, string name)
    {
        foreach(Equipment equipment in equipments)
        {
            if(string.Compare(equipment.Name, name, true) == 0)
            {
                return true;
            }
        }

        return false;
    }

    private Equipment FindEquipment(List<Equipment> equipments, string name)
    {
        foreach (Equipment equipment in equipments)
        {
            if (string.Compare(equipment.Name, name, true) == 0)
            {
                return equipment;
            }
        }

        return null;
    }

    private void RemoveProperty(string name)
    {
        foreach(PanelWithInfo panel in propPanels)
        {
            if(string.Compare(name, panel.Name, true) == 0)
            {
                Destroy(panel.gameObject);
                propPanels.Remove(panel);
                break;
            }
        }
    }

    private void SaveProperty(string path, PropertyCharacter property)
    {
        Directory.CreateDirectory(path);
        JSONReader jSONReader = new JSONReader();
        jSONReader.name = property.Name;
        List<string> data = new List<string>();
        data.Add(JsonUtility.ToJson(jSONReader));
        File.WriteAllLines($"{path}/Param.JSON", data);
        data.Clear();
        data.Add(property.Description);
        File.WriteAllLines($"{path}/Описание.txt", data);
    }

    public void SaveCharacter()
    {
        if (creationCharacter.SaveCharacter())
        {
            returnCharacter?.Invoke(creationCharacter.Character);
            Destroy(gameObject);
        }
    }
}
