using UnityEngine;

public class StairCaseTp : MonoBehaviour
{

  
    public bool isOutsideStairs;
    public float distance = 0.2f;

    public To_Teleport tp { get; private set; }

    private void Awake()
    {
        tp = new To_Teleport(isOutsideStairs, distance);

    }

    void Start()
    {
        tp.toTeleport("OutsidePirateShip", "InsidePirateShip");
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            tp.teleportTransition();
            GameManager.instance.CurrentQuest.SetText("Talk To The Pirate Captain");
            other.transform.position = new Vector2(tp.destination.position.x, tp.destination.position.y);
        }
    }


}
