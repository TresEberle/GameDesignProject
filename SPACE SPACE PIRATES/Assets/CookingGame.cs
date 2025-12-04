using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum CookingGameState {
    Start,
    Wait,
    RandomItem,
    Done,


}


public class CookingGame : MonoBehaviour
{


    [Header("Obj access to Player")]
    [SerializeField]
    GameObject player;

    [Header("Obj Hover On Player")]
    [SerializeField]
    GameObject itemHover;


    [Header("Item Looking For")]
    [SerializeField]
    TextMeshPro ItemString;

    [SerializeField]
    SpriteRenderer ItemSprite;

    [Header("Cooking List")]
    [SerializeField]
    public List<GameObject> CookingItems = new List<GameObject>();


    [Header("Item Locations")]
    [SerializeField]
    public List<GameObject> Items_Prefab = new List<GameObject>();

    CookingGameState state;

    int randomNumber;
    int Rounds = 5;
    int currentRound = 0;

    public static event Action<CookingGameState> OnCookChanged;
    public static CookingGame instance { get; private set; }

    public List<GameObject> curr = new List<GameObject>();

    public void UpdateCookingGame(CookingGameState newState)
    {
        state = newState;

        switch (newState)
        {
            case CookingGameState.Start:
                handleStart();
                break;
            case CookingGameState.Wait:
                handleWait();
                break;
            case CookingGameState.RandomItem:
                randomItemPicker();
                break;
            case CookingGameState.Done:
                GameManager.instance.CurrentQuest.SetText("Investigate The Freezer Glow");
                break;


            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnCookChanged?.Invoke(newState);

    }

    void handleStart()
    {
        UpdateCookingGame(CookingGameState.Wait);
    }


    void handleWait() {
        UpdateCookingGame(CookingGameState.RandomItem);


    }


    void randomItemPicker() 
    {
        UpdateCookingGame(CookingGameState.Done);





        //System.Random random = new System.Random();
        //randomNumber = random.Next(0, CookingItems.Count);

        ////  Looking For Ingredient 
        //string x = "I need ";
        //ItemString.text = x + CookingItems[randomNumber].GetComponent<itemIngredient>().getItemName();
        //ItemSprite.sprite = CookingItems[randomNumber].GetComponent<itemIngredient>().cooking_sprite.sprite;
        ////  Random 3 SPOTS 1 RIGHT ANSWER

        //string y;

        //for (int i = 0; i < Items_Prefab.Count-1; i++) {
        //    int v = random.Next(0, Items_Prefab.Count);
        //    Items_Prefab[i].SetActive(false);
        //    curr.Add(Instantiate(CookingItems[v], Items_Prefab[i].transform.position, Quaternion.identity));

        //}


    }




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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
