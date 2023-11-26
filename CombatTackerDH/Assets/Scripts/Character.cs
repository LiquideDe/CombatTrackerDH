using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public delegate void DeleteThis(Character character);
    DeleteThis deleteThis;
    [SerializeField] TextMeshProUGUI textName, textHealth, textClip;
    [SerializeField] TMP_InputField iniciativeInput;
    [SerializeField] PanelDamage panelDamage;
    int head, leftHand, rightHand, body, leftLeg, rightLeg, health, bonusAgility, bonusFatigue, iniciative, clip, maxClip, ammo;
    string nameCharacter;
    [SerializeField] Image image;

    public int Init { get => iniciative; }
    public void SetParams(int head, int leftHand, int rightHand, int body, int leftLeg, int rightLeg, int health, int bonusAgility, int bonusFatigue, string name, int clip, DeleteThis deleteThis)
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
        this.clip = clip;
        maxClip = clip;
        ammo = clip * 2;
        UpdateText();
        this.deleteThis = deleteThis;
    }

    private void Start()
    {
        Game.BattleStarted += StartBattle;
        Game.BattleFinished += FinishBattle;

    }

    private void GenerateIniciative()
    {
        if(iniciativeInput.text == "")
        {
            var rand = new System.Random();
            iniciative = rand.Next(1, 10) + bonusAgility;
        }
        else
        {
            iniciative = int.Parse(iniciativeInput.text);
        }
        
    }

    private void UpdateText()
    {
        textHealth.text = health.ToString();
        textClip.text = clip.ToString();
    }

    public void GetDamage()
    {
        panelDamage.RegDelegateFromNPC(GetDamageWithParams);
    }

    private void GetDamageWithParams(int place, int damage, int prob)
    {
        if(place < 10)
        {
            Damage(head - prob, damage);
        }
        else if(place > 10 && place < 21)
        {
            Damage(rightHand - prob, damage);
        }
        else if (place > 20 && place < 31)
        {
            Damage(leftHand - prob, damage);
        }
        else if (place > 30 && place < 71)
        {
            Damage(body - prob, damage);
        }
        else if (place > 70 && place < 86)
        {
            Damage(rightLeg - prob, damage);
        }
        else if (place > 85 && place < 101)
        {
            Damage(leftLeg - prob, damage);
        }
    }

    private void Damage(int armor, int damage)
    {
        if (armor < 0)
            armor = 0;
        damage -= (armor + bonusFatigue);
        if (damage < 0)
        {
            damage = 0;
        }
        health -= damage;
        UpdateText(); 
    }

    public void Shoot()
    {
        if(clip > 0)
            clip--;
        UpdateText();
    }

    public void Reload()
    {
        if(ammo >= maxClip)
        {
            clip = maxClip;
            ammo -= maxClip;
        }
        else if(ammo > 0)
        {
            clip = ammo;
            ammo = 0;
        }
        UpdateText();
    }

    private void StartBattle()
    {
        GenerateIniciative();
    }

    private void FinishBattle()
    {
        image.color = Color.white;
    }

    public void DeleteThisCharacter()
    {
        deleteThis?.Invoke(this);
        Game.BattleStarted -= StartBattle;
        Game.BattleFinished -= FinishBattle;
        Destroy(gameObject);
    }

    public void CharacterTurn()
    {
        image.color = Color.green;
    }

    public void CharacterEndTurn()
    {
        image.color = Color.white;
    }
}
