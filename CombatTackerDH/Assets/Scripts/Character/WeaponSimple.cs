using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WeaponSimple : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textName, textClass, textRange, textDamage, textPen, textClip, textReload, textWeight, textTrade, textSingle, textSemiAuto, textAuto;
    [SerializeField] protected PanelWithInfo panelWithInfoExample;
    [SerializeField] protected Transform content;

    protected List<PanelWithInfo> properties = new List<PanelWithInfo>();
    protected Weapon weapon;

    public void SetParams(Weapon weapon)
    {
        gameObject.SetActive(true);
        this.weapon = weapon;
        textName.text = weapon.Name;
        textClass.text = weapon.ClassWeapon;
        if (weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            textRange.text = weapon.Range.ToString();
            textClip.text = weapon.Clip.ToString();
            textReload.text = weapon.Reload;
            List<string> rof = weapon.Rof.Split(new char[] { '/' }).ToList();
            textSingle.text = rof[0];
            textSemiAuto.text = rof[1];
            textAuto.text = rof[2];

        }
        else
        {
            textRange.text = "";
            textClip.text = "";
            textReload.text = "";
            textSingle.text = "";
            textSemiAuto.text = "";
            textAuto.text = "";
        }

        textDamage.text = weapon.Damage;
        textPen.text = weapon.Penetration.ToString();
        textWeight.text = weapon.Weight.ToString();
        textTrade.text = weapon.Rarity;
        if (weapon.Properties.Length > 1)
        {
            List<string> props = new List<string>();
            props.AddRange(weapon.Properties.Split(new char[] { ',' }).ToList());
            foreach (string prop in props)
            {
                properties.Add(Instantiate(panelWithInfoExample, content));
                properties[^1].SetParams(new PropertyCharacter(prop, ""), null);
            }
        }
    }
}
