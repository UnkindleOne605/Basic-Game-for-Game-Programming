using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public static float CalculateDamage(PlayerStats attacker, ICombatStats defender)
    {
        return CalculateDamage(attacker.modifiedAttackDamage, defender.Armor);
    }

    public static float CalculateDamage(float attackPower, float defense)
    {
        Debug.Log($"Calculating damage: Attack Power = {attackPower}, Defense = {defense}");
        float damage = attackPower - defense;
        return Mathf.Max(damage, 1f); 
    }
}
