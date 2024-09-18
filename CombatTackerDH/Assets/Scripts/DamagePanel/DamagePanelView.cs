using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DamagePanelView : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputPenetration, _inputPlace, _inputDamage;
    [SerializeField] Toggle _toggleIsWarp, _toggleIsIgnoreArmor, _toggleIsIgnoreToughness;
    [SerializeField] DamageItem _damageItemPrefab;
    [SerializeField] Transform _contentForDamageItems;
    [SerializeField] Button _buttonAddDamage, _buttonCancel, _buttonDone;

    public event Action<List<DamageItem>> CalculateDamage;
    public event Action Cancel, PlayClick, PlayWarning;

    private List<DamageItem> _damageItems = new List<DamageItem>();
    private int[] placesTakeDamage = new int[6];

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

    private void CancelPressed() => Cancel?.Invoke();

    private void CalculateDamagePressed() => CalculateDamage?.Invoke(_damageItems);    

    private void PlayClickPressed(bool arg) => PlayClick?.Invoke();

    private void PlayWarningPressed() => PlayWarning?.Invoke();

    private void AddDamagePressed()
    {
        if (_inputPlace.text.Length > 0 && _inputDamage.text.Length > 0)
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
        _damageItems[^1].Initialize(placeText, damageText, penetrationText, _toggleIsWarp.isOn, _toggleIsIgnoreArmor.isOn, _toggleIsIgnoreToughness.isOn);
        _damageItems[^1].ChooseThis += RemoveItem;
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_contentForDamageItems as RectTransform);
    }

    private void RemoveItem(string name)
    {
        PlayClickPressed(true);
        foreach(DamageItem damageItem in _damageItems)
            if(string.Compare(name, damageItem.Name, true) == 0)
            {
                Destroy(damageItem.gameObject);
                _damageItems.Remove(damageItem);
                break;
            }
    }


}
