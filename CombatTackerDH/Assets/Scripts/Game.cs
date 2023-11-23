using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] TMP_InputField head, leftHand, rightHand, body, leftLeg, rightLeg, health, bonusAgility, bonusFatigue, textName;
    [SerializeField] GameObject panel, content;
    [SerializeField] Character character;
    private List<Character> characters = new List<Character>();
    

    public void CreateCharacter()
    {
        panel.SetActive(false);
        characters.Add(Instantiate(character, content.transform));
        characters[^1].gameObject.SetActive(true);
        characters[^1].SetParams(int.Parse(head.text), int.Parse(leftHand.text), int.Parse(rightHand.text), int.Parse(body.text), int.Parse(leftLeg.text), int.Parse(rightLeg.text), 
            int.Parse(health.text), int.Parse(bonusAgility.text), int.Parse(bonusFatigue.text), textName.text);
    }

    public void ShowPanelForNewCharacter()
    {
        panel.SetActive(true);
    }
}
