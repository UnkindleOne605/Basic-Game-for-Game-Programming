using UnityEngine;

public class ZombieActions : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public EnemyStats stats;

    private UnityEngine.Vector2 target;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (stats.attackRange > Vector2.Distance(transform.position, targetPosition))
        {
            AttackTarget();
        }
        else
        {
            ChaseTarget(targetPosition);
        }
    }

    public void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        //Debug.Log("Chasing player...Location is " + targetPosition);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
    }

    public void AttackTarget()
    {
        //Debug.Log("Biting player...");
    }
}
