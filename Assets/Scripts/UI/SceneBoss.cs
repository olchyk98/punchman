using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoss : MonoBehaviour
{
    public void MapSelect(int Select) // this gets called when the player wants to change scene
    {
        SceneManager.LoadScene(Select);
    }
}
