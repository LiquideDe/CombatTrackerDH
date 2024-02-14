using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyCharacter : IName
{
    private string name, description;
    private int lvl;

    public string Name { get => name; }
    public string Description { get => description; set => description = value; }
    public int Lvl { get => lvl; set => lvl = value; }

    public PropertyCharacter(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}
