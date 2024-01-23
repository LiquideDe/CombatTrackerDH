using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PanelWithInfo : MonoBehaviour, IPointerDownHandler, IPanel
{
    public delegate void MouseDown(string text);
    MouseDown mouseDown;
    PropertyCharacter propertyCharacter;
    [SerializeField] TextMeshProUGUI textName;
    public string Name { get => propertyCharacter.Name; }
    public string Description { get => propertyCharacter.Description; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(propertyCharacter.Description.Length > 0)
        {
            mouseDown?.Invoke(propertyCharacter.Description);
        }
        else
        {
            mouseDown?.Invoke(propertyCharacter.Name);
        }
        
    }

    
    public void SetParams(PropertyCharacter param, MouseDown deleg)
    {
        gameObject.SetActive(true);
        this.propertyCharacter = param;
        this.mouseDown = deleg;
        textName.text = propertyCharacter.Name;
        if (propertyCharacter.Lvl > 0)
        {
            textName.text += $" ({propertyCharacter.Lvl})";
        }
    }

}
