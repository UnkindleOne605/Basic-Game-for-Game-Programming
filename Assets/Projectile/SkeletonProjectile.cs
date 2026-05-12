using UnityEngine;

public class SkeletonProjectile : MonoBehaviour
{
    public EnemyStats skeleton;

    public ProjectileStats projectile;
    public PlayerStats player;
    private UnityEngine.Vector3 direction;

    private UnityEngine.Vector3 startPosition;
    private float timer = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    public void Setup(UnityEngine.Vector3 direction)
    {
        //Debug.Log("Setting direction: " + direction);
        this.direction = direction;
        startPosition = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        transform.position += direction * projectile.speed * Time.deltaTime;

        if (UnityEngine.Vector3.Distance(startPosition, transform.position) >= skeleton.attackRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(CombatCalculation.CalculateDamage(skeleton, player));
        }
    }
}
