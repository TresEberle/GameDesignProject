using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Current Story State
public enum GameState {
    //Linear Porgression Instead of multiple different progressions
    start,
    Sequence01,
    Sequence02,
    Sequence03,
    Sequence04,
    Sequence05,
    Sequence06,
    end,
}




public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public Player player;

    [Header("Portal Locations")]
    [SerializeField] public GameObject portal1;
    [SerializeField] public GameObject portal2;

    [Header("TO DISABLE MAIN MENU")]
    public GameObject MENU;

    [Header("Current Respawn")]
    private GameObject currentSpawn;
    [Header("Spawns List")] // this is give to the handler to know where to respwan player,
    public GameObject menu;
    public GameObject seq01Spawn;
    public GameObject seq02Spawn;

    [Header("Buttons")]
    public Button returntoMenuButton;
    public Button exitGameButton;

    [Header("Death Scene")]
    public GameObject deathCanvas;
    public GameObject scenePanel;
    public GameObject speechPanel;

    [Header("Current Transition Picture")]
    public GameObject pic;

    public Animator animator;
    public bool animatedTransitionPlays = false;
    public bool isSpaceShip = true;
  
    GameState State;

    public static event Action<GameState> OnStateChanged;
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

        UpdateGameState(GameState.start);
        pic.SetActive(true);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.start:
                
                break;
            case GameState.Sequence01: //killing the crabs and meeting the captain

                HandleFirstSequence();
                break;
            case GameState.Sequence02:
                HandleSecondSequence();
                break;
            case GameState.Sequence03:
                break;
            case GameState.Sequence04:
                break;
            case GameState.Sequence05:
                break;
            case GameState.Sequence06:
                break;
            case GameState.end:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState,null);
        }

        OnStateChanged?.Invoke(newState);
      
    }

    // each handler needs to set a spawn to location
    void HandleFirstSequence()
    {
        MENU.SetActive(false);
        MusicManager.instance.UpdateGameMusic(GameMusic.BossCrab);
        PlayTransition();
        // Set spawn to room  (if player dies he can respawn)
        SetSpawnOfPlayer(seq01Spawn);
        TeleportPlayerToRespawnLocation();
        // to spawn enemy crabs WAVES
        //intercom captain says to kill the crabs onboard
        //camera shake is now avaible 
        // KILL 10 CRABS
        // to talk to captain to get more INFO/QUESTS
    }

    void HandleSecondSequence() {
        SetSpawnOfPlayer(seq02Spawn);
        // fix pipes? minigame?

    }

    void HandleThirdSequence()
    {
        // Cooking in pot,
        // TALK TO CHEF
        // WEIRD GLOW IN FREEZER
        // TO TELEPORT TO PIRATE LAND

    }


    void HandleFourthSequence()
    {
      // Talk to strange pirate bird
      //pirate says to collect starfish 
      // tell bird you collected the starfish
      //aslso enemies spawn upon traversing to diff islands,
      //check the individual island portals to set enemies there 

        //pirate bird says he want to be captain of the world

    }


    void HandleFifthSequence()
    {
        // When player finds out about the evil bird plans
        // player fights bird boss simialr to mini crab boss
        // bird defeated drops spaceship keys

    }

    void HandleSixthSequence()
    {
        // Ship Flies Away and kid wakes up
        // end credits
        // change song

    }



    void SetSpawnOfPlayer(GameObject spawnObjectPos) {
        currentSpawn = spawnObjectPos;
    }

    Transform CurrentPlayerSpawnPositon() {
        return currentSpawn.GetComponent<Transform>();
    }

    void TeleportPlayerToRespawnLocation() {
        Transform currSpawn = CurrentPlayerSpawnPositon();
        playerTeleport(new Vector2(currSpawn.position.x, currSpawn.position.y));
    }

    void PlayTransition() {
       animator.SetTrigger("START");
    }

    public void playerTeleport(Vector2 playerPosition) 
    {
      player.transform.position = playerPosition;        
    }

    public void playerTeleportToPirateShip() { //test
        playerTeleport(portal2.GetComponent<Transform>().position);
    }

    public void playerTeleportToSpaceShip()//test
    {
        playerTeleport(portal1.GetComponent<Transform>().position);

    }

    public void playerTeleport() {

        if (!isSpaceShip)
        {
            playerTeleportToPirateShip();
            isSpaceShip = !isSpaceShip;

        }
        else 
        {
            playerTeleportToSpaceShip();
            isSpaceShip = !isSpaceShip;

        }
    }

    // Only show the death scene panels when player is dead:
    public void showScene()
    {
        if(deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
    }

    //when player dies, let them have two options:
    //go back to menu or exit game
    public void goToMain()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void exitGame()
    {
        Application.Quit();
   }

}
