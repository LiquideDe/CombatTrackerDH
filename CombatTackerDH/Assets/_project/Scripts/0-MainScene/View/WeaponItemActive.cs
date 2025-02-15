using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

namespace CombarTracker
{
    public class WeaponItemActive : WeaponItemPassive
    {
        [SerializeField] private TextMeshProUGUI _singleFireText, _semiAutoFireText, _autoFireText;
        [SerializeField] private Button _buttonSingle, _buttonSemiAuto, _buttonAuto, _buttonReload;
        public event Action<string> SingleFire, SemiAutoFire, AutoFire, Reload;
        private string _name;

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            _name = weapon.Name;
            if (weapon.TypeEq == Equipment.TypeEquipment.Range)
            {
                List<string> rofs = weapon.Rof.Split(new char[] { '/' }).ToList();
                if (rofs[0] != "-")
                {
                    _singleFireText.text = $"1";
                    _buttonSingle.onClick.AddListener(SingleFirePressed);
                }
                else
                {
                    _singleFireText.text = "-";
                    _buttonSingle.gameObject.SetActive(false);
                }

                if (rofs[1] != "-")
                {
                    _semiAutoFireText.text = rofs[1];
                    _buttonSemiAuto.onClick.AddListener(SemiAutoPressed);
                }
                else
                {
                    _semiAutoFireText.text = "-";
                    _buttonSemiAuto.gameObject.SetActive(false);
                }

                if (rofs[2] != "-")
                {
                    _autoFireText.text = rofs[2];
                    _buttonAuto.onClick.AddListener(AutoFirePressed);
                }
                else
                {
                    _buttonAuto.gameObject.SetActive(false);
                }

                _buttonReload.onClick.AddListener(ReloadPressed);
            }
            else
            {
                _singleFireText.text = "-";
                _buttonSingle.gameObject.SetActive(false);
                _semiAutoFireText.text = "-";
                _buttonSemiAuto.gameObject.SetActive(false);
                _autoFireText.text = "-";
                _buttonAuto.gameObject.SetActive(false);
                _buttonReload.gameObject.SetActive(false);
            }
        }



        private void SemiAutoPressed() => SemiAutoFire.Invoke(_name);

        private void SingleFirePressed() => SingleFire.Invoke(_name);

        private void AutoFirePressed() => AutoFire.Invoke(_name);

        private void ReloadPressed() => Reload.Invoke(_name);

    }
}

