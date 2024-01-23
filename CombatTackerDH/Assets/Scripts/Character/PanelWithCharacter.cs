using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PanelWithCharacter : MonoBehaviour, IPointerDownHandler, IPanel
{
    public delegate void MouseDown(Character character);
    MouseDown mouseDown;
    Character character;
    [SerializeField] TextMeshProUGUI textName, textTreate;
    string namePanel;
    public string Name { get => namePanel; }
    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown?.Invoke(character);
    }

    public void SetParams(Character character, MouseDown mouseDown)
    {
        gameObject.SetActive(true);
        textName.text = character.InternalName;
        textTreate.text = character.Treate.ToString();
        namePanel = character.InternalName;
        this.character = character;
        this.mouseDown = mouseDown;
    }
}
