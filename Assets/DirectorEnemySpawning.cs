using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class DirectorEnemySpawning : MonoBehaviour
{
    public UnityEngine.Vector2 playerPosition;
    public UnityEngine.Vector2 spawnPosition;
    public UnityEngine.Vector2 randomDirection;

    public float distance;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnRate;
    public float spawnTimer;
    public float baseSpawnRate;
    public float timerSpawnScaling;
    public float spawnMultiplier;
    public float worldTimerEventTriggerTime;
    public bool eventTriggered = false;
    public bool bossSpawned = false;
    public float timer;
    private float worldTimer;
    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        worldTimer = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorWorldData>().worldTimer;
        timer += Time.deltaTime;

        spawnMultiplier = timer/60;
        SpawnRateFunction();

        if (timer == worldTimerEventTriggerTime)
        {
            eventTriggered = true;
        }

        if (timer >= 1/spawnRate && !eventTriggered)
        {
            SpawnEnemy();
            timer = 0f;
        }
        else if (eventTriggered && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnEnemy()
    {
        eventTriggered = worldTimer >= worldTimerEventTriggerTime;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        spawnPosition = playerPosition + (randomDirection * distance);

        Instantiate(enemyPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        timer = 0f;
    }

    void SpawnRateFunction()
    {
        spawnRate = baseSpawnRate * Mathf.Pow(timerSpawnScaling, spawnMultiplier);
    }

    void SpawnBoss()
    {
        //playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        spawnPosition = playerPosition + (randomDirection * distance);

        Instantiate(bossPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        timer = 0f;

        bossSpawned = true;
    }
}
