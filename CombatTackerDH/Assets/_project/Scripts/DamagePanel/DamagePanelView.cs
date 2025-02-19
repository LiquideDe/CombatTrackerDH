using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace CombarTracker
{
    public class DamagePanelView : MonoBehaviour
    {
        [SerializeField] TMP_InputField _inputPenetration, _inputPlace, _inputDamage, _inputDamageHorde;
        [SerializeField] Toggle _toggleIsWarp, _toggleIsIgnoreArmor, _toggleIsIgnoreToughness;
        [SerializeField] DamageItem _damageItemPrefab;
        [SerializeField] Transform _contentForDamageItems;
        [SerializeField] Button _buttonAddDamage, _buttonCancel, _buttonDone;
        [SerializeField] GameObject _placeDamageObject, _hordeDamageObject;

        public event Action<List<DamageItem>> CalculateDamage;
        public event Action Cancel, PlayClick, PlayWarning;

        private List<DamageItem> _damageItems = new List<DamageItem>();
        private int[] placesTakeDamage = new int[6];
        private bool _isHorde;

        private void OnEnable()
        {
            _buttonAddDamage.onClick.AddListener(AddDamagePressed);
            _buttonDone.onClick.AddListener(CalculateDamagePressed);
            _buttonCancel.onClick.AddListener(CancelPressed);
            _toggleIsIgnoreArmor.onValueChanged.AddListener(PlayClickPressed);
            _toggleIsWarp.onValueChanged.AddListener(PlayClickPressed);
            _toggleIsIgnoreToughness.onValueChanged.AddListener(PlayClickPressed);
        }

        private void OnDisable()
        {
            _buttonAddDamage.onClick.RemoveAllListeners();
            _buttonDone.onClick.RemoveAllListeners();
            _buttonCancel.onClick.RemoveAllListeners();
            _toggleIsIgnoreArmor.onValueChanged.RemoveAllListeners();
            _toggleIsWarp.onValueChanged.RemoveAllListeners();
            _toggleIsIgnoreToughness.onValueChanged.RemoveAllListeners();
        }

        public void DestroyView() => Destroy(gameObject);

        public void SetHordeOrNot(bool isHorde)
        {
            _isHorde = isHorde;
            if (isHorde)
            {
                _placeDamageObject.SetActive(false);
                _hordeDamageObject.SetActive(true);
                _inputDamageHorde.text = "1";
            }
            else
            {
                _placeDamageObject.SetActive(true);
                _hordeDamageObject.SetActive(false);
            }
        }

        private void CancelPressed() => Cancel?.Invoke();

        private void CalculateDamagePressed() => CalculateDamage?.Invoke(_damageItems);

        private void PlayClickPressed(bool arg) => PlayClick?.Invoke();

        private void PlayWarningPressed() => PlayWarning?.Invoke();

        private void AddDamagePressed()
        {
            if (_inputDamage.text.Length > 0)
            {
                PlayClickPressed(true);
                SetNewDamage(_inputPlace.text, _inputDamage.text, _inputPenetration.text);
                _inputDamage.text = "";
                _inputDamage.Select();
            }
            else
                PlayWarningPressed();
        }

        private void SetNewDamage(string placeText, string damageText, string penetrationText)
        {
            _damageItems.Add(Instantiate(_damageItemPrefab, _contentForDamageItems));
            if (_isHorde)
            {
                _damageItems[^1].Initialize(damageText, penetrationText, _toggleIsWarp.isOn, _toggleIsIgnoreArmor.isOn, _toggleIsIgnoreToughness.isOn, _inputDamageHorde.text);
            }
            else
            {
                _damageItems[^1].Initialize(placeText, damageText, penetrationText, _toggleIsWarp.isOn, _toggleIsIgnoreArmor.isOn, _toggleIsIgnoreToughness.isOn);
            }            
            
            _damageItems[^1].ChooseThis += RemoveItem;
            //LayoutRebuilder.ForceRebuildLayoutImmediate(_contentForDamageItems as RectTransform);
        }

        private void RemoveItem(string name)
        {
            PlayClickPressed(true);
            foreach (DamageItem damageItem in _damageItems)
                if (string.Compare(name, damageItem.Name, true) == 0)
                {
                    Destroy(damageItem.gameObject);
                    _damageItems.Remove(damageItem);
                    break;
                }
        }


    }
}

