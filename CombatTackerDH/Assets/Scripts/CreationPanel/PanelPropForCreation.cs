using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PanelPropForCreation : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TMP_InputField inputLvl;
    PropertyCharacter propertyCharacter;
    public delegate void ReturnProperty(PropertyCharacter propertyCharacter);
    ReturnProperty returnProperty;
    int lvl;
    public string Name { get => propertyCharacter.Name; }

    public void SetParams(PropertyCharacter propertyCharacter, ReturnProperty returnProperty)
    {
        gameObject.SetActive(true);
        textName.text = propertyCharacter.Name;
        this.propertyCharacter = propertyCharacter;
        this.returnProperty = returnProperty;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(inputLvl.text.Length > 0)
        {
            int.TryParse(inputLvl.text, out lvl);
            propertyCharacter.Lvl = lvl;
        }
        returnProperty?.Invoke(propertyCharacter);
    }
}
