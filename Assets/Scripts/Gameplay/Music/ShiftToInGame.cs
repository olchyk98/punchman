using System.Collections;
using System.Collections.Generic;
using Gameplay.Music;
using UnityEngine;

public class ShiftToInGame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        MusicManager.Main.PlayRandomTrackOfType(MusicType.IN_GAME);
        StartCoroutine(StartIntenseAfter(60));
    }

    IEnumerator StartIntenseAfter(int delay)
    {
        yield return new WaitForSeconds(delay);
        MusicManager.Main.PlayRandomTrackOfType(MusicType.IN_GAME_INTENSE);
    }
}
