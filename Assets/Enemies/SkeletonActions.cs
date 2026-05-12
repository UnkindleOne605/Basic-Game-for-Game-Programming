using Unity.VisualScripting;
using UnityEngine;

public class SkeletonActions : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public EnemyStats stats;
    private float timer;
    private float tempSpeed;
    [SerializeField] private Transform SkeletonProjectile;

    private UnityEngine.Vector2 target;
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {   
        timer -= Time.deltaTime;

        UnityEngine.Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        if (stats.attackRange > Vector2.Distance(transform.position, targetPosition) && timer <= 0)
        {
            AttackTarget();
            timer = stats.attackSpeed;
        }
        else
        {
            stats.moveSpeed = tempSpeed;
            ChaseTarget(targetPosition);
        }
    }

    public void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
    }

    public void AttackTarget()
    {
        stats.moveSpeed = 0;
        UnityEngine.Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        direction.z = 0;
        direction.Normalize();
        Transform projectileTransform = Instantiate(SkeletonProjectile, transform.position, UnityEngine.Quaternion.identity);
        projectileTransform.GetComponent<SkeletonProjectile>().Setup(direction);
    }

    void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyStats>();
        timer = stats.attackSpeed;
        tempSpeed = stats.moveSpeed;
    }
}
