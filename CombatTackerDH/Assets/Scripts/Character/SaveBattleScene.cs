using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SaveBattleScene : MonoBehaviour
{
    [SerializeField] TMP_InputField inputNameScene;
    List<string> data = new List<string>();
    public void ShowPanelSave(List<CharacterVisual> characters)
    {
        gameObject.SetActive(true);
        
        foreach(CharacterVisual character in characters)
        {
            SaveLoadCharacterNames saveCharacter = new SaveLoadCharacterNames();
            saveCharacter.internalNamesCharacter = character.InternalName;
            saveCharacter.nameCharacter = character.Name;
            data.Add(JsonUtility.ToJson(saveCharacter));
        }
    }

    public void Save()
    {
        if(inputNameScene.text.Length > 0)
        {            
            var path = Path.Combine($"{Application.dataPath}/StreamingAssets/BattleScenes/", inputNameScene.text + ".JSON");
            File.WriteAllLines(path, data);
            Cancel();
        }
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
