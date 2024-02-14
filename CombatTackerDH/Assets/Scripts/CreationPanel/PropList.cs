using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PropList : MonoBehaviour
{
    public delegate void ReturnProp(PropertyCharacter propertyCharacter);
    ReturnProp returnProp;
    [SerializeField] PanelPropForCreation panelPropExample;
    [SerializeField] Transform content;
    [SerializeField] TMP_InputField inputSearch;
    PanelCreation panelCreationExample;
    private List<PanelPropForCreation> panelProps = new List<PanelPropForCreation>();

    public void SetParams(List<PropertyCharacter> properties, ReturnProp returnProp, PanelCreation panelCreationExample)
    {
        this.panelCreationExample = panelCreationExample;
        this.returnProp = returnProp;
        foreach (PropertyCharacter prop in properties)
        {
            panelProps.Add(Instantiate(panelPropExample, content));
            panelProps[^1].SetParams(prop, ReturnProperty);
        }
    }

    private void ReturnProperty(PropertyCharacter propertyCharacter)
    {
        returnProp?.Invoke(propertyCharacter);
        Destroy(gameObject);
    }

    public void SearchProp()
    {
        foreach (PanelPropForCreation panel in panelProps)
        {
            if (inputSearch.text.Length > 0)
            {
                if (panel.Name.IndexOf(inputSearch.text, 0, panel.Name.Length, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    panel.gameObject.SetActive(false);
                }
            }
            else
            {
                panel.gameObject.SetActive(true);
            }
        }
    }

    public void CreateNewProperty()
    {
        PanelCreation panelCreation = Instantiate(panelCreationExample, transform);
        panelCreation.SetParams(ReturnProperty);
    }

    public void CloseList()
    {
        Destroy(gameObject);
    }
}
