using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShowCaseManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Arts = new List<GameObject>();

    private int ShownArt = 0;
    
    
    public void OnHover(int index)
    {
        Arts[ShownArt].SetActive(false);
        Arts[index].SetActive(true);
        ShownArt = index;
    }
}
