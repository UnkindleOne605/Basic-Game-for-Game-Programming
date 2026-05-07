using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hurtbox : MonoBehaviour
{
    public EnemyStats enemy;
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
            Debug.Log("Enemy Defeated");
            player.GainGold(enemy.goldDropped);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            if (timer > enemy.invincibilityDuration)
            {
                enemy.TakeDamage(CombatCalculation.CalculateDamage(player, enemy));
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
        if (collision.CompareTag("Player"))
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
        enemy = transform.GetComponentInParent<EnemyStats>();
        body = transform.GetComponentInParent<Rigidbody2D>();
        enemy.currentHealth = enemy.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
}
