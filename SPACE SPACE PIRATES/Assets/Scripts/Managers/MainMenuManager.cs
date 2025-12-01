using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float buttonGrowth = 1.5f; //How much the button grows when cursor hovers it.

    public Vector3 ogButtonSize; //Stores OG button size.

    void Start()
    {
        //Stores OG button size as the size it started as. 
        ogButtonSize = transform.localScale;
    }

    public void startGame()
        {
            SceneManager.LoadSceneAsync("Main Scene");
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
