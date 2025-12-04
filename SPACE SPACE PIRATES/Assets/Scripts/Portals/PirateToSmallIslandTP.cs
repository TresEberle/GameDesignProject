using UnityEngine;

public class PirateToSmallIslandTP : MonoBehaviour
{



    public bool isOnPiarateShip;
    public float distance = 0.2f;

    public To_Teleport tp { get; private set; }

    private void Awake()
    {
        tp = new To_Teleport(isOnPiarateShip, distance);

    }

    void Start()
    {
        tp.toTeleport("pirateToSmallIsland", "SmallIslandToPirate"); //this could be done as a get tag and we can write less code
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            tp.teleportTransition();
            EnemySpawner._instance.startSpawningEnemiesForSequence(GameState.Sequence05, 2);
            other.transform.position = new Vector2(tp.destination.position.x, tp.destination.position.y);
        }
    }
}


