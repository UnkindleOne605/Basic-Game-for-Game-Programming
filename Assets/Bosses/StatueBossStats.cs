using UnityEngine;

public class StatueBossStats : MonoBehaviour, ICombatStats
{
    public float maxHealth;
    public float currentHealth;
    public float attackDamage;
    public float Damage { get; set; }
    public float attackSpeed;
    public float moveSpeed;
    public float tempMoveSpeed;
    public float healthRegen;
    public float armor;
    public float Armor { get; set; }
    public float attackRange;
    public float invincibilityDuration;
    public float combatDistance;

    //Hand Specific Stats
    public float followDistance;
    public float initialMoveSpeed;
    public float returnMoveSpeed;
    public float tolerance;
    public float offset;

    //Right Hand Specific
    public float slamHeight;
    public float slamDown;

    //Left hand Specific
    void Start()
    {
        Damage = attackDamage;
        Armor = armor;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
    }
}
