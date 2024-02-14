using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using System.Text;

public class PanelCreationMelee : PanelCreation
{
    [SerializeField] protected TMP_InputField inputClass, inputDamage, inputPenetration, inputWeight, inputRarity;
    [SerializeField] PanelPropForCreation propertyWeaponExample;
    [SerializeField] PanelWithInfo propPanelExample;
    [SerializeField] Transform contentlist, contentWeaponProp;
    [SerializeField] GameObject ListWithProps;
    protected List<PanelPropForCreation> propertiesWeaponInList = new List<PanelPropForCreation>();
    protected List<PanelWithInfo> propertiesInWeapon = new List<PanelWithInfo>();
    public override void Done()
    {
        float.TryParse(inputWeight.text, out float weight);
        int.TryParse(inputPenetration.text, out int penetration);
        string properties = "";
        foreach(PanelWithInfo panel in propertiesInWeapon)
        {
            properties += $"{panel.TextName},";
        }
        properties = DeleteLastChar(properties);
        PropertyMelee melee = new PropertyMelee(inputName.text, "", weight, inputRarity.text, inputClass.text, inputDamage.text, penetration, properties);
        returnProp?.Invoke(melee);
        Destroy(gameObject);
    }
    private void Start()
    {
        List<string> dirs = Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/PropertiesOfWeapon").ToList();
        foreach(string path in dirs)
        {
            name = ReadText(path + "/Название.txt");
            propertiesWeaponInList.Add(Instantiate(propertyWeaponExample, contentlist));
            propertiesWeaponInList[^1].SetParams(new PropertyCharacter(name, ""), AddProperty);
        }
    }

    private void AddProperty(PropertyCharacter propertyCharacter)
    {
        propertiesInWeapon.Add(Instantiate(propPanelExample, contentWeaponProp));
        propertiesInWeapon[^1].SetParams(propertyCharacter, RemoveProperty);
        ListWithProps.SetActive(false);
    }

    private void RemoveProperty(string name)
    {
        foreach(PanelWithInfo panel in propertiesInWeapon)
        {
            if(string.Compare(name, panel.Name, true) == 0)
            {
                propertiesInWeapon.Remove(panel);
                break;
            }
        }
    }
    private string ReadText(string nameFile)
    {
        string txt;
        using (StreamReader _sw = new StreamReader(nameFile, Encoding.Default))
        {
            txt = (_sw.ReadToEnd());
            _sw.Close();
        }
        return txt;
    }
    protected string DeleteLastChar(string text)
    {
        if (text.Length > 0)
        {
            string tex = text.TrimEnd(',');
            return tex;
        }
        else
        {
            return text;
        }

    }
}
