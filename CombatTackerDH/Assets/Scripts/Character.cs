using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textName, textHealth;
    int head, leftHand, rightHand, body, leftLeg, rightLeg, health, bonusAgility, bonusFatigue, iniciative;
    string nameCharacter;

    public void SetParams(int head, int leftHand, int rightHand, int body, int leftLeg, int rightLeg, int health, int bonusAgility, int bonusFatigue, string name)
    {
        this.head = head;
        this.leftHand = leftHand;
        this.rightHand = rightHand;
        this.body = body;
        this.leftLeg = leftLeg;
        this.rightLeg = rightLeg;
        this.health = health;
        this.bonusAgility = bonusAgility;
        this.bonusFatigue = bonusFatigue;
        nameCharacter = name;
        textName.text = name;
    }

    private void GenerateIniciative()
    {
        var rand = new System.Random();
        iniciative = rand.Next(1, 10) + bonusAgility;
    }

    private void UpdateText()
    {
        textHealth.text = health.ToString();
    }
}
