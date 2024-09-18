using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using System;

public class NewMelee : CreatorNewEquipment
{
    public event Action NeedInProperties;
    [SerializeField] protected Button _buttonAddProperty;
    public TMP_InputField inputClass, inputDamage, inputBonusDamage, inputPenetration;
    public TMP_Dropdown dropDown;
    public Transform content;
    public ItemWithNumberInList itemPrefab;
    List<ItemWithNumberInList> items = new List<ItemWithNumberInList>();

    public override void Initialize()
    {
        base.Initialize();
        _buttonAddProperty.onClick.AddListener(NeedInPropertiesPressed);
    }

    public override void FinishCreating()
    {
        if (_inputName.text != "" && inputClass.text.Length > 0 && inputPenetration.text.Length > 0 && _inputWeight.text.Length > 0)
        {
            float.TryParse(_inputWeight.text, out float weight);
            int.TryParse(inputPenetration.text, out int penetration);
            JSONMeleeReader meleeReader = new JSONMeleeReader();
            meleeReader.name = _inputName.text;
            meleeReader.penetration = penetration;
            meleeReader.properties = TranslatePropertiesToText();
            meleeReader.weaponClass = inputClass.text;
            meleeReader.weight = weight;
            meleeReader.damage = MakeDamageText();
            meleeReader.typeEquipment = Equipment.TypeEquipment.Melee.ToString();
            meleeReader.amount = 1;
            meleeReader.rarity = _inputRarity.text;

            SaveEquipment($"{Application.dataPath}/StreamingAssets/Equipments/Weapons/Melee/{meleeReader.name}.JSON", meleeReader);

            Weapon weapon = new Weapon(meleeReader);
            SendEquipment(weapon);
        }
        else
            WrongInputPressed();
    }

    public override void AddProperty(string property)
    {
        ItemWithNumberInList item = Instantiate(itemPrefab, content);
        item.RemoveThisItem += RemoveThisProperty;
        item.Initialize(property, 0);
        items.Add(item);
    }

    protected string MakeDamageText()
    {
        string typeDamage = "";
        switch (dropDown.value)
        {
            case 0:
                typeDamage = "�";
                break;
            case 1:
                typeDamage = "�";
                break;
            case 2:
                typeDamage = "�";
                break;
            case 3:
                typeDamage = "�";
                break;
        }
        int.TryParse(inputDamage.text, out int damage);
        int.TryParse(inputBonusDamage.text, out int bonusDamage);
        if(damage == 0)
        {
            damage = 1;
        }
        string textdamage = $"{damage}�10+{bonusDamage}{typeDamage}";
        return textdamage;
    }
     
    private void RemoveThisProperty(string name)
    {
        foreach(ItemWithNumberInList item in items)
        {
            if(string.Compare(item.Name, name, true) == 0)
            {
                items.Remove(item);
                Destroy(item.gameObject);                
                break;
            }
        }
    }

    protected string TranslatePropertiesToText()
    {
        string properties = "";
        foreach(ItemWithNumberInList item in items)
        {
            int.TryParse(item.Lvl, out int lvl);
            if(lvl != 0)
            {
                properties += $"{item.Name}({lvl}),";
            }
            else
            {
                properties += $"{item.Name},";
            }
            
        }
        properties = properties.TrimEnd(',');
        return properties;
    }

    private void NeedInPropertiesPressed() => NeedInProperties?.Invoke();

}
