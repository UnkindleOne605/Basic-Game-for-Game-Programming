using UnityEngine;

public class HandStats : MonoBehaviour, ICombatStats
{
    public float maxHealth;
    public float currentHealth;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float initialMoveSpeed;
    public float returnMoveSpeed;
    public float healthRegen;
    public float armor;
    public float Armor => armor;
    public float attackRange;
    public float invincibilityDuration;
    public float followDistance;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
    }
}
