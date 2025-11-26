using UnityEngine;



public class MainMenuStart : MonoBehaviour {
    public Transform getPlayer;
    public Transform playerNextSpawn;

    private void Awake()
    {
        getPlayer = GameObject.FindGameObjectWithTag("player").transform;
        playerNextSpawn = GameObject.FindGameObjectWithTag("spawn01").transform;
    }

    public void nextState() {

        GameManager.instance.UpdateGameState(GameState.Sequence01);
        Debug.Log("GameState.Sequence01");
    }

    public void QuitApplication() {
        Application.Quit();
        Debug.Log("Quit App");

    }


}
