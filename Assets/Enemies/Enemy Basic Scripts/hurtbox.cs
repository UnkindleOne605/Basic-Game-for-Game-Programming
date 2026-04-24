using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hurtbox : MonoBehaviour
{
    public EnemyStats hp;
    public PlayerStats player;
    public Rigidbody2D body;
    public float timer;

    private bool isDead = false;

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

        //Debug.Log(hp.currentHealth);
        if (hp.currentHealth <= 0 && !isDead)
        {   
            isDead = true;
            Destroy(body.gameObject);
            Debug.Log("Enemy Defeated");
            
            
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
