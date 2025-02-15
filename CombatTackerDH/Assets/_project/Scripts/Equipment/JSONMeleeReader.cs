using System.Collections;
using System.Collections.Generic;
using System;

namespace CombarTracker
{
    [Serializable]
    public class JSONMeleeReader
    {
        public string name, description, properties, type, weaponClass, damage, rarity, typeEquipment;
        public int penetration, amount;
        public float weight;
    }
}


