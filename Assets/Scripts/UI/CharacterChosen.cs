using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChosen : MonoBehaviour
{
    public List<int> selected = new List<int>();

    public void PlayerAdd()
    {
        selected.Add(0);
    }
    
    public void ChangeSelected(int playernr, int character)
    {
        selected[playernr] = character;
    }
}
