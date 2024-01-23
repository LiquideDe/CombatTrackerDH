using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game : MonoBehaviour
{

    [SerializeField] CanvasWithTracker mainCanvas;
    [SerializeField] CanvasNewCharacter newCharacter;
    Creators creators;

    private void Start()
    {
        StartCoroutine(StartTracker());
        
    }

    private void CreatenewCharacter()
    {
        mainCanvas.gameObject.SetActive(false);
        CanvasNewCharacter canvasNew=  Instantiate(newCharacter);
        canvasNew.SetParams(creators, AddCharacter);
    }

    private void AddCharacter(Character character)
    {
        mainCanvas.gameObject.SetActive(true);
        mainCanvas.AddCharacterFromList(character);
    }
    IEnumerator StartTracker()
    {
        creators = new Creators();
        yield return new WaitForSeconds(1);
        mainCanvas.SetParams(creators, CreatenewCharacter);
    }
    /*
    [SerializeField] TMP_InputField head, leftHand, rightHand, body, leftLeg, rightLeg, health, bonusAgility, bonusFatigue, textName, clip;
    [SerializeField] GameObject panel, content, startB, finishB, nextB;
    //[SerializeField] Character character;
    //private List<Character> characters = new List<Character>();
    private List<TMP_InputField> inputs = new List<TMP_InputField>();
    public static event Action BattleStarted;
    public static event Action BattleFinished;
    private int whoTurn;

    private void Awake()
    {
        inputs.Add(head);
        inputs.Add(leftHand);
        inputs.Add(rightHand);
        inputs.Add(body);
        inputs.Add(leftLeg);
        inputs.Add(rightLeg);
        inputs.Add(health);
        inputs.Add(bonusAgility);
        inputs.Add(bonusFatigue);
        inputs.Add(textName);
        inputs.Add(clip);
    }

    public void CreateCharacter()
    {
        panel.SetActive(false);
        characters.Add(Instantiate(character, content.transform));
        characters[^1].gameObject.SetActive(true);
        if (CheckInputs())
        {
            characters[^1].SetParams(int.Parse(head.text), int.Parse(leftHand.text), int.Parse(rightHand.text), int.Parse(body.text), int.Parse(leftLeg.text), int.Parse(rightLeg.text),
            int.Parse(health.text), int.Parse(bonusAgility.text), int.Parse(bonusFatigue.text), textName.text, int.Parse(clip.text), DeleteCharacter);
        }
        
    }

    public void ShowPanelForNewCharacter()
    {
        panel.SetActive(true);
    }

    private bool CheckInputs()
    {
        foreach (TMP_InputField input in inputs)
        {
            if(input.text == "")
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteCharacter(Character character)
    {
        characters.Remove(character);
    }

    public void StartBattle()
    {
        whoTurn = 0;
        BattleStarted?.Invoke();
        characters.Sort(
            delegate (Character cb1, Character cb2)
            {
                return cb2.Init.CompareTo(cb1.Init);
            }
            );
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].transform.SetSiblingIndex(i + 1);
        }
        startB.SetActive(false);
        finishB.SetActive(true);
        nextB.SetActive(true);
    }

    public void FinishBattle()
    {
        BattleFinished?.Invoke();
        startB.SetActive(true);
        finishB.SetActive(false);
        nextB.SetActive(false);
    }

    public void NextTurn()
    {
        if (whoTurn < characters.Count)
        {
            if (whoTurn > 0)
            {
                characters[whoTurn - 1].CharacterEndTurn();
            }
            else
            {
                characters[characters.Count - 1].CharacterEndTurn();
            }
            characters[whoTurn].CharacterTurn();
            whoTurn += 1;
        }
        else
        {
            whoTurn = 0;
            NextTurn();
        }

    }*/
}
