using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private WeaponItemActive _weaponPrefab;
    [SerializeField] private Transform _contentWeapons;

    public event Action<string> ShowThisProperty, SingleFire, SemiAutoFire, AutoFire, Reload;

    private List<WeaponItemActive> _weaponItems = new List<WeaponItemActive>();

    public void Initialize(List<Weapon> weapons)
    {
        if(_weaponItems.Count > 0)
        {
            foreach (WeaponItemActive weaponItem in _weaponItems)
                Destroy(weaponItem.gameObject);

            _weaponItems.Clear();
        }

        foreach(Weapon weapon in weapons)
        {
            WeaponItemActive item = Instantiate(_weaponPrefab, _contentWeapons);
            item.AutoFire += AutoFirePressed;
            item.Reload += ReloadPressed;
            item.SemiAutoFire += SemiAutoFirePressed;
            item.ShowThisProperty += ShowThisPropertyPressed;
            item.SingleFire += SingleFirePressed;
            item.Initialize(weapon);
            _weaponItems.Add(item);
        }
    }

    public void ClearWeapons()
    {
        foreach(WeaponItemActive weapon in _weaponItems)
            Destroy(weapon.gameObject);

        _weaponItems.Clear();
        
    }

    private void SingleFirePressed(string name) => SingleFire?.Invoke(name);
    private void SemiAutoFirePressed(string name) => SemiAutoFire?.Invoke(name);
    private void AutoFirePressed(string name) => AutoFire?.Invoke(name);
    private void ReloadPressed(string name) => Reload?.Invoke(name);
    private void ShowThisPropertyPressed(string name) => ShowThisProperty?.Invoke(name);
}
