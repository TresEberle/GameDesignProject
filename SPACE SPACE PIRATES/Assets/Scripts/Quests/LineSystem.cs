using UnityEngine;
using UnityEngine.UI;

// This is from my first game.

// Meant for a quest for a player to connect the stars
// to obtain a code to unlock something.

// This is the script for the individual lines between the stars.
// Meant for prefab.

public class LineSystem : MonoBehaviour
{
    //I want the guidelines to show as white,
    //and then the player connects the lines 
    //to turn it blue.
    public Color dimColor = new Color(1f,1f,1f,0.3f);
    public Color glowColor = Color.blue;
    public float dimHeight = 2f;
    public float glowHeight = 4f;

    private Image lineImage;
    public bool isConnected = false;
    public int starA, starB;

    //So, the line should be "dim" (white).
    void Start()
    {
        lineImage = GetComponent<Image>();
        SetDimState();
    }

    //Make the guidelines connect.
    public void SetConnected()
    {
        if(isConnected)
        {
            return;
        }

        isConnected = true;
        lineImage.color = glowColor;
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, glowHeight);
    }
    
    //Guidelines are white:
    public void SetDimState()
    {
        lineImage.color = dimColor;
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, dimHeight);
    }
    
    public void SetupLine(Vector2 pos1, Vector2 pos2, int starAID, int starBID)
    {
        starA = starAID;
        starB = starBID;
        
        RectTransform rect = GetComponent<RectTransform>();
        
        // Calculate position (midpoint)
        Vector2 midPoint = (pos1 + pos2) / 2;
        rect.anchoredPosition = midPoint;
        
        // Calculate distance and rotation
        Vector2 direction = pos2 - pos1;
        float distance = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Set size and rotation
        rect.sizeDelta = new Vector2(distance, dimHeight);
        rect.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
