using Unity.VisualScripting;
using UnityEngine;

public class Boss1Hurtbox : MonoBehaviour
{
    public StatueBossStats boss;
    public PlayerStats player;
    public Rigidbody2D body;
    public GameObject loot;
    public float timer;

    private bool isDead = false;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Debug.Log(hp.currentHealth);
        if (boss.currentHealth <= 0 && !isDead)
        {   
            Instantiate(loot, transform.position, Quaternion.identity);
            isDead = true;
            Destroy(body.gameObject);
            Debug.Log("Enemy Defeated"); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (timer > boss.invincibilityDuration)
            {
                boss.TakeDamage(CombatCalculation.CalculateDamage(player, boss));
                Debug.Log("Hit");
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
    }

    void Initialize()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }

        boss = transform.GetComponentInParent<StatueBossStats>();
        body = transform.GetComponentInParent<Rigidbody2D>();
        boss.currentHealth = boss.maxHealth;
    }
}
