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
    [SerializeField] SaveBattleScene saveBattle;
    [SerializeField] LoadBattleScene loadBattle;
    private List<PanelWithInfo> properties = new List<PanelWithInfo>();
    private List<PanelWithCharacter> panelWithCharacters = new List<PanelWithCharacter>();
    private List<CharacterVisual> characterVisuals = new List<CharacterVisual>();
    private List<SmallCharacter> smallCharacters = new List<SmallCharacter>();
    private Creators creators;
    private int whoTurn;

    public void SetParams(Creators creators, CreateNewCharacter createNewCharacter)
    {
        this.creators = creators;
        this.createNewCharacter = createNewCharacter;
        CreateProperty(creators.Skills);
        CreateProperty(creators.Features);
        CreateProperty(creators.Talents);
        CreateProperty(creators.PsyPowers);
        CreateProperty(creators.WeaponProp);
        foreach (Equipment equipment in creators.Equipments)
        {
            properties.Add(Instantiate(featureExample, contentFeatures));
            properties[^1].SetParams(new PropertyCharacter(equipment.Name, equipment.Description), ShowThisFeature);
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
            properties[^1].SetParams(property, ShowThisFeature);
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
            float step = 1f / (characterVisuals.Count - 1);
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
            else
            {
                panel.gameObject.SetActive(true);
            }
        }
    }

    public void AddingNewCharacter()
    {
        createNewCharacter?.Invoke();
    }

    public void SaveBattleScene()
    {
        var save = Instantiate(saveBattle, transform);
        save.ShowPanelSave(characterVisuals);
    }

    public void LoadBattleScene()
    {
        var load = Instantiate(loadBattle, transform);
        load.ShowLoads(AddingCharacters);
    }

    private void AddingCharacters(List<SaveLoadCharacterNames> characters)
    {
        foreach(SaveLoadCharacterNames character in characters)
        {
            Character newCharacter = creators.GetCharacterByName(character.internalNamesCharacter);
            newCharacter.Name = character.nameCharacter;
            AddCharacterFromList(newCharacter);
        }
    }
    public void ClearListCharacters()
    {
        foreach (CharacterVisual character in characterVisuals)
        {
            Destroy(character.gameObject);
        }
        characterVisuals.Clear();
        DestroySmallCharacters();
    }

    private void DestroySmallCharacters()
    {
        for (int i = 0; i < smallCharacters.Count; i++)
        {
            Destroy(smallCharacters[i].gameObject);
        }

        smallCharacters.Clear();
    }
    public void StartBattle()
    {
        whoTurn = 0;
        foreach(CharacterVisual character in characterVisuals)
        {
            character.CalculateInitiative();
        }
        characterVisuals.Sort(
            delegate (CharacterVisual cb1, CharacterVisual cb2)
            {
                return cb2.Initiative.CompareTo(cb1.Initiative);
            }
            );
        DestroySmallCharacters();
        SortingCharacters();
    }

    private void SortingCharacters()
    {
        for (int i = 0; i < characterVisuals.Count; i++)
        {
            smallCharacters.Add(Instantiate(smallCharacterExample, contentSmallCharacters));
            smallCharacters[^1].SetParams(characterVisuals[i].Name, i, ShowThisCharacter);
            characterVisuals[i].Id = i;
            characterVisuals[i].transform.SetSiblingIndex(i + 1);
        }
    }

    public void NextTurn()
    {
        if (whoTurn < characterVisuals.Count)
        {
            if (whoTurn > 0)
            {
                characterVisuals[whoTurn - 1].CharacterEndTurn();
                smallCharacters[whoTurn - 1].CharacterEndTurn();
            }
            else
            {
                characterVisuals[^1].CharacterEndTurn();
                smallCharacters[^1].CharacterEndTurn();
            }
            ShowThisCharacter(whoTurn);
            characterVisuals[whoTurn].CharacterTurn();
            smallCharacters[whoTurn].CharacterTurn();
            whoTurn += 1;
        }
        else
        {
            whoTurn = 0;
            NextTurn();
        }

    }

    public void FinishBattle()
    {
        if (whoTurn > 0)
        {
            characterVisuals[whoTurn - 1].CharacterEndTurn();
            smallCharacters[whoTurn - 1].CharacterEndTurn();
        }
        else
        {
            characterVisuals[^1].CharacterEndTurn();
            smallCharacters[^1].CharacterEndTurn();
        }
    }

}
