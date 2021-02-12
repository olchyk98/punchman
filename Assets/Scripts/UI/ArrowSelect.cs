using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    public List<Sprite> Sprites = new List<Sprite>();

    private int Shown = 0;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        
    }

    public void CharacterChange(int change)
    {
        image.sprite = Sprites[Mathf.Clamp(Shown + change, 0, Sprites.Count-1)];
    }
}
