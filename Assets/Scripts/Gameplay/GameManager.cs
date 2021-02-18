using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int mapSectionStart;

    public static GameManager Main;

    private int selectedMap = -1;
    private Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
    private void Awake()
    {
        if(Main != null && Main != this) {
            DestroyImmediate(gameObject);
            return;
        }
        Main = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += StartGame;
    }

    public void SetMap(int map)
    {
        selectedMap = mapSectionStart + map;
    }

    public void SetCharacter(int player, GameObject character)
    {
        characters.Add(player, character);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(selectedMap);
    }

    private void StartGame(Scene s, LoadSceneMode mode)
    {
        if (s.buildIndex == selectedMap)
        {
            MatchManager m = GameObject.Find("Match Manager").GetComponent<MatchManager>();
            m.playerPrefabs = characters;
        }
    }
    
}
