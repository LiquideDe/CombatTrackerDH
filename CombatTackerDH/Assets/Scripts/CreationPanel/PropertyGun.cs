using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyGun : PropertyMelee
{
    private string rof, reload;
    private int range, clip;
    public PropertyGun(string name, string description, float weight, string rarity, string classWeapon, string damage, int penetration, string properties, int range, string rof, int clip, string reload) 
        : base(name, description, weight, rarity, classWeapon, damage, penetration, properties)
    {
        this.rof = rof;
        this.reload = reload;
        this.range = range;
        this.clip = clip;
    }

    public string Rof { get => rof; }
    public string Reload { get => reload; }
    public int Range { get => range; }
    public int Clip { get => clip; }
}
