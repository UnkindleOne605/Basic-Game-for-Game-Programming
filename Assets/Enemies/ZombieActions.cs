using Unity.VisualScripting;
using UnityEngine;

public class ZombieActions : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public EnemyStats zombie;
    public PlayerStats player;
    private float timer;

    private UnityEngine.Vector2 target;
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UnityEngine.Vector3 targetPosition = player.transform.position;
        ChaseTarget(targetPosition);
    }

    public void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, zombie.moveSpeed * Time.deltaTime);
    }

    void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        zombie = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
}
