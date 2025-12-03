using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This is from my first game.

// Meant for a quest for a player to connect the stars
// to obtain a code to unlock something.

// This is the script for the individual stars.
// Meant for prefab.

public class StarSystem : MonoBehaviour, IPointerClickHandler
{
    //The constellation will be preset.
    //Each star has its position according to the constellation.
    //This will control how the player connects each star.
    //To avoid random connections.
    public int starID;
    public bool isSelected = false;

    public ConnectStarsQuest manager;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public float selectedScale;
    
    private Image starImage;
    private Vector3 originalScale;
    
    //On game start, the stars should display in normal scale.
    //Waiting for the player to click on it.
    void Start()
    {
        starImage = GetComponent<Image>();

        originalScale = transform.localScale;
        SetSelected(false);
    }
    
    //Make sure store the player's clicks.
    public void OnPointerClick(PointerEventData eventData)
    {
        if(manager != null)
        {
            manager.SelectStar(this);
        }
    }
    
    //And the star will scale up to show that it has been selected.
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        starImage.color = selected ? selectedColor : normalColor;
        transform.localScale = selected ? originalScale * selectedScale : originalScale;
    }
}
