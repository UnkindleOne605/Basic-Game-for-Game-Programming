using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatueBossHurtbox : MonoBehaviour
{
    public StatueBossStats enemy;
    public PlayerStats player;
    public Rigidbody2D body;
    public float timer;

    private bool isDead = false;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (enemy.currentHealth <= 0 && !isDead)
        {   
            isDead = true;
            Destroy(body.gameObject);
            Debug.Log("Boss Defeated");

            SceneManager.LoadScene("Win Screen");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.CompareTag("PlayerProjectile"))
        {
            Debug.Log("Hit by player projectile");
            if (timer > enemy.invincibilityDuration)
            {
                enemy.TakeDamage(CombatCalculation.CalculateDamage(player, enemy));
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
        else if (collision.CompareTag("Player"))
        {
            if (timer > player.invincibilityDuration)
            {
                player.TakeDamage(CombatCalculation.CalculateDamage(enemy, player));
                timer = 0f;
            }
        }
    }

    void Initialize()
    {
        enemy = transform.GetComponentInParent<StatueBossStats>();
        body = transform.GetComponentInParent<Rigidbody2D>();
        enemy.currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
}
