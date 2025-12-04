using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class spawnEnemyAtLocation : MonoBehaviour
{
    [Header("Enemy To Spawn")]
    public EnemyHealth enemyPrefab;  // drag  enemy prefab here

    [Header("Where To Spawn")]
    public Transform spawnPoint;     // optional

    [Header("Behavior")]
    public bool spawnOnce = true;    // only spawn the first time player enters

    private bool hasSpawned = false;

    void Awake()
    {
        var col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (spawnOnce && hasSpawned) return;
        if (enemyPrefab == null)
        {
            Debug.LogWarning("spawnEnemyAtLocation: enemyPrefab is not assigned!");
            return;
        }

        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(enemyPrefab, pos, Quaternion.identity);

        hasSpawned = true;
    }
}
