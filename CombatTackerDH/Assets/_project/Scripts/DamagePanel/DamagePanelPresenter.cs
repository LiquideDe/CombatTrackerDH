using System.Collections.Generic;
using System;
using UnityEngine;

namespace CombarTracker
{
    public class DamagePanelPresenter : IPresenter
    {
        public event Action<string> ReturnTextToArmor;
        public event Action DamageToShelter;

        private AudioManager _audioManager;
        private DamagePanelView _view;
        private Character _character;
        private int[] _placesWithDamage = new int[6];

        public DamagePanelPresenter(AudioManager audioManager, DamagePanelView view, Character character)
        {
            _audioManager = audioManager;
            _view = view;
            _character = character;
            _view.SetHordeOrNot(character.IsHorde);
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
                    Damage(_character.ArmorTotalHead, _character.ArmorHead + _character.ArmorAblHead, _character.ShelterHead, item, 0); //0 - голова                
                }
                else if (item.Place > 10 && item.Place < 21)
                {
                    Damage(_character.ArmorTotalRightHand, _character.ArmorRightHand + _character.ArmorAblRightHand, _character.ShelterRightHand, item, 1); //1 - правая рука
                }
                else if (item.Place > 20 && item.Place < 31)
                {
                    Damage(_character.ArmorTotalLeftHand, _character.ArmorLeftHand + _character.ArmorAblLeftHand, _character.ShelterLeftHand, item, 2); //2 - левая рука
                }
                else if (item.Place > 30 && item.Place < 71)
                {
                    Damage(_character.ArmorTotalBody, _character.ArmorBody + _character.ArmorAblBody, _character.ShelterBody, item, 3);//3 - тело
                }
                else if (item.Place > 70 && item.Place < 86)
                {
                    Damage(_character.ArmorTotalRightLeg, _character.ArmorRightLeg + _character.ArmorAblRightLeg, _character.ShelterRightLeg, item, 4);//4 - правая нога
                }
                else if (item.Place > 85 && item.Place < 101)
                {
                    Damage(_character.ArmorTotalLeftLeg, _character.ArmorLeftLeg + _character.ArmorAblLeftLeg, _character.ShelterLeftLeg, item, 5);//5 - левая нога
                }
            }
            SetFinalText();
        }

        private void Damage(int totalDef, int armor, int shelter, DamageItem item, int idPlace)
        {
            int damage = 0;
            int bToughness = totalDef - armor;
            int bWillpower;
            if (_character.WillpowerSuper > 0)
                bWillpower = _character.WillpowerSuper;
            else
                bWillpower = _character.Willpower / 10;

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
            else if (item.IsIgnoreArmor && item.IsWarp)
            {
                damage = item.Damage - bWillpower;
            }
            else if (!item.IsIgnoreArmor && item.IsWarp)
            {
                if (armor < item.Penetration)
                {
                    damage = item.Damage - bWillpower;
                }
                else
                {
                    damage = item.Damage - (armor - item.Penetration + bWillpower);
                }
            }
            else if (item.IsIgnoreArmor && item.IsIgnoreToughness && !item.IsWarp)
            {
                damage = item.Damage;
            }
            Debug.Log($"item.DamageToMagnitude = {item.DamageToMagnitude}");
            if (damage > 0 && _character.IsHorde)
                _placesWithDamage[idPlace] += item.DamageToMagnitude;

            else
                _placesWithDamage[idPlace] += damage;



            if (shelter > 0 && (item.Penetration > shelter || item.Penetration + item.Damage > shelter))
                DamageToShelter?.Invoke();


        }

        private void SetFinalText()
        {
            int totalDamage = 0;
            string textDamage = "";
            if (_placesWithDamage[0] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[0]} урона в голову. \n";
                totalDamage += _placesWithDamage[0];
            }
            if (_placesWithDamage[1] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[1]} урона в правую руку. \n";
                totalDamage += _placesWithDamage[1];
            }
            if (_placesWithDamage[2] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[2]} урона в левую руку. \n";
                totalDamage += _placesWithDamage[2];
            }
            if (_placesWithDamage[3] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[3]} урона в тело. \n";
                totalDamage += _placesWithDamage[3];
            }
            if (_placesWithDamage[4] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[4]} урона в правую ногу. \n";
                totalDamage += _placesWithDamage[4];
            }
            if (_placesWithDamage[5] > 0)
            {
                textDamage += $"Нанесено {_placesWithDamage[5]} урона в левую ногу. \n";
                totalDamage += _placesWithDamage[5];
            }
            textDamage += $"Всего нанесено {totalDamage} урона";
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
}

