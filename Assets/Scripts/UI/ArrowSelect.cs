using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    public List<Sprite> Sprites = new List<Sprite>();

    [SerializeField] GameObject Manager;

    
    
    [SerializeField] int PlayerNR;
    
    private int Shown = 0;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        var CharacterIndex = Manager.GetComponent<CharacterChosen>();
        CharacterIndex.PlayerAdd();
        CharacterIndex.ChangeSelected(PlayerNR, Shown);
    }
    
    public void CharacterChange(int change)
    {
        Shown = Mathf.Clamp(Shown + change, 0, Sprites.Count - 1);
        image.sprite = Sprites[Shown];
        var CharacterIndex = Manager.GetComponent<CharacterChosen>();
        CharacterIndex.ChangeSelected(PlayerNR, Shown);
    }
}
