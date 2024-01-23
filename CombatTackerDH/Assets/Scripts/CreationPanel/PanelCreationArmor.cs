using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelCreationArmor : PanelCreation
{
    [SerializeField] protected TMP_InputField inputWeight, inputRarity, inputHead, inputHands, inputBody, inputLegs;
    public override void Done()
    {
        float weight;
        float.TryParse(inputWeight.text, out weight);
        int head, hands, body, legs;
        int.TryParse(inputHead.text, out head);
        int.TryParse(inputHands.text, out hands);
        int.TryParse(inputBody.text, out body);
        int.TryParse(inputLegs.text, out legs);
        PropertyArmor armor = new PropertyArmor(inputName.text, inputDescription.text, weight, inputRarity.text, head, hands, body, legs);
        returnProp?.Invoke(armor);
        Destroy(gameObject);
    }
}
