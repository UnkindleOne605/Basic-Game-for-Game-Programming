using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public static float CalculateDamage(ICombatStats attacker, ICombatStats defender)
    {
        return CalculateDamage(attacker.Damage, defender.Armor);
    }

    public static float CalculateDamage(float attackPower, float defense)
    {
        Debug.Log($"Calculating damage: Attack Power = {attackPower}, Defense = {defense}");
        float damage = attackPower - defense;
        Debug.Log($"Damage after armor: {damage}");
        return Mathf.Max(damage, 1f); 
    }
}
