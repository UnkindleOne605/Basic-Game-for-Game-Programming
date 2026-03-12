using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public static float CalculateDamage(PlayerStats attacker, EnemyStats defender)
    {
        return CalculateDamage(attacker.modifiedAttackDamage, defender.armor);
    }

    public static float CalculateDamage(float attackPower, float defense)
    {
        float damage = attackPower - defense;
        return Mathf.Max(damage, 1f); 
    }
}
