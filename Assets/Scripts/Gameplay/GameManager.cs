using System.Collections.Generic;
using Gameplay;
using Gameplay.Materials;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{

    public const int mapSectionStart = 5;
    public static int selectedMap = -1;
    public static Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();

    public static void SetMap(int map)
    {
        selectedMap = mapSectionStart + map;
    }

    public static void SetCharacter(int player, string character)
    {
        characters.Add(player, PrefabManager.Main.GetPrefabByName(character));
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene(selectedMap);
    }

    public static void ResetState()
    {
        characters = new Dictionary<int, GameObject>();
        selectedMap = -1;
    }

}
