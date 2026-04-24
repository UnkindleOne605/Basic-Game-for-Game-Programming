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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UnityEngine.Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        ChaseTarget(targetPosition);

        if (timer > spawnTime)
        {
            SpawnZombie();
            timer = 0f;
        }
    }

    public void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        //Debug.Log("Chasing player...Location is " + targetPosition);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
    }

    public void SpawnZombie()
    {
        Instantiate(zombiePrefab, transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle * spawnRange), Quaternion.identity);
    }
}
