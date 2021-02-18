using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Music;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        yield return 0;
        MusicManager.Main.PlayRandomTrackOfType(MusicType.MAIN_MENU);
    }
}
