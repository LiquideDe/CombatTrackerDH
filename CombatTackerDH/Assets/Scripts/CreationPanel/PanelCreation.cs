using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PanelCreation : MonoBehaviour
{
    [SerializeField]protected TMP_InputField inputName, inputDescription;

    public delegate void ReturnProp(PropertyCharacter propertyCharacter);
    protected ReturnProp returnProp;
    public void SetParams(ReturnProp returnProp)
    {
        gameObject.SetActive(true);
        this.returnProp = returnProp;
    }

    public abstract void Done();

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
