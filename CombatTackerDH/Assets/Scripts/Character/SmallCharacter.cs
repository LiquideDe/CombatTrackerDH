using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SmallCharacter : MonoBehaviour, IPointerDownHandler
{
    public delegate void MouseDown(int id);
    MouseDown mouseDown;
    int id;
    [SerializeField] TextMeshProUGUI textname;

    public int Id { get => id; }
    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown?.Invoke(id);
    }

    public void SetParams(string name, int id, MouseDown mouseDown)
    {
        gameObject.SetActive(true);
        textname.text = name;
        this.id = id;
        this.mouseDown = mouseDown;
    }

    public void UpdateName(string name)
    {
        textname.text = name;
    }
}