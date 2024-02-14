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
        if(character != null)
        {
            mainCanvas.AddCharacterFromList(character);
        }
        
    }
    IEnumerator StartTracker()
    {
        creators = new Creators();
        yield return new WaitForSeconds(1);
        mainCanvas.SetParams(creators, CreatenewCharacter);
    }
    
}
