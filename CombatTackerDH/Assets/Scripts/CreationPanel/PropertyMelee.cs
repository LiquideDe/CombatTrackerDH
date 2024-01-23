using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMelee : PropertyEquipment
{
    private string classWeapon, damage, propetries;
    private int penetration;
    public PropertyMelee(string name, string description, float weight, string rarity, string classWeapon, string damage, int penetration, string propetries) : base(name, description, weight, rarity)
    {
        this.classWeapon = classWeapon;
        this.damage = damage;
        this.penetration = penetration;
        this.propetries = propetries;
    }

    public string ClassWeapon { get => classWeapon; }
    public string Damage { get => damage; }
    public int Penetration { get => penetration; }
    public string Propetries { get => propetries; }
}
