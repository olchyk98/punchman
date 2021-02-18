using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    private Image image;

    [SerializeField] private List<Sprite> mySprites = new List<Sprite>(); // all character sprites

    private CharacterChosen mySpritesHandler; // the class that saves the selected characters

    [SerializeField] GameObject CharacterManager; // the manager object that contains CharacterChosen


    [SerializeField] private int myPlayerIndex; // at every player the player must input this to identify what number the players are

    private int CurrentSpriteIndex = 0; // the current shown character so that the game can display the correct image

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        mySpritesHandler = CharacterManager.GetComponent<CharacterChosen>();
        mySpritesHandler.PlayerAdd(); // adds the player to the list inside of the manager
    }

    public void CharacterChange(int change) // this gets called when an Onclick() command is recived with an 1 or an -1
    {
        CurrentSpriteIndex = Mathf.Clamp(CurrentSpriteIndex + change, 0, mySprites.Count -1);
        // changes the Shown sprite index and clamps it by the minimum value and the max sprite count
        image.sprite = mySprites[CurrentSpriteIndex];
        // changes the current displayed sprite to the correct sprite
        mySpritesHandler.ChangeSelected(myPlayerIndex, CurrentSpriteIndex);
        // tells the manager that the shown sprite has been changed so it can store it until the player comfirms the slected characters.
    }
}
