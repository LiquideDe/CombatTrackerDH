using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelCreationGun : PanelCreationMelee
{
    [SerializeField] TMP_InputField inputRange, inputRof, inputClip, inputReload;

    public override void Done()
    {
        float.TryParse(inputWeight.text, out float weight);
        int.TryParse(inputPenetration.text, out int penetration);
        int.TryParse(inputRange.text, out int range);
        int.TryParse(inputClip.text, out int clip);
        string properties = "";
        foreach (PanelWithInfo panel in propertiesInWeapon)
        {
            properties += $"{panel.Name},";
        }
        properties = DeleteLastChar(properties);
        PropertyGun gun = new PropertyGun(inputName.text, "", weight, inputRarity.text, inputClass.text, inputDamage.text, penetration, properties, range, inputRof.text, clip, inputReload.text);
        returnProp?.Invoke(gun);
        Destroy(gameObject);
    }
}
