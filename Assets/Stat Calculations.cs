using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class StatCalculations : MonoBehaviour
{
    public PlayerStats stats;

    private void FixedUpdate()
    {
        
    }

    public void regenHealth()
    {
        stats.currentHealth += stats.modifiedHealthRegen * Time.fixedDeltaTime;
        if (stats.currentHealth > stats.modifiedMaxHealth)
        {
            stats.currentHealth = stats.modifiedMaxHealth;
            Debug.Log("Health healed by: " + stats.modifiedHealthRegen * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        stats.currentHealth -= damage;
        stats.currentHealth = Mathf.Max(stats.currentHealth, 0);
    }
}

