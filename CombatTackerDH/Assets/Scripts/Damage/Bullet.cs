using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    private int place, damage, penetration;
    private bool isIgnoreArmor, isIgnoreToughness, isWarpWeapon;
    public int Place { get => place; }
    public int Damage { get => damage; }
    public int Penetration { get => penetration; }
    public bool IsIgnoreArmor { get => isIgnoreArmor; }
    public bool IsIgnoreToughness { get => isIgnoreToughness; }
    public bool IsWarpWeapon { get => isWarpWeapon; }

    public Bullet(string place, string penetration, string damage, bool isIgnoreArmor, bool isIgnoreToghness, bool isWarpWeapon)
    {
        int.TryParse(place, out this.place);
        int.TryParse(penetration, out this.penetration);
        int.TryParse(damage, out this.damage);
        this.isIgnoreArmor = isIgnoreArmor;
        this.isIgnoreToughness = isIgnoreToghness;
        this.isWarpWeapon = isWarpWeapon;
    }
}
