using UnityEngine;

public class freezerToPirateShip : MonoBehaviour
{

    public bool isFreezer;
    public float distance = 0.2f;


    public To_Teleport tp { get; private set; }

    private void Awake()
    {
        tp = new To_Teleport(isFreezer, distance);
        

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
            



        }
    }
}
