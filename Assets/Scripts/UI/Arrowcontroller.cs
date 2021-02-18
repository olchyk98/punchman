using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrowcontroller : MonoBehaviour
{
    [SerializeField]
    List<Sprite> Sprites = new List<Sprite>(); // the sprites the arrow will change to
    private Image image; // the image that will change sprites
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>(); // gets the component of the gameObject
    }
    public void ChangeArrowSprite(int spriteIndex) // this gets called when the arrow wants to change sprites
    {
        image.sprite = Sprites[spriteIndex];
    }
}
