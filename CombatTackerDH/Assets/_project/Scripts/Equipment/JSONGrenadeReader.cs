using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombarTracker
{
    [Serializable]
    public class JSONGrenadeReader
    {
        public string name, description, damage, rarity, weaponClass, properties, typeEquipment;
        public int penetration, amount;
        public float weight;
    }
}


