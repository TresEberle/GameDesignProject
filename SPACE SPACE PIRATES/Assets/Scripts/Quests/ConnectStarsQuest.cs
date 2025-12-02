using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

// This is from my first game.

// Meant for a quest for a player to connect the stars
// to obtain a code to unlock something.

//This will hold the constellations, this is better
//for controlling what constellation shows.
//And each have their own unique shape.
public class ConstellationData
{
    public string name;
    public StarData[] stars;
    public ConnectionData[] connections;
}

//Here holds the stars themselves.
//Meant for the constellations.
public class StarData
{
    public Vector2 position;
    public int starID;
}

//Here holds the connections between each star.
public class ConnectionData
{
    public int starA;
    public int starB;
    public bool isConnected = false;
}

public class ConnectStarsQuest : MonoBehaviour
{
    private bool roundOver = false;

    public GameObject StarsPanel;
    public AudioClip correctBeep;
    
    //starContainer holds the starPrefab for the star constellations
    //lineContainer holds the linePrefab for the lines
    public Transform starContainer;
    public Transform lineContainer;
    public GameObject starPrefab;
    public GameObject linePrefab;

    //what constellation on
    private int currentConstellationIndex = 0;

    //player's selection 
    private StarSystem firstSelectedStar = null;
    private StarSystem secondSelectedStar = null;

    private List<GameObject> currentStars = new List<GameObject>();
    private List<LineSystem> currentLines = new List<LineSystem>();

    private ConstellationData[] constellations;

    //This will control what panel shows and when.
    void ShowPanel(GameObject panel)
    {
        StarsPanel.SetActive(false);

        panel.SetActive(true);
    }

    void Start()
    {
        ShowPanel(StarsPanel);

        //And the player should see the constellation immediately
        SetupConstellations();

        //the game should start at Lion, with 5 lives, fresh at no constellations done,
        //the timer is at 30 seconds counting down, and the round has just begun
        currentConstellationIndex = 0;
        roundOver = false;

        //show the correct constellation
        LoadConstellation(currentConstellationIndex);
    }

    void Update()
    {
        if(roundOver)
        {
            return;
        }

        bool allConnected = true;

        foreach(LineSystem line in currentLines)
        {
            if(!line.isConnected)
            {
                allConnected = false;
                break;
            }
        }

        //once completed, close panel and go back to game
        if(!roundOver && allConnected)
        {
            roundOver = true;

        }
    }

    //These are the coordinates that will show on the screen for the player.
    // Code is 1074, which has 11 lines and 14 stars total
    void SetupConstellations()
    {
        constellations = new ConstellationData[1];

        constellations[0] = new ConstellationData();
        constellations[0].name = "4DigCode";
        constellations[0].stars = new StarData[]
        {
            new StarData{position = new Vector2(-700,300), starID = 0},//Number 1
            new StarData{position = new Vector2(-700,-300), starID = 1},

            new StarData{position = new Vector2(-400,300), starID = 2},//Number 0
            new StarData{position = new Vector2(-100,300), starID = 3},
            new StarData{position = new Vector2(-400,-300), starID = 4},
            new StarData{position = new Vector2(-100,-300), starID = 5},

            new StarData{position = new Vector2(100,300), starID = 6},//Number 7
            new StarData{position = new Vector2(400,300), starID = 7},
            new StarData{position = new Vector2(400,-300), starID = 8},

            new StarData{position = new Vector2(600,300), starID = 9},//Number 4
            new StarData{position = new Vector2(600,100), starID = 10},
            new StarData{position = new Vector2(800,300), starID = 11},
            new StarData{position = new Vector2(800,100), starID = 12},
            new StarData{position = new Vector2(800,-300), starID = 13}
        };

        // Connect the stars for guidance
        constellations[0].connections = new ConnectionData[]
        {
            new ConnectionData{starA = 0, starB = 1},
            new ConnectionData{starA = 2, starB = 3},
            new ConnectionData{starA = 2, starB = 4},
            new ConnectionData{starA = 4, starB = 5},
            new ConnectionData{starA = 3, starB = 5},
            new ConnectionData{starA = 6, starB = 7},
            new ConnectionData{starA = 7, starB = 8},
            new ConnectionData{starA = 9, starB = 10},
            new ConnectionData{starA = 10, starB = 12},
            new ConnectionData{starA = 11, starB = 12},
            new ConnectionData{starA = 12, starB = 13},
        };
    }

    //show the constellation to the player
    void LoadConstellation(int index)
    {
        ClearCurrentConstellation();

        roundOver = false;

        ConstellationData constellation = constellations[index];
        
        //StarSystem file:
        //this will show the stars based on the SetupConstellations information
        //of the exact position and which stars are connected.
        //The stars should be held in that container and be in the prefab
        for(int i = 0; i < constellation.stars.Length; i++)
        {
            GameObject starObj = Instantiate(starPrefab, starContainer);
            RectTransform starRect = starObj.GetComponent<RectTransform>();
            starRect.anchoredPosition = constellation.stars[i].position;
            
            StarSystem starScript = starObj.GetComponent<StarSystem>();
            starScript.starID = constellation.stars[i].starID;
            starScript.manager = this;
            
            currentStars.Add(starObj);
        }
        
        //LineSystem file:
        //this will show the lines between the stars based on the SetupConstellations information
        //of the exact position and which stars are connected.
        //The lines should be held in that container and be in the prefab
        for(int i = 0; i < constellation.connections.Length; i++)
        {
            ConnectionData connection = constellation.connections[i];
            
            Vector2 pos1 = currentStars[connection.starA].GetComponent<RectTransform>().anchoredPosition;
            Vector2 pos2 = currentStars[connection.starB].GetComponent<RectTransform>().anchoredPosition;

            GameObject lineObj = Instantiate(linePrefab, lineContainer);
            LineSystem lineScript = lineObj.GetComponent<LineSystem>();
            lineScript.SetupLine(pos1, pos2, connection.starA, connection.starB);
            
            currentLines.Add(lineScript);
        }
    }

    //once the player has finished connecting and ready to move to the next constellation,
    //the screen will clear in preparation for the next
    void ClearCurrentConstellation()
    {
        //no more stars
        foreach(GameObject star in currentStars)
        {
            Destroy(star);
        }
        currentStars.Clear();

        //no more lines
        foreach(LineSystem line in currentLines)
        {
            if(line != null)
            {
                Destroy(line.gameObject);
            }
        }
        currentLines.Clear();
        //clear the selections since done with this constellation
        firstSelectedStar = null;
        secondSelectedStar = null;
    }

    //once the player clicks the star
    public void SelectStar(StarSystem star)
    {
        //first star the player has clicked on
        if(firstSelectedStar == null)
        {
            firstSelectedStar = star;
            star.SetSelected(true);
        }//next star clicked on and so on
        else if(secondSelectedStar == null && star != firstSelectedStar)
        {
            secondSelectedStar = star;
            star.SetSelected(true);
            CheckConnection();//make sure it is actually connected
        }
        else
        {
            //otherwise, wait the player to connect
            if(firstSelectedStar != null)
            {
                firstSelectedStar.SetSelected(false);
            }
            if(secondSelectedStar != null)
            {
                secondSelectedStar.SetSelected(false);
            }
            
            firstSelectedStar = star;
            secondSelectedStar = null;
            star.SetSelected(true);
        }
    }

    void CheckConnection()
    {
        // Is the connection valid?
        // If so, make it light up
        // Find the matching line
        foreach(LineSystem line in currentLines)
        {
            if ((line.starA == firstSelectedStar.starID && line.starB == secondSelectedStar.starID) ||
                (line.starB == firstSelectedStar.starID && line.starA == secondSelectedStar.starID))
            {
                line.SetConnected();
                if(correctBeep != null)//this will notify the player if correctly connected
                {
                    AudioSource.PlayClipAtPoint(correctBeep, Camera.main.transform.position);
                }
                break;
            }
        }
        
        //clear it once done
        firstSelectedStar.SetSelected(false);
        secondSelectedStar.SetSelected(false);
        firstSelectedStar = null;
        secondSelectedStar = null;
    }
}
