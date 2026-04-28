using UnityEngine;

public class StatueBossHurtbox : MonoBehaviour
{
    public StatueBossStats enemy;
    public PlayerStats player;
    public Rigidbody2D body;
    public float timer;

    private bool isDead = false;

    void Start()
    {
        enemy = transform.GetComponentInParent<StatueBossStats>();
        body = transform.GetComponentInParent<Rigidbody2D>();
        enemy.currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Debug.Log(hp.currentHealth);
        if (enemy.currentHealth <= 0 && !isDead)
        {   
            isDead = true;
            Destroy(body.gameObject);
            Debug.Log("Boss Defeated");

            //Run Victory or Level Complete Code
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (timer > enemy.invincibilityDuration)
            {
                //Debug.Log($"Player null? {player == null}");
                //Debug.Log($"Enemy null? {enemy == null}");
                enemy.TakeDamage(CombatCalculation.CalculateDamage(player, enemy));
                Debug.Log("Hit");
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
    }
}
