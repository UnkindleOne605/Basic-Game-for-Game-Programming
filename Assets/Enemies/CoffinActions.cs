using System;
using UnityEngine;

public class CoffinActions : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public EnemyStats stats;
    public GameObject zombiePrefab;
    public float spawnTime = 5f;
    public float spawnRange = 2f;
    private float timer;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            SpawnZombie();
            timer = 0f;
        }
    }

    public void SpawnZombie()
    {
        Instantiate(zombiePrefab, transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle * spawnRange), Quaternion.identity);
    }

    void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyStats>();
    }
}
