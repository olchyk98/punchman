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

    
    
    [SerializeField] int PlayerNR; // at every player the player must input this to identify what number the players are
    
    private int ShownSpriteNR = 0; // the current shown character so that the game can display the correct image
    
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        var CharacterManager = Manager.GetComponent<CharacterChosen>();
        CharacterManager.PlayerAdd();
        CharacterManager.ChangeSelected(PlayerNR, ShownSpriteNR);
    }
    
    public void CharacterChange(int change)
    {
        ShownSpriteNR = Mathf.Clamp(ShownSpriteNR + change, 0, Sprites.Count - 1);
        image.sprite = Sprites[ShownSpriteNR];
        var CharacterManager = Manager.GetComponent<CharacterChosen>();
        CharacterManager.ChangeSelected(PlayerNR, ShownSpriteNR);
    }
}
