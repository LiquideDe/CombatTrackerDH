using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

public class SaveSlot : MonoBehaviour, IPointerDownHandler
{
    public delegate void ReturnName(string name);
    ReturnName returnName;
    [SerializeField] TextMeshProUGUI textName;
    string nameSave;
    public void OnPointerDown(PointerEventData eventData)
    {
        returnName?.Invoke(nameSave);
    }

    public void SetParams(string nameSave, ReturnName returnName)
    {
        gameObject.SetActive(true);
        this.nameSave = nameSave;
        this.returnName = returnName;
        textName.text = nameSave;
    }
    public void RemoveThisSave()
    {
        File.Delete($"{Application.dataPath}/StreamingAssets/BattleScenes/{nameSave}");
        Destroy(gameObject);
    }
}
