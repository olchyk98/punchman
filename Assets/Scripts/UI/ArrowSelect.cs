using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    [SerializeField] private List<Sprite> mySprites = new List<Sprite>();

    private CharacterChosen mySpritesHandler;

    [SerializeField] GameObject CharacterManager;


    [SerializeField] private int myPlayerIndex; // at every player the player must input this to identify what number the players are

    private int CurrentSpriteIndex = 0; // the current shown character so that the game can display the correct image

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        mySpritesHandler = CharacterManager.GetComponent<CharacterChosen>();
        mySpritesHandler.PlayerAdd();
    }

    public void CharacterChange(int change)
    {
        CurrentSpriteIndex = Mathf.Clamp(CurrentSpriteIndex + change, 0, mySprites.Count -1);
        image.sprite = mySprites[CurrentSpriteIndex];
        mySpritesHandler.ChangeSelected(myPlayerIndex, CurrentSpriteIndex);
    }
}
