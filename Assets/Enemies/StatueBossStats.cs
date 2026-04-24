using UnityEngine;

public class StatueBossStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float tempMoveSpeed;
    public float healthRegen;
    public float armor;
    public float attackRange;
    public float invincibilityDuration;
    public float combatDistance;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
    }
}
