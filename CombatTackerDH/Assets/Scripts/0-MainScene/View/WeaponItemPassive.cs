using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class WeaponItemPassive : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameWeaponText, _classWeaponText, _rangeWeaponText, _damageText, _penetrationText, _reloadText, _weightText, _rarityText, _clipText;

    [SerializeField] private ItemInList _propertyPrefab;
    [SerializeField] private Transform _contentProperties;

    public event Action<string> ShowThisProperty;

    public virtual void Initialize(Weapon weapon)
    {
        gameObject.SetActive(true);
        _nameWeaponText.text = weapon.NameWithAmount;
        _classWeaponText.text = $"Класс. {weapon.ClassWeapon}";
        _damageText.text = $"Урон {weapon.Damage}";
        _penetrationText.text = $"Проб. {weapon.Penetration}";
        _weightText.text = $"Вес {weapon.Weight}";
        _rarityText.text = $"Дост. {weapon.Rarity}";
        if (weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            _rangeWeaponText.text = $"Дальн. {weapon.Range}";
            _reloadText.text = $"Перез. {weapon.Reload}";
            _clipText.text = $"Маг. {weapon.Clip}/{weapon.MaxClip}";
        }
        else
        {
            _rangeWeaponText.text = $"Дальн. -";
            _clipText.text = $"Маг.  -";
            _reloadText.text = $"-";
        }

        List<string> propertiesString = weapon.Properties.Split(new char[] { ',' }).ToList();
        foreach (string propertyName in propertiesString)
        {
            ItemInList item = Instantiate(_propertyPrefab, _contentProperties);
            item.ChooseThis += ShowThisPropertyPressed;
            item.Initialize(RemoveAllBrackets(propertyName), propertyName);
        }
    }

    public void UpdateClip(int amount, int clipMax)
    {
        _clipText.text = $"Маг. {amount}/{clipMax}";
    }

    private void ShowThisPropertyPressed(string name) => ShowThisProperty?.Invoke(name);

    private string RemoveAllBrackets(string property)
    {
        int lb = property.IndexOf('(');
        if (lb < 0) return property;

        string text = property.Substring(0, lb);
        Debug.Log($"Левая скобка {lb},{text}, original {property}");
        return text;
    }

}
