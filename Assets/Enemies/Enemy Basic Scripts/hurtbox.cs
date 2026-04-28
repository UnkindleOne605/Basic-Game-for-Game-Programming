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

    void Start()
    {
        enemy = transform.GetComponentInParent<EnemyStats>();
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
            Debug.Log("Enemy Defeated");
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ontrigger Works");
        if (collision.CompareTag("PlayerProjectile"))
        {
            //Debug.Log("Collision PlayerProjectile Works");
            if (timer > enemy.invincibilityDuration)
            {
                //Debug.Log("If statement works");
                //Debug.Log("Player Power: " + player.Damage + "Enemy Defense: " + enemy.Armor);
                Debug.Log($"Player null? {player == null}");
                Debug.Log($"Enemy null? {enemy == null}");
                enemy.TakeDamage(CombatCalculation.CalculateDamage(player, enemy));
                Debug.Log("Hit");
                Destroy(collision.gameObject);
                timer = 0f;
            }
        }
    }
}
