using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCreationProp : PanelCreation
{
    public override void Done()
    {
        if (inputName.text.Length > 0)
        {
            PropertyCharacter propertyCharacter = new PropertyCharacter(inputName.text, inputDescription.text);
            returnProp?.Invoke(propertyCharacter);
            Destroy(gameObject);
        }
    }
}
