using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] TMP_InputField[] inputArmorPoints;
    [SerializeField] TMP_InputField[] inputArmorAbl;
    [SerializeField] TextMeshProUGUI[] textArmorPointsTotal;
    [SerializeField] TextMeshProUGUI[] textCharacteristics;
    [SerializeField] TextMeshProUGUI[] textUnCharacteristics;
    [SerializeField] TextMeshProUGUI textHalf, textFull, textNatisk, textRun, textWound;
    [SerializeField] PanelWithInfo featureExample;
    [SerializeField] Transform contentFeatures, contentWeapons;
    [SerializeField] WeaponVisual weaponExample;
    [SerializeField] TMP_InputField inputName, inputInitiative;
    [SerializeField] Image background;
    [SerializeField] DamagePanel damagePanel;

    private List<PanelWithInfo> feautures = new List<PanelWithInfo>();
    private List<WeaponVisual> weapons = new List<WeaponVisual>();
    private Character character;
    private int initiative, wounds, treate;
    private int[] bonusCharacteristics = new int[10];
    private int[] characteristics = new int[10];
    private int[] unNatCharacteristics = new int[10];
    private int[] armorPoints = new int[6];
    private int[] armorAblPoints = new int[6];
    private int[] armorTotalPoints = new int[6];
    private int id;

    public int Id { get => id; set => id = value; }
    public string InternalName { get => character.InternalName; }
    public string Name { get => inputName.text; }
    public int Initiative { get => initiative; }

    public delegate void DeleteThisCharacter(CharacterVisual characterVisual);
    DeleteThisCharacter deleteThisCharacter;

    public delegate void ShowThisThing(string name);
    ShowThisThing showThisThing;

    public delegate void ChangeNameCharacter(int id, string name);
    ChangeNameCharacter changeName;

    public void SetParams(Character character,int id, DeleteThisCharacter deleteThisCharacter, ShowThisThing showThisThing, ChangeNameCharacter changeName)
    {
        gameObject.SetActive(true);
        this.id = id;
        this.character = character;
        this.deleteThisCharacter = deleteThisCharacter;
        this.showThisThing = showThisThing;
        this.changeName = changeName;
        inputName.text = character.Name;
        for(int i = 0; i < characteristics.Length; i++)
        {
            characteristics[i] = character.Characteristics[i];
        }

        for (int i = 0; i < unNatCharacteristics.Length; i++)
        {
            unNatCharacteristics[i] = character.UnnaturualChar[i];
        }

        for (int i = 0; i < armorPoints.Length; i++)
        {
            armorPoints[i] = character.ArmorPoints[i];
        }

        for (int i = 0; i < armorPoints.Length; i++)
        {
            armorAblPoints[i] = character.ArmorPointsAbl[i];
        }

        wounds = character.Wounds;
        treate = character.Treate;

        CreateFeatures(character.Skills);
        CreateFeatures(character.Talents);
        CreateFeatures(character.Features);
        CreateFeatures(character.PsyPowers);

        foreach (Equipment equipment in character.Equipments)
        {
            feautures.Add(Instantiate(featureExample, contentFeatures));
            feautures[^1].SetParams(new PropertyCharacter(equipment.Name, equipment.Description), ShowInfoFromInfoPanel);
        }

        foreach(Armor armor in character.Armors)
        {
            feautures.Add(Instantiate(featureExample, contentFeatures));
            feautures[^1].SetParams(new PropertyCharacter(armor.Name, armor.Description), ShowInfoFromInfoPanel);
        }

        foreach(Weapon weapon in character.Weapons)
        {
            weapons.Add(Instantiate(weaponExample, contentWeapons));
            weapons[^1].SetParams(weapon);
            weapons[^1].Init(ShowInfoFromInfoPanel);
        }

        CalculateBonuses();
        CalculateTotalArmorPoints();

        for(int i = 0; i < textCharacteristics.Length; i++)
        {
            textCharacteristics[i].text = $"{characteristics[i]}";
        }

        for(int i = 0; i < textUnCharacteristics.Length; i++)
        {
            if(unNatCharacteristics[i] > 0)
            {
                textUnCharacteristics[i].text = $"{unNatCharacteristics[i]}";
            }
            else
            {
                textUnCharacteristics[i].text = "";
            }
        }

        for(int i = 0; i < inputArmorPoints.Length; i++)
        {
            if(armorPoints[i] > 0)
            {
                inputArmorPoints[i].text = $"{armorPoints[i]}";
            }
            else
            {
                inputArmorPoints[i].text = "";
            }
            
            if(armorAblPoints[i] > 0)
            {
                inputArmorAbl[i].text = $"{armorAblPoints[i]}";
            }
            else
            {
                inputArmorAbl[i].text = "";
            }
        }
        UpdateWounds();
    }

    private void CreateFeatures(List<PropertyCharacter> properties)
    {
        foreach (PropertyCharacter property in properties)
        {
            feautures.Add(Instantiate(featureExample, contentFeatures));
            feautures[^1].SetParams(property, ShowInfoFromInfoPanel);
        }
    }

    private void ShowInfoFromInfoPanel(string name)
    {
        showThisThing?.Invoke(name);
    }
    private void CalculateBonuses()
    {
        for(int i = 0; i < characteristics.Length; i++)
        {
            bonusCharacteristics[i] = characteristics[i] / 10;
        }
    }

    private void CalculateTotalArmorPoints()
    {
        for(int i = 0; i < armorPoints.Length; i++)
        {
            armorTotalPoints[i] = armorPoints[i] + armorAblPoints[i];
            if(unNatCharacteristics[3]==0)
            {
                armorTotalPoints[i] += bonusCharacteristics[3];
            }
            else
            {
                armorTotalPoints[i] += unNatCharacteristics[3];
            }
        }
        UpdateArmorsText();
    }

    private void UpdateArmorsText()
    {        
        for(int i = 0; i < textArmorPointsTotal.Length; i++)
        {
            textArmorPointsTotal[i].text = $"{armorTotalPoints[i]}";
        }
    }

    private void UpdateWounds()
    {
        textWound.text = $"{wounds}";
    }

    private void CancelSelect()
    {
        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
    }

    public void ReCalculateArmors()
    {
        CancelSelect();
        for (int i = 0; i < inputArmorPoints.Length; i++)
        {
            int.TryParse(inputArmorPoints[i].text, out armorPoints[i]);
            int.TryParse(inputArmorAbl[i].text, out armorAblPoints[i]);
        }

        CalculateTotalArmorPoints();
    }

    public void ChangeName()
    {
        CancelSelect();
        changeName?.Invoke(id, inputName.text);
    }

    public void CalculateInitiative()
    {
        if(inputInitiative.text.Length < 1)
        {
            System.Random rand = new System.Random();
            initiative = rand.Next(1, 10);
            initiative += bonusCharacteristics[4];
        }
        else
        {
            int.TryParse(inputInitiative.text, out initiative);
        }
    }

    public void CharacterEndTurn()
    {
        background.color = Color.black;
    }

    public void CharacterTurn()
    {
        background.color = Color.white;
    }

    public void ShowDamagePanel()
    {
        var dampanel = Instantiate(damagePanel, transform);
        dampanel.SetParams(GetDamage);
    }

    private void GetDamage(List<Bullet> bullets)
    {
        Debug.Log($"Получили пули");
        foreach(Bullet item in bullets)
        {
            Debug.Log($"Прилетела пуля в {item.Place} с бронебойностью {item.Penetration}, урон {item.Damage}");
            if (item.Place < 10)
            {
                Damage(armorPoints[0] + armorAblPoints[0], item); //0 - голова
            }
            else if (item.Place > 10 && item.Place < 21)
            {
                Damage(armorPoints[1] + armorAblPoints[1], item); //1 - правая рука
            }
            else if (item.Place > 20 && item.Place < 31)
            {
                Damage(armorPoints[2] + armorAblPoints[2], item); //2 - левая рука
            }
            else if (item.Place > 30 && item.Place < 71)
            {
                Damage(armorPoints[3] + armorAblPoints[3], item);//3 - тело
            }
            else if (item.Place > 70 && item.Place < 86)
            {
                Damage(armorPoints[4] + armorAblPoints[4], item);//4 - правая нога
            }
            else if (item.Place > 85 && item.Place < 101)
            {
                Damage(armorPoints[5] + armorAblPoints[5], item);//5 - левая нога
            }
        }
    }
    private void Damage(int armor, Bullet item)
    {
        int toughness;
        if (unNatCharacteristics[3] == 0)
        {
            toughness = bonusCharacteristics[3];
            Debug.Log($"Тафна = {toughness}");
        }
        else
        {
            toughness = unNatCharacteristics[3];
        }
        int damage = 0;
        if (!item.IsIgnoreArmor && !item.IsIgnoreToughness && !item.IsWarpWeapon)
        {

            if (armor < item.Penetration)
            {
                damage = item.Damage - toughness;
                Debug.Log($"Урон вышел {damage}");
            }
            else
            {
                armor -= item.Penetration;
                damage = item.Damage - (armor + toughness);
                Debug.Log($"Урон вышел {damage} = {item.Damage} - ({armor} - {item.Penetration} + {toughness})");
            }
        }
        else if (item.IsIgnoreArmor && !item.IsIgnoreToughness && !item.IsWarpWeapon)
        {
            damage = item.Damage - toughness;
        }
        else if (!item.IsIgnoreArmor && item.IsIgnoreToughness && !item.IsWarpWeapon)
        {
            if (armor >= item.Penetration)
            {
                armor -= item.Penetration;
                armor = armor < 0 ? armor = 0 : armor;
                damage = item.Damage - (armor - item.Penetration);
            }                
            else
                damage = item.Damage;
        }
        else if (item.IsWarpWeapon)
        {
            damage = item.Damage - characteristics[7];
        }
        else if (item.IsIgnoreArmor && item.IsIgnoreToughness && !item.IsWarpWeapon)
        {
            damage = item.Damage;
        }

        Debug.Log($"Урон = '{damage}'");
        if (damage > 0)
        {
            Debug.Log($"Здоровье до {wounds}");
            wounds -= damage;
            Debug.Log($"Здоровье после {wounds}");
            UpdateWounds();
        }
    }
}
