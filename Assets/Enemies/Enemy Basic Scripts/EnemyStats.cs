using UnityEngine;

public class EnemyStats : MonoBehaviour, ICombatStats
{
    public float maxHealth;
    public float currentHealth;
    public float baseDamage;
    public float Damage { get; set; }
    public float attackSpeed;
    public float moveSpeed;
    public float healthRegen;
    public float armor;
    public float Armor { get; set; }
    public float attackRange;
    public float invincibilityDuration;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
    }

    void Update ()
    {
        Damage = baseDamage;
        Armor = armor;
    }
}
