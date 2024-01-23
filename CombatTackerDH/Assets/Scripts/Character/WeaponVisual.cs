using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WeaponVisual : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textName, textClass, textRange, textRof, textDamage, textPen, textClip, textReload, textWeight, textTrade;
    [SerializeField] PanelWithInfo panelWithInfoExample;
    [SerializeField] Transform content;
    private List<PanelWithInfo> properties = new List<PanelWithInfo>();
    public delegate void ShowThisProp(string name);
    ShowThisProp showThisProp;
    public void SetParams(Weapon weapon, ShowThisProp showThisProp)
    {
        gameObject.SetActive(true);
        textName.text = weapon.Name;
        textClass.text = weapon.ClassWeapon;
        if(weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            textRange.text = weapon.Range.ToString();
            textClip.text = weapon.Clip.ToString();
            textReload.text = weapon.Reload;
            textRof.text = weapon.Rof;
        }
        else
        {
            textRange.text = "";
            textClip.text = "";
            textReload.text = "";
        }

        textDamage.text = weapon.Damage;
        textPen.text = weapon.Penetration.ToString();
        textWeight.text = weapon.Weight.ToString();
        textTrade.text = weapon.Rarity;
        if(weapon.Properties.Length > 1)
        {
            List<string> props = new List<string>();
            props.AddRange(weapon.Properties.Split(new char[] { ',' }).ToList());
            foreach(string prop in props)
            {
                properties.Add(Instantiate(panelWithInfoExample, content));
                properties[^1].SetParams(new PropertyCharacter(prop,""), ShowProperty);
            }
        }
    }

    private void ShowProperty(string name)
    {
        showThisProp?.Invoke(name);
    }
}
