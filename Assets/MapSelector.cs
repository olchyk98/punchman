using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    public void SelectMap(int i)
    {
        GameManager.Main.SetMap(i);
    }
}
