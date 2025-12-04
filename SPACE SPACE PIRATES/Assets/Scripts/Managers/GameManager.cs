using System;
using TMPro;
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
    [Header("Quest Text")]
    [SerializeField] public TMP_Text CurrentQuest;

    [Header("Player")]
    [SerializeField] public Player player;

    [Header("Portal Locations")]
    [SerializeField] public GameObject portal1;
    [SerializeField] public GameObject portal2;

    [Header("TO DISABLE MAIN MENU")]
    public GameObject MENU;

    [Header("Captain")]
    public GameObject CaptainScream_seq2;
    public GameObject CaptainScream_seq3;
    public GameObject CaptainScream_seq4;

    [Header("Stars")]
    public GameObject stars;
 


    [Header("Doors")]
    public GameObject DoorCoverKitchen;


    [Header("Current Respawn")]
    private GameObject currentSpawn;
    [Header("Spawns List")] // this is give to the handler to know where to respwan player,
    public GameObject menu;
    public GameObject seq01Spawn;
    public GameObject seq02Spawn;

    [Header("UI Pressed")]
    public bool isUIPressed;

    [Header("Cooking Minigame")]
    public GameObject seq03;
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
                setState(GameState.start);
                CurrentQuest.SetText("Finish The Tutorial!");

                isUIPressed = false; // allows for system to know if player has clicked a UI button

                // if not disabled
                CaptainScream_seq2.SetActive(false); 
                CaptainScream_seq3.SetActive(false);
                CaptainScream_seq4.SetActive(false);
                stars.SetActive(false);
                DoorCoverKitchen.SetActive(true);
                seq03.SetActive(false);//cooking minigame
                break;
            case GameState.Sequence01: //killing the crabs and meeting the captain
                setState(GameState.Sequence01);
                CaptainScream_seq2.SetActive(true);
                EnemySpawner._instance.isSpawning = false;
                HandleFirstSequence();
                break;
            case GameState.Sequence02:
                setState(GameState.Sequence02);
                CaptainScream_seq3.SetActive(true);
                HandleSecondSequence();
                break;
            case GameState.Sequence03:
                setState(GameState.Sequence03);
                HandleThirdSequence();

                break;
            case GameState.Sequence04:
                setState(GameState.Sequence04);
                HandleFourthSequence();

                break;
            case GameState.Sequence05:
                setState(GameState.Sequence05);
                HandleFifthSequence();

                break;
            case GameState.Sequence06:
                setState(GameState.Sequence06);
                HandleSixthSequence();

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
        //intercom captain says to kill the crabs onboard
        MusicManager.instance.UpdateGameMusic(GameMusic.BossCrab);
        PlayTransition();
        // Set spawn to room  (if player dies he can respawn)
        SetSpawnOfPlayer(seq01Spawn);
        TeleportPlayerToRespawnLocation();
        // to spawn enemy crabs WAVES  //camera shake is now avaible 
        player.OnDisable();
        // to talk to captain to get more INFO/QUESTS
        CurrentQuest.SetText("Kill all crabs, or you are fired...");


    }

    public void playerAllowMovement(GameState state) 
    {
        player.OnEnable();
        EnemySpawner._instance.isSpawning = true;
        EnemySpawner._instance.startSpawningEnemiesForSequence(state,0);
    }

    public void playerAllowMovement()
    {
        player.OnEnable();

    }


    void HandleSecondSequence() {
        SetSpawnOfPlayer(seq02Spawn);
        MusicManager.instance.UpdateGameMusic(GameMusic.spaceMusic);
        CurrentQuest.SetText("Fix The Captain's Brocken Circuit");

        //pipes? minigame?
        //START MINIGAME
        //Completed Pipes to next state 
        //GameManager.instance.UpdateGameState(GameState.Sequence03);
        

    }

    void HandleThirdSequence()
    {
        CurrentQuest.SetText("Go find Crew Member Chef");
        DoorCoverKitchen.SetActive(false);
        CaptainScream_seq4.SetActive(true);
        //seq03.SetActive(true);//cooking minigame
        MusicManager.instance.UpdateGameMusic(GameMusic.BossCrab);
        // Cooking in pot,
        // TALK TO CHEF
        //START MINIGAME
        CookingGame.instance.UpdateCookingGame(CookingGameState.Start);//SKIPPED due to time
        // WEIRD GLOW IN FREEZER
        // TO TELEPORT TO PIRATE LAND
        
    }


    void HandleFourthSequence()
    {
        CurrentQuest.SetText("Explore The Weird Area");
        seq03.SetActive(false);//cooking minigame
        MusicManager.instance.UpdateGameMusic(GameMusic.PirateMusic);
        // Talk to strange pirate bird
        //pirate says to collect starfish 
        // tell bird you collected the starfish
        //also enemies spawn upon traversing to diff islands
        //check the individual island portals to set enemies there 
        stars.SetActive(true);
        //pirate bird says he want to be captain of the world

    }


    void HandleFifthSequence()
    {
        CurrentQuest.SetText("A Portal Spawned?");
        MusicManager.instance.UpdateGameMusic(GameMusic.BossParrot);
        // When player finds out about the evil bird plans
        // player fights bird boss simialr to mini crab boss
        // bird defeated drops spaceship keys

    }

    void HandleSixthSequence()
    {
        MusicManager.instance.UpdateGameMusic(GameMusic.endingMusic);

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

    public void setState(GameState state) {
        this.State = state;
   
    }

    public GameState getState() {
        return State;
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
