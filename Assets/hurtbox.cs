using UnityEngine;

public class hurtbox : MonoBehaviour
{
    public EnemyStats hp;
    public PlayerStats player;
    public Rigidbody2D body;
    public GameObject loot;
    public float timer;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }

        hp = transform.GetComponentInParent<EnemyStats>();
        body = transform.GetComponentInParent<Rigidbody2D>();
        hp.currentHealth = hp.maxHealth;
    }

    void Update()
    {
        timer += Time.deltaTime;

        Debug.Log(hp.currentHealth);
        if (hp.currentHealth <= 0)
        {
            Destroy(body.gameObject);
            Debug.Log("Enemy Defeated");
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (timer > hp.invincibilityDuration)
            {
                hp.TakeDamage(CombatCalculation.CalculateDamage(player, hp));
                Debug.Log("Hit");
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
    }
}
