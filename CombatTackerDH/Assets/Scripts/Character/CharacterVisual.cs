using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private List<PanelWithInfo> feautures = new List<PanelWithInfo>();
    private List<WeaponVisual> weapons = new List<WeaponVisual>();
    private Character character;
    private int initiative, wounds, treate;
    private int[] bonusCharacteristics;
    private int[] characteristics = new int[10];
    private int[] unNatCharacteristics = new int[10];
    private int[] armorPoints = new int[6];
    private int[] armorAblPoints = new int[6];
    private int[] armorTotalPoints = new int[6];
    private int id;

    public int Id { get => id; }

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
            weapons[^1].SetParams(weapon, ShowInfoFromInfoPanel);
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
            if(bonusCharacteristics[3] > unNatCharacteristics[3])
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
}
