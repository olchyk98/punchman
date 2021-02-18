using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChosen : MonoBehaviour
{
    [SerializeField]
    List<int> SelectedCharacterIndex = new List<int>();
    [SerializeField]
    private List<string> characterNames = new List<string>();

    public void PlayerAdd() // this gets called in the arrowcontroller script and that means that for every character that exists there will be an spot on the list for them
    {
        SelectedCharacterIndex.Add(0);
    }
    
    public void ChangeSelected(int playernr, int character) // this will get called so that the game manager can keep track of all characters people have selected
    {
        SelectedCharacterIndex[playernr] = character;
    }

    public void ConfirmSelected()
    {
        for (int i = 0; i < 2; i++)
            GameManager.SetCharacter(i, characterNames[SelectedCharacterIndex[i]]);
        GameManager.LoadGame();
    }
}
