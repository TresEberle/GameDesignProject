using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject CutsceneCanvas; //has the CutsceneSpeechManager script as a component, will extract later for cutscene

    CutsceneSpeechManager cutsceneSpeechManager; //we'll put CutsceneSpeechManager component here

    public float buttonGrowth = 1.5f; //How much the button grows when cursor hovers it.

    public Vector3 ogButtonSize; //Stores OG button size.

    public bool IsCutsceneDone; //check for cutscene being done
    void Start()
    {
        //goes to CutsceneCanvas and gets its CutsceneSpeechManager script component
        cutsceneSpeechManager = CutsceneCanvas.GetComponent<CutsceneSpeechManager>();

        //Stores OG button size as the size it started as. 
        ogButtonSize = transform.localScale;
    }

   void Update()
    {
        //checks if the cutscene is done (from boolean value in CutsceneSpeechManager script)
        IsCutsceneDone = cutsceneSpeechManager.cutsceneDone;

        if (IsCutsceneDone == true) //load main game when cutscene is done
        {
            SceneManager.LoadSceneAsync("Main Scene");
        }
    }

    public void startGame() //when game "starts" the cutscene will play
        {

            cutsceneSpeechManager.turnOnCutscene = true; //lets us iterate through cutscene dialogue
            
            CutsceneCanvas.SetActive(true);
        }

    public void exitGame()
        {
            Application.Quit();
        }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("We have entered button!");
        transform.localScale = ogButtonSize * buttonGrowth; //scales button by ButtonGrowth
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("We have exited button!");
        transform.localScale = ogButtonSize; //scales button back to OG size
    }
}
