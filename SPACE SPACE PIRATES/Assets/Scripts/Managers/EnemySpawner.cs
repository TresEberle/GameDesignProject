using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [Header("Location To Set Spawner")]
    public Transform[] Location;


    [Header("Spawning")]

    public EnemyHealth[] enemyPrefab;
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
    public bool isSpawning;
    private bool halt;
    public static EnemySpawner _instance { get; private set; }

    public List<EnemyHealth> enemiesSpawning = new List<EnemyHealth>();

    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        
    }


    private void Update()
    {



    }

    public void startSpawningEnemiesForSequence(GameState state, int location) {
        isSpawning = true;
        this.transform.position = Location[location].position;
        Debug.Log(Location[location].position);
        StartCoroutine(StartNextRound(state, location));

    }


    public static void NotifyEnemyKilled()
    {
        if (_instance == null) return;
        _instance.OnEnemyKilled();

    }

    void OnEnemyKilled()
    {
        if (GameManager.instance.getState() == GameState.Sequence01) {
            EnemiesAliveCount(GameState.Sequence02);
            GameManager.instance.CurrentQuest.SetText("Go Talk to the Captain");
        }


        //enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        //if (enemiesAlive == 0 && !isSpawning && !halt)
        //{

        //}
    }

     IEnumerator StartNextRound(GameState state, int location)
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
        yield return StartCoroutine(SpawnRoutine(state, location));
        Debug.Log($"[Spawner] Round {currentRound} → Spawning {enemiesToSpawn}");
        
    }



    IEnumerator SpawnRoutine(GameState state, int location)
    {
        //isSpawning = true;  // Gamemanager can decides to spawn enemies

        if (isSpawning == true) {
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                CameraShake.instance.ShakeCamera(5f, 2f); //camera shaker per spawn
                SpawnOne(location);
                yield return new WaitForSeconds(spawnInterval);
                

            }

        }
        
        isSpawning = false;

  

    }


    void SpawnOne(int location)
    {
        int i = Random.Range(0, enemyPrefab.Length);
        if (!enemyPrefab[i] || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("[Spawner] Not configured: set prefab and spawn points.");
            return;
        }
        
        var p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var x =  Instantiate(enemyPrefab[i], p.position, Quaternion.identity);

        enemiesSpawning.Add(x);
        
    }

    public void HaltAll()
    {
        halt = true;
        StopAllCoroutines();
        if (countdownText) countdownText.text = "";
    }

    public void EnemiesAliveCount(GameState state) {
        Debug.Log("dead");
        if (enemiesSpawning.Count == 0) {
            Debug.Log("NEXT STATE");
            GameManager.instance.UpdateGameState(state);
            GameManager.instance.UpdateGameState(state);
        }

    }


}
