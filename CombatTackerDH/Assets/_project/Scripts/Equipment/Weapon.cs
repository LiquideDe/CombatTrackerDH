using System.Collections.Generic;
using System.Linq;

namespace CombarTracker
{
    public class Weapon : Equipment
    {
        private string classWeapon, rof, damage, reload, properties;
        private int range, penetration, clip, typeSound;

        public Weapon(JSONRangeReader rangeReader) : base(rangeReader.name, rangeReader.description, rangeReader.rarity, rangeReader.amount, rangeReader.weight)
        {
            typeEquipment = TypeEquipment.Range;
            classWeapon = rangeReader.weaponClass;
            range = rangeReader.range;
            rof = rangeReader.rof;
            damage = rangeReader.damage;
            penetration = rangeReader.penetration;
            clip = rangeReader.clip;
            reload = rangeReader.reload;
            properties = rangeReader.properties;
            typeSound = rangeReader.typeSound;
            List<string> list = new List<string>();
            list = rof.Split(new char[] { '/' }).ToList();
            int.TryParse(list[1], out int semiauto);
            int.TryParse(list[2], out int auto);
            SemiAuto = semiauto;
            Auto = auto;
            MaxClip = clip;
            TotalAmmo = clip * 3;
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

        public Weapon(JSONGrenadeReader grenadeReader) : base(grenadeReader.name, grenadeReader.description, grenadeReader.rarity, grenadeReader.amount, grenadeReader.weight)
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
            typeSound = weapon.TypeSound;
            if (weapon.SemiAuto != 0)
                SemiAuto = weapon.SemiAuto;
            if (weapon.Auto != 0)
                Auto = weapon.Auto;
            MaxClip = weapon.MaxClip;
        }

        public string ClassWeapon { get => classWeapon; }
        public int Range { get => range; }
        public string Rof { get => rof; }
        public string Damage { get => damage; }
        public int Penetration { get => penetration; }
        public int Clip { get => clip; set => clip = value; }
        public string Reload { get => reload; }
        public string Properties { get => properties; }
        public int TypeSound => typeSound;
        public int SemiAuto { get; private set; }
        public int Auto { get; private set; }
        public int MaxClip { get; private set; }

        public int TotalAmmo { get; set; }
    }
}


