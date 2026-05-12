using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

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
            player.GainGold(enemy.goldDropped);
            Destroy(enemy.gameObject);
            //Debug.Log("Enemy Defeated");
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision Detected: " + collision.gameObject.name + " with tag: " + collision.gameObject.tag);
        if (collision.CompareTag("PlayerProjectile"))
        {
            enemy.TakeDamage(CombatCalculation.CalculateDamage(player, enemy));
            //Debug.Log("Enemy Took Damage: " + CombatCalculation.CalculateDamage(player, enemy) + "from object: " + collision.gameObject.name);
            //Debug.Log("Enemy Hurt: " + enemy.currentHealth);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            player.TakeDamage(CombatCalculation.CalculateDamage(enemy, player));
            //Debug.Log("Enemy Health: " + enemy.currentHealth);
        }
        else if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("Collision with another enemy: " + collision.gameObject.name);
            Vector2 knockbackDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; 
            body.AddForce(knockbackDirection * enemy.moveSpeed, ForceMode2D.Impulse);
        }
        else
        {
            //Debug.Log("Collision with non-projectile, non-playerProjectile object: " + collision.gameObject.name);
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
