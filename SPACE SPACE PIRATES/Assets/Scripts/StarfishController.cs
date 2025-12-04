using System.Linq;
using TMPro;
using UnityEngine;

public class StarfishController : MonoBehaviour
{
    public static StarfishController instance { get; private set; }

    GameObject[] starfish;
    public int count=0;
    [SerializeField] public TMP_Text Counter;
    
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

    public void getStars() {
        GameManager.instance.CurrentQuest.SetText("Collect Stars For The Pirate Captain");
        starfish = GameObject.FindGameObjectsWithTag("star");
    }

    public void DisplayCurrentStartCount() {
        int i = GameObject.FindGameObjectsWithTag("star").Length-1;
        Counter.SetText(i + "stars left");
        onZero(i);
    }

    public void onZero(int i) {
        if (i == 0) {
            GameManager.instance.UpdateGameState(GameState.Sequence05);
        }
    }


}
