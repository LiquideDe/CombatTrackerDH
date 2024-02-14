using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductingFight : MonoBehaviour
{
    int whoTurn;
    List<CharacterVisual> characters = new List<CharacterVisual>();

    public void StartBattle(List<CharacterVisual> characters)
    {
        this.characters = characters;
        
    }

    
}
