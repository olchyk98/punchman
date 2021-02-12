using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    [SerializeField] private List<Sprite> mySprites = new List<Sprite>();

    [SerializeField] private CharacterChosen mySpritesHandler;

    [SerializeField] CharacterChosen CharacterManager;
    
    
    [SerializeField] private int myPlayerIndex; // at every player the player must input this to identify what number the players are
    
    private int CurrentSpriteIndex = 0; // the current shown character so that the game can display the correct image
    
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        CharacterManager = mySpritesHandler.GetComponent<CharacterChosen>();
        CharacterManager.PlayerAdd();
        CharacterManager.ChangeSelected(myPlayerIndex, CurrentSpriteIndex);
    }
    
    public void CharacterChange(int change)
    {
        CurrentSpriteIndex = Mathf.Clamp(CurrentSpriteIndex + change, 0, mySprites.Count - 1);
        image.sprite = mySprites[CurrentSpriteIndex];
        CharacterManager.ChangeSelected(myPlayerIndex, CurrentSpriteIndex);
    }
}
