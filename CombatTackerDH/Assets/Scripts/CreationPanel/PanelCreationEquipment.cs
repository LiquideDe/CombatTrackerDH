using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelCreationEquipment : PanelCreation
{
    [SerializeField] protected TMP_InputField inputWeight, inputRarity;
    public override void Done()
    {
        float weight;
        float.TryParse(inputWeight.text, out weight);
        PropertyEquipment equipment = new PropertyEquipment(inputName.text, inputDescription.text, weight, inputRarity.text);
        returnProp?.Invoke(equipment);
        Destroy(gameObject);
    }
}
