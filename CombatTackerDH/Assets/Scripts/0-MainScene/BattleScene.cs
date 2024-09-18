using System.Collections.Generic;

public class BattleScene : IName
{
    private string _name;
    private List<Character> _characters = new List<Character>();

    public BattleScene(SaveLoadScene loadScene, List<Character> characters)
    {
        _name = loadScene.nameScene;
        _characters = new List<Character>(characters);
    }

    public string Name => _name;

    public List<Character> Characters => _characters;
}
