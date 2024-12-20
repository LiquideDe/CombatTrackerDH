using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class DamagePanelPresenter : IPresenter
{
    public event Action<string> ReturnTextToArmor;
    public event Action DamageToShelter;

    private AudioManager _audioManager;
    private DamagePanelView _view;
    private Character _character;
    private int[] _placesWithDamage = new int[6];

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;


    public void Initialize(DamagePanelView view, Character character)
    {
        _view = view;
        _character = character;
        Subscribe();
    }

    private void Subscribe()
    {
        _view.CalculateDamage += CalculateDamageDown;
        _view.Cancel += CancelDown;
        _view.PlayClick += _audioManager.PlayClick;
        _view.PlayWarning += _audioManager.PlayWarning;
    }

    private void Unscribe()
    {
        _view.CalculateDamage -= CalculateDamageDown;
        _view.Cancel -= CancelDown;
        _view.PlayClick -= _audioManager.PlayClick;
        _view.PlayWarning -= _audioManager.PlayWarning;
    }

    private void CalculateDamageDown(List<DamageItem> damageItems)
    {
        _audioManager.PlayDone();
        foreach (DamageItem item in damageItems)
        {
            if (item.Place <= 10)
            {
                Damage(_character.ArmorTotalHead, _character.ArmorHead + _character.ArmorAblHead, _character.ShelterHead, item, 0); //0 - ������                
            }
            else if (item.Place > 10 && item.Place < 21)
            {
                Damage(_character.ArmorTotalRightHand, _character.ArmorRightHand + _character.ArmorAblRightHand, _character.ShelterRightHand, item, 1); //1 - ������ ����
            }
            else if (item.Place > 20 && item.Place < 31)
            {
                Damage(_character.ArmorTotalLeftHand, _character.ArmorLeftHand + _character.ArmorAblLeftHand, _character.ShelterLeftHand, item, 2); //2 - ����� ����
            }
            else if (item.Place > 30 && item.Place < 71)
            {
                Damage(_character.ArmorTotalBody, _character.ArmorBody + _character.ArmorAblBody, _character.ShelterBody, item, 3);//3 - ����
            }
            else if (item.Place > 70 && item.Place < 86)
            {
                Damage(_character.ArmorTotalRightLeg, _character.ArmorRightLeg + _character.ArmorAblRightLeg, _character.ShelterRightLeg, item, 4);//4 - ������ ����
            }
            else if (item.Place > 85 && item.Place < 101)
            {
                Damage(_character.ArmorTotalLeftLeg, _character.ArmorLeftLeg + _character.ArmorAblLeftLeg, _character.ShelterLeftLeg, item, 5);//5 - ����� ����
            }
        }
        SetFinalText();
    }

    private void Damage(int totalDef, int armor, int shelter ,DamageItem item, int idPlace)
    {
        int damage = 0;
        int bToughness = totalDef - armor;
        armor += shelter;
        if (!item.IsIgnoreArmor && !item.IsIgnoreToughness && !item.IsWarp)
        {

            if (armor < item.Penetration)
            {
                damage = item.Damage - bToughness;
            }
            else
            {
                damage = item.Damage - (armor - item.Penetration + bToughness);
            }
        }
        else if (item.IsIgnoreArmor && !item.IsIgnoreToughness && !item.IsWarp)
        {
            damage = item.Damage - bToughness;
        }
        else if (!item.IsIgnoreArmor && item.IsIgnoreToughness && !item.IsWarp)
        {
            if (armor >= item.Penetration)
                damage = item.Damage - (armor - item.Penetration);
            else
                damage = item.Damage;
        }
        else if (item.IsWarp)
        {
            damage = item.Damage - (_character.Willpower/10);
        }
        else if (item.IsIgnoreArmor && item.IsIgnoreToughness && !item.IsWarp)
        {
            damage = item.Damage;
        }

        if (damage > 0 && _character.IsHorde)        
            _placesWithDamage[idPlace] += 1;            
        
        else
            _placesWithDamage[idPlace] += damage;



        if (shelter > 0 && (item.Penetration > shelter || item.Penetration+item.Damage > shelter))
            DamageToShelter?.Invoke();


    }

    private void SetFinalText()
    {
        int totalDamage = 0;
        string textDamage = "";
        if (_placesWithDamage[0] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[0]} ����� � ������. \n";
            totalDamage += _placesWithDamage[0];
        }
        if (_placesWithDamage[1] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[1]} ����� � ������ ����. \n";
            totalDamage += _placesWithDamage[1];
        }
        if (_placesWithDamage[2] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[2]} ����� � ����� ����. \n";
            totalDamage += _placesWithDamage[2];
        }
        if (_placesWithDamage[3] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[3]} ����� � ����. \n";
            totalDamage += _placesWithDamage[3];
        }
        if (_placesWithDamage[4] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[4]} ����� � ������ ����. \n";
            totalDamage += _placesWithDamage[4];
        }
        if (_placesWithDamage[5] > 0)
        {
            textDamage += $"�������� {_placesWithDamage[5]} ����� � ����� ����. \n";
            totalDamage += _placesWithDamage[5];
        }
        textDamage += $"����� �������� {totalDamage} �����";    
        _character.Wounds -= totalDamage;            
        Unscribe();
        _view.DestroyView();
        ReturnTextToArmor?.Invoke(textDamage);
    }

    private void CancelDown()
    {
        _audioManager.PlayClick();
        Unscribe();
        _view.DestroyView();
    }
}
