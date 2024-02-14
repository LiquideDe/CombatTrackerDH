using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class PanelWithInfo : MonoBehaviour, IPointerDownHandler, IPanel
{
    public delegate void MouseDown(string text);
    MouseDown mouseDown;
    PropertyCharacter propertyCharacter;
    [SerializeField] TextMeshProUGUI textName;
    public string Name { get => propertyCharacter.Name; }
    public string Description { get => propertyCharacter.Description; }
    public string TextName { get => textName.text; }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*
        Debug.Log($"Отправляем инфу {propertyCharacter.Name}, {propertyCharacter.Description}");
        if(propertyCharacter.Description.Length > 0)
        {
            mouseDown?.Invoke(propertyCharacter.Description);
        }
        else
        {
            string text = propertyCharacter.Name;
            string pattern = @"\((.*)\)";
            text = Regex.Replace(text, pattern, String.Empty);

            mouseDown?.Invoke(text);
        }*/
        string text = propertyCharacter.Name;
        string pattern = @"\((.*)\)";
        text = Regex.Replace(text, pattern, String.Empty);
        text = text.Trim();
        mouseDown?.Invoke(text);
    }

    
    public void SetParams(PropertyCharacter param, MouseDown deleg)
    {
        gameObject.SetActive(true);
        this.propertyCharacter = param;
        this.mouseDown = deleg;
        textName.text = propertyCharacter.Name;
        if (propertyCharacter.Lvl > 0)
        {
            textName.text += $"({propertyCharacter.Lvl})";
        }
    }

    public void RegDelegate(MouseDown deleg)
    {
        this.mouseDown = deleg;
    }

}
