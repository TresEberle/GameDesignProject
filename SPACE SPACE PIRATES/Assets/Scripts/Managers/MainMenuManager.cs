using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

public void startGame()
    {
        SceneManager.LoadSceneAsync("Main Scene");
    }

public void exitGame()
    {
        Application.Quit();
    }

}
