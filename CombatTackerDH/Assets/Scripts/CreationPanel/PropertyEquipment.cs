using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyEquipment : PropertyCharacter
{
    private float weight;
    private string rarity;

    public PropertyEquipment(string name, string description, float weight, string rarity) : base (name, description)
    {
        this.weight = weight;
        this.rarity = rarity;
    }

    public float Weight { get => weight; }
    public string Rarity { get => rarity; }
}
