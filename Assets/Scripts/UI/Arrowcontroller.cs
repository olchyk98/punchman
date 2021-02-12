using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrowcontroller : MonoBehaviour
{
    [SerializeField]
    List<Sprite> Sprites = new List<Sprite>();
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void ChangeArrowSprite(int spriteIndex)
    {
        image.sprite = Sprites[spriteIndex];
    }
}
