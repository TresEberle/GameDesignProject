using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]

    public EnemyHealth enemyPrefab;
    public Transform[] spawnPoints;


    [Header("Rounds")]
    public int currentRound = 1;
    public int baseEnemies = 5;
    public float roundMultiplier = 1.5f; // enemies grow each round
    public float spawnInterval = 3f;
    public float intermmissionTime = 5f;

    [Header("UI")]
    public TMP_Text roundText;
    public TMP_Text countdownText;

    [Header("Sound")]
    public AudioClip roundStartSFX;
    [Range(0f, 1f)] public float soundVolume = 0.5f;

    private int enemiesToSpawn;
    private int enemiesAlive;
    private bool isSpawning;
    private bool halt;
    private static EnemySpawner _instance;
    void Awake() { _instance = this; }

    void Start()
    {
        StartCoroutine(StartNextRound());
    }

    public static void NotifyEnemyKilled()
    {
        if (_instance == null) return;
        _instance.OnEnemyKilled();
    }

    void OnEnemyKilled()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        if (enemiesAlive == 0 && !isSpawning && !halt)
        {
            currentRound++;
        }
    }

     IEnumerator StartNextRound()
    {
        if (halt) yield break;

        if (roundStartSFX) AudioSource.PlayClipAtPoint(roundStartSFX, transform.position, soundVolume);

        
        enemiesToSpawn = Mathf.Max(1, Mathf.RoundToInt(baseEnemies * Mathf.Pow(roundMultiplier, currentRound - 1)));
        enemiesAlive = enemiesToSpawn;
        if (currentRound > 1 && intermmissionTime > 0f)
        {
            float t = intermmissionTime;
            while (t > 0f && !halt)
            {
                if (countdownText) countdownText.text = $"Next round in {Mathf.CeilToInt(t)}...";
                yield return new WaitForSeconds(1f);
                t -= 1f;
            }
        }

        if (countdownText) countdownText.text = "";
        yield return StartCoroutine(SpawnRoutine());
        Debug.Log($"[Spawner] Round {currentRound} → Spawning {enemiesToSpawn}");
        
    }

    IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }


    void SpawnOne()
    {
        if (!enemyPrefab || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("[Spawner] Not configured: set prefab and spawn points.");
            return;
        }

        var p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, p.position, Quaternion.identity);
    }

    public void HaltAll()
    {
        halt = true;
        StopAllCoroutines();
        if (countdownText) countdownText.text = "";
    }
}
