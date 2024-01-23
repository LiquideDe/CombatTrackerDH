using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyArmor : PropertyEquipment
{
    private int head, hands, body, legs;

    public PropertyArmor(string name, string description, float weight, string rarity, int head, int hands, int body, int legs) : base (name, description, weight, rarity)
    {
        this.head = head;
        this.hands = hands;
        this.body = body;
        this.legs = legs;
    }

    public int Head { get => head; }
    public int Hands { get => hands; }
    public int Body { get => body; }
    public int Legs { get => legs; }
}
