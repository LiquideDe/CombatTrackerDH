using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCreationGrenade : PanelCreationMelee
{
    public override void Done()
    {
        float.TryParse(inputWeight.text, out float weight);
        int.TryParse(inputPenetration.text, out int penetration);
        string properties = "";
        foreach (PanelWithInfo panel in propertiesInWeapon)
        {
            properties += $"{panel.Name},";
        }
        properties = DeleteLastChar(properties);

        PropertyGrenade grenade = new PropertyGrenade(inputName.text, "", weight, inputRarity.text, inputClass.text, inputDamage.text, penetration, properties);
        returnProp?.Invoke(grenade);
        Destroy(gameObject);
    }
}
