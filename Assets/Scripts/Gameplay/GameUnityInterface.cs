using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnityInterface : MonoBehaviour
{
    private static int i = 0;

    public void SetMap(int i)
    {
        GameManager.SetMap(i);
    }

    public void AddCharacter(string character)
    {
        GameManager.SetCharacter(i, character);
        i++;
    }

    public void Load()
    {
        GameManager.LoadGame();
    }

    public void Reset()
    {
        GameManager.characters = new Dictionary<int, GameObject>();
        GameManager.selectedMap = -1;
        i = 0;

    }
}
