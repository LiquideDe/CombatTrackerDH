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
        _classWeaponText.text = $"�����. {weapon.ClassWeapon}";
        _damageText.text = $"���� {weapon.Damage}";
        _penetrationText.text = $"����. {weapon.Penetration}";
        _weightText.text = $"��� {weapon.Weight}";
        _rarityText.text = $"����. {weapon.Rarity}";
        if (weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            _rangeWeaponText.text = $"�����. {weapon.Range}";
            _reloadText.text = $"�����. {weapon.Reload}";
            _clipText.text = $"���. {weapon.Clip}/{weapon.MaxClip}";
        }
        else
        {
            _rangeWeaponText.text = $"�����. -";
            _clipText.text = $"���.  -";
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
        _clipText.text = $"���. {amount}/{clipMax}";
    }

    private void ShowThisPropertyPressed(string name) => ShowThisProperty?.Invoke(name);

    private string RemoveAllBrackets(string property)
    {
        int lb = property.IndexOf('(');
        if (lb < 0) return property;

        string text = property.Substring(0, lb);
        Debug.Log($"����� ������ {lb},{text}, original {property}");
        return text;
    }

}
