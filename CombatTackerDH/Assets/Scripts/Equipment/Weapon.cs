using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : Equipment
{
    private string classWeapon, rof, damage, reload, properties;
    private int range, penetration, clip, maxClip, totalAmmo, semiAuto, auto;    

    public Weapon(JSONRangeReader rangeReader) :base(rangeReader.name, rangeReader.description, rangeReader.rarity, rangeReader.amount, rangeReader.weight)
    {
        typeEquipment = TypeEquipment.Range;
        classWeapon = rangeReader.weaponClass;
        range = rangeReader.range;
        rof = rangeReader.rof;
        damage = rangeReader.damage;
        penetration = rangeReader.penetration;
        clip = rangeReader.clip;
        totalAmmo = clip * 2;
        maxClip = clip;
        reload = rangeReader.reload;
        properties = rangeReader.properties;
        List<string> list = new List<string>();
        list = rof.Split(new char[] { '/' }).ToList();
        int.TryParse(list[1], out semiAuto);
        int.TryParse(list[2], out auto);
    }

    public Weapon(JSONMeleeReader meleeReader) : base(meleeReader.name, meleeReader.description, meleeReader.rarity, meleeReader.amount, meleeReader.weight)
    {
        typeEquipment = TypeEquipment.Melee;
        classWeapon = meleeReader.weaponClass;
        damage = meleeReader.damage;
        penetration = meleeReader.penetration;
        properties = meleeReader.properties;
        rof = "";
    }

    public Weapon(JSONGrenadeReader grenadeReader) : base (grenadeReader.name, grenadeReader.description, grenadeReader.rarity, grenadeReader.amount, grenadeReader.weight)
    {
        typeEquipment = TypeEquipment.Grenade;
        classWeapon = grenadeReader.weaponClass;
        damage = grenadeReader.damage;
        penetration = grenadeReader.penetration;
        properties = grenadeReader.properties;
        rof = "";
    }

    public Weapon(Weapon weapon) : base(weapon.Name, weapon.Description, weapon.Rarity, weapon.Amount, weapon.Weight)
    {
        typeEquipment = weapon.TypeEq;
        classWeapon = weapon.ClassWeapon;
        range = weapon.Range;
        rof = weapon.Rof;
        damage = weapon.Damage;
        penetration = weapon.Penetration;
        clip = weapon.Clip;        
        reload = weapon.Reload;
        properties = weapon.Properties;
        if(typeEquipment == TypeEquipment.Range)
        {
            totalAmmo = clip * 2;
            maxClip = clip;
            List<string> list = new List<string>();
            list = rof.Split(new char[] { '/' }).ToList();
            int.TryParse(list[1], out semiAuto);
            int.TryParse(list[2], out auto);
        }
    }

    public string ClassWeapon { get => classWeapon; }
    public int Range { get => range; }
    public string Rof { get => rof; }
    public string Damage { get => damage; }
    public int Penetration { get => penetration; }
    public int Clip { get => clip; set => clip = value; }
    public string Reload { get => reload; }
    public string Properties { get => properties; }
    public int TotalAmmo { get => totalAmmo; set => totalAmmo = value; }
    public int MaxClip { get => maxClip;}
    public int Auto => auto; 
    public int SemiAuto  => semiAuto; 
}
