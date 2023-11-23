using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JustNew : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Game game;
    public void OnPointerDown(PointerEventData eventData)
    {
        game.ShowPanelForNewCharacter();
    }

}
