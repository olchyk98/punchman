using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    public List<Sprite> Sprites = new List<Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void ArrowChange(int index)
    {
        image.sprite = Sprites[index];
    }
}
