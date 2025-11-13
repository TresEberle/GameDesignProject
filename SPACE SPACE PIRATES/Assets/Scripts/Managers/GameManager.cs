using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
 
    [Header("Music Background")]  
    public static int currentSong = 0;
    public  AudioClip[] backgroundMusic;
    [SerializeField] public  AudioSource audioSource;

    [Header("Player")]
    [SerializeField] public Player player;

    [Header("Portal Locations")]
    [SerializeField] public Vector2 portal1;
    [SerializeField] public Vector2 portal2;

    //to keep track of camera confiner (might not be implemented) 
    [Header("Cinemachine Camera Confiner")] 
    public static GameObject[] CameraConfiner { get; set; }
    
    //to keep track player location
    bool isInSpaceship { get; set; } 
    bool isInPirateShip { get; set; }

    //to keep track world states. locked doors... 


    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;        
        }
    }


    private void Start()
    {
        
    }

    // PLAYER TELEPORT -- 
    public void playerTeleport(Vector2 playerPosition) 
    {
        player.transform.position = playerPosition;
        
    }

    public void playerTeleportToPirateShip() { //test
        playerTeleport(portal2);
    }

    public void playerTeleportToSpaceShip()//test
    {
        playerTeleport(portal1);
    }




    //MUSIC  
    public void currentSongPlaying() {
        Debug.Log(backgroundMusic[currentSong].name);
        return;
    }

    public void changeSong(int songID) {
        currentSong = songID;
        audioSource.clip = backgroundMusic[songID];
        audioSource.Play();
        currentSongPlaying();
    }

    public void PirateShipSong() //Test
    {
        changeSong(1);
    }

    public void SpaceShipSong() //Test
    { 
        changeSong(0);
    }


}
