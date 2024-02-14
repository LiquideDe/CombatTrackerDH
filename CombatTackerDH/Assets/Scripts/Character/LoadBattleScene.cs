using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadBattleScene : MonoBehaviour
{
    public delegate void ReturnListNames(List<SaveLoadCharacterNames> characters);
    ReturnListNames returnListNames;
    [SerializeField] SaveSlot saveSlotExample;
    [SerializeField] Transform content;
    private List<SaveSlot> saveSlots = new List<SaveSlot>();
    public void ShowLoads(ReturnListNames returnListNames)
    {
        gameObject.SetActive(true);
        this.returnListNames = returnListNames;
        string[] files = Directory.GetFiles($"{Application.dataPath}/StreamingAssets/BattleScenes", "*.JSON");
        foreach (string file in files)
        {
            saveSlots.Add(Instantiate(saveSlotExample, content));
            saveSlots[^1].SetParams(Path.GetFileName(file), LoadBattle);
        }
    }

    public void LoadBattle(string name)
    {
        string[] data = File.ReadAllLines($"{Application.dataPath}/StreamingAssets/BattleScenes/{name}");
        List<SaveLoadCharacterNames> scenes = new List<SaveLoadCharacterNames>();
        foreach (string character in data)
        {
            SaveLoadCharacterNames loadCharacter = JsonUtility.FromJson<SaveLoadCharacterNames>(character);
            scenes.Add(loadCharacter);
        }
        returnListNames?.Invoke(scenes);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
