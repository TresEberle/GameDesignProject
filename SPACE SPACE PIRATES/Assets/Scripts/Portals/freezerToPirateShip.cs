using UnityEngine;

public class freezerToPirateShip : MonoBehaviour
{

    public bool isFreezer;
    public float distance = 0.2f;

    public bool IsCutsceneDone; //check for cutscene being done
    public GameObject CutsceneCanvas2; //reference to 2nd cutscene canvas

    CutsceneSpeechManager cutsceneSpeechManager; //Put CutsceneSpeechManager component here

    public To_Teleport tp { get; private set; }

    private void Awake()
    {
        tp = new To_Teleport(isFreezer, distance);

        cutsceneSpeechManager = CutsceneCanvas2.GetComponent<CutsceneSpeechManager>();
    }

    void Start()
    {
        tp.toTeleport("FreezerSpaceShip", "FreezerToPirateShip"); //this could be done as a get tag and we can write less code
    }


    public void OnTriggerEnter2D(Collider2D other)
    {

        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
    
            tp.teleportTransition();
            other.transform.position = new Vector2(tp.destination.position.x, tp.destination.position.y);

            //cutcene 2 trigger logic

            IsCutsceneDone = cutsceneSpeechManager.cutsceneDone;

            if (IsCutsceneDone == false)
            {
                cutsceneSpeechManager.turnOnCutscene = true;

                CutsceneCanvas2.SetActive(true);
            }
            
        }
    }
}
