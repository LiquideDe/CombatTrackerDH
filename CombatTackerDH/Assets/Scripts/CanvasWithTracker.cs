using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class CanvasWithTracker : MonoBehaviour
{
    public delegate void CreateNewCharacter();
    CreateNewCharacter createNewCharacter;
    [SerializeField] Transform contentPlayingCharacters, contentFeatures, contentListCharacters, contentSmallCharacters;
    [SerializeField] Scrollbar scrollbarCharacters;
    [SerializeField] PanelWithInfo featureExample;
    [SerializeField] CharacterVisual characterExample;
    [SerializeField] GameObject panelWithText;
    [SerializeField] TextMeshProUGUI textInfoPanel;
    [SerializeField] PanelWithCharacter panelWithCharacter;
    [SerializeField] TMP_InputField inputCharacter, inputFeature;
    [SerializeField] SmallCharacter smallCharacterExample;
    private List<PanelWithInfo> properties = new List<PanelWithInfo>();
    private List<PanelWithCharacter> panelWithCharacters = new List<PanelWithCharacter>();
    private List<CharacterVisual> characterVisuals = new List<CharacterVisual>();
    private List<SmallCharacter> smallCharacters = new List<SmallCharacter>();

    public void SetParams(Creators creators, CreateNewCharacter createNewCharacter)
    {
        this.createNewCharacter = createNewCharacter;
        CreateProperty(creators.Skills);
        CreateProperty(creators.Features);
        CreateProperty(creators.Talents);
        CreateProperty(creators.PsyPowers);
        CreateProperty(creators.WeaponProp);
        foreach (Equipment equipment in creators.Equipments)
        {
            properties.Add(Instantiate(featureExample, contentFeatures));
            properties[^1].SetParams(new PropertyCharacter(equipment.Name, equipment.Description), ShowInfo);
        }

        foreach(Character character in creators.Characters)
        {
            panelWithCharacters.Add(Instantiate(panelWithCharacter, contentListCharacters));
            panelWithCharacters[^1].SetParams(character, AddCharacterFromList);
        }
    }

    private void CreateProperty(List<PropertyCharacter> props)
    {
        foreach(PropertyCharacter property in props)
        {
            properties.Add(Instantiate(featureExample, contentFeatures));
            properties[^1].SetParams(property, ShowInfo);
        }
    }
    private void ShowInfo(string description)
    {
        panelWithText.SetActive(true);
        textInfoPanel.text = description;
    }

    public void AddCharacterFromList(Character character)
    {
        characterVisuals.Add(Instantiate(characterExample, contentPlayingCharacters));
        characterVisuals[^1].SetParams(character, characterVisuals.Count - 1, RemoveCharacter, ShowThisFeature, CharacterChangename);

        smallCharacters.Add(Instantiate(smallCharacterExample, contentSmallCharacters));
        smallCharacters[^1].SetParams(character.Name, characterVisuals.Count - 1, ShowThisCharacter);
    }
    private void RemoveCharacter(CharacterVisual characterVisual)
    {
        characterVisuals.Remove(characterVisual);
        Destroy(characterVisual.gameObject);
    }
    public void SearchCharacter()
    {
        SearchFeature(panelWithCharacters, inputCharacter);
    }

    private void ShowThisFeature(string name)
    {
        foreach(PanelWithInfo panel in properties)
        {
            if(string.Compare(panel.Name, name, true) == 0)
            {
                ShowInfo(panel.Description);
                break;
            }
        }
    }
     private void ShowThisCharacter(int id)
    {
        if(characterVisuals.Count > 1)
        {
            float step = 1 / (characterVisuals.Count - 1);
            scrollbarCharacters.value = 1 - step * id;
        }        
    }

    private void CharacterChangename(int id, string name)
    {
        smallCharacters[id].UpdateName(name);
    }

    public void SearchFeature()
    {
        SearchFeature(properties, inputFeature);
    }

    public void SearchFeature<T>(List<T> list, TMP_InputField input)
        where T: IPanel
    {
        foreach(T panel in list)
        {
            if(input.text.Length > 0)
            {
                //Debug.Log($"»щем {input.text} в слове {panel.Name} и получаем {panel.Name.IndexOf(input.text, 0, panel.Name.Length, StringComparison.OrdinalIgnoreCase) >= 0}");
                if (panel.Name.IndexOf(input.text, 0, panel.Name.Length, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    panel.gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddingNewCharacter()
    {
        createNewCharacter?.Invoke();
    }
}
