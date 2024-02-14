using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PanelCreationCharacter : MonoBehaviour
{
    [SerializeField] TMP_InputField inputName, inputWound, inputHalf, inputFull, inputNatisk, inputRun, inputTreate;
    [SerializeField] TMP_InputField[] inputArmorPoint;
    [SerializeField] TMP_InputField[] inputAblArmorPoint;
    [SerializeField] TextMeshProUGUI[] textTotalArmorPoint;
    [SerializeField] TMP_InputField[] inputCharacteristics;
    [SerializeField] TMP_InputField[] inputUnnatCharacteristics;
    [SerializeField] WeaponSimple weaponSimpleExample;
    [SerializeField] Transform contentWeapons;
    int[] armorPoints = new int[6];
    int[] armorAblPoints = new int[6];
    int bonusToughness = 0;

    Character character;
    public Character Character { get => character; }
    List<WeaponSimple> weapons = new List<WeaponSimple>();

    public void SetParams()
    {
        character = new Character();
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(Instantiate(weaponSimpleExample, contentWeapons));
        weapons[^1].SetParams(weapon);
        character.Weapons.Add(weapon);
    }

    public void AddArmor(Armor armor)
    {
        character.Armors.Add(armor);
        armorPoints[0] = armor.DefHead;
        armorPoints[1] = armor.DefHands;
        armorPoints[2] = armor.DefHands;
        armorPoints[3] = armor.DefBody;
        armorPoints[4] = armor.DefLegs;
        armorPoints[5] = armor.DefLegs;

        for(int i = 0; i < armorPoints.Length; i++)
        {
            inputArmorPoint[i].text = armorPoints[i].ToString();
        }

        ReCalculatingArmor();
    }

    public void ChangeToughness()
    {
        CancelSelect();
        ReCalculateToughness();
    }

    public void ChangeAgility()
    {
        CancelSelect();
        int.TryParse(inputCharacteristics[4].text, out int speed);
        speed = speed / 10;
        inputHalf.text = speed.ToString();
        inputFull.text = $"{speed * 2}";
        inputNatisk.text = $"{speed * 3}";
        inputRun.text = $"{speed * 6}";
    }

    public void ChangeArmor()
    {
        CancelSelect();
        for(int i = 0; i < armorPoints.Length; i++)
        {
            int.TryParse(inputArmorPoint[i].text, out armorPoints[i]);
            int.TryParse(inputAblArmorPoint[i].text, out armorAblPoints[i]);
        }
        ReCalculatingArmor();
    }

    private void CancelSelect()
    {
        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
    }
    
    private void ReCalculatingArmor()
    {
        for(int i = 0; i < armorPoints.Length; i++)
        {
            textTotalArmorPoint[i].text = $"{armorPoints[i] + bonusToughness + armorAblPoints[i]}";
        }
    }

    private void ReCalculateToughness()
    {
        int.TryParse(inputCharacteristics[3].text, out int toughness);
        int.TryParse(inputUnnatCharacteristics[3].text, out int unNatToughness);
        toughness = toughness / 10;
        if(unNatToughness == 0)
        {
            bonusToughness = toughness;
        }
        else
        {
            bonusToughness = unNatToughness;
        }
        ReCalculatingArmor();
    }

    public bool SaveCharacter()
    {
        if(inputName.text.Length > 0)
        {
            character.InternalName = inputName.text;
            character.ArmorPoints = armorPoints;
            character.ArmorPointsAbl = armorAblPoints;
            for(int i = 0; i < inputCharacteristics.Length; i++)
            {
                int.TryParse(inputCharacteristics[i].text, out character.Characteristics[i]);
            }

            for(int i = 0; i < inputUnnatCharacteristics.Length; i++)
            {
                int.TryParse(inputUnnatCharacteristics[i].text, out character.UnnaturualChar[i]);
            }
            
            character.Name = inputName.text;
            int.TryParse(inputTreate.text, out int treate);
            character.Treate = treate;
            int.TryParse(inputWound.text, out int wound);
            character.Wounds = wound;
            character.SaveCharacter();
            return true;
        }
        return false;
    }
}
