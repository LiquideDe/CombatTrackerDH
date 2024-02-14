using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaponVisual : WeaponSimple
{    
    [SerializeField] Button buttonSingle, buttonSemiAuto, buttonAuto, buttonReload;
    public delegate void ShowThisProp(string name);
    ShowThisProp showThisProp;
    int clip=0, maxClip, totalAmmo, single=0, semiAuto=0, auto=0;

    public void Init(ShowThisProp showThisProp)
    {
        this.showThisProp = showThisProp;
        if (weapon.TypeEq == Equipment.TypeEquipment.Range)
        {
            clip = weapon.Clip;
            maxClip = clip;
            totalAmmo = clip * 2;
            List<string> rof = weapon.Rof.Split(new char[] { '/' }).ToList();

            textSingle.text = rof[0];
            textSemiAuto.text = rof[1];
            textAuto.text = rof[2];
            if (buttonSingle != null)
            {
                if (rof[0] != "-")
                {
                    int.TryParse(rof[0], out single);
                }
                else
                {
                    buttonSingle.interactable = false;
                }

                if (rof[1] != "-")
                {
                    int.TryParse(rof[1], out semiAuto);
                }
                else
                {
                    buttonSemiAuto.interactable = false;
                }

                if (rof[2] != "-")
                {
                    int.TryParse(rof[2], out auto);
                }
                else
                {
                    buttonAuto.interactable = false;
                }
            }

        }
        else
        {
            if (buttonSingle != null)
            {
                buttonSingle.interactable = false;
                buttonSemiAuto.interactable = false;
                buttonAuto.interactable = false;
                buttonReload.interactable = false;
            }
        }

        if (weapon.Properties.Length > 1)
        {
            foreach (PanelWithInfo prop in properties)
            {
                prop.RegDelegate(ShowProperty);
            }
        }
    }

    private void ShowProperty(string name)
    {
        showThisProp?.Invoke(name);
    }

    public void ShootSingle()
    {
        Shoot(1);
    }

    public void ShootSemiAuto()
    {
        Shoot(semiAuto);
    }

    public void ShootAuto()
    {
        Shoot(auto);
    }

    private void Shoot(int amount)
    {
        if(clip > 0)
        {
            if(clip >= amount)
            {
                clip -= amount;
            }
            else
            {
                clip = 0;
            }
        }
        textClip.text = clip.ToString();
    }

    public void Reload()
    {
        if(totalAmmo > 0)
        {
            if (totalAmmo >= maxClip)
            {
                int raz = maxClip - clip;
                clip += raz;
                totalAmmo -= raz;
            }
            else
            {
                int raz = totalAmmo - clip;
                clip = raz;
                totalAmmo -= raz;
            }
        }
        textClip.text = clip.ToString();
    }
}
