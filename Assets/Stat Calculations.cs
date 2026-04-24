using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class StatCalculations : MonoBehaviour
{
    public PlayerStats stats;
    public float healAmount = 0f;
    public List<ItemList> items = new List<ItemList>();

    private void FixedUpdate()
    {
        
    }

    void Awake()
    {
        StartCoroutine(ItemUpdate());
    }

    IEnumerator ItemUpdate()
    {
        while (true) {
            foreach (ItemList i in items)
            {
                i.item.Update(stats, i.amount);
            }
            yield return new WaitForSeconds(1f);
        }
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
        //should grab damage calculation from combatCalculations and apply it here, for now just a placeholder

        stats.currentHealth -= damage;
        stats.currentHealth = Mathf.Max(stats.currentHealth, 0);

        foreach (ItemList i in items)
        {
            if (i.name == "Shield of Redemption")
            {
                healAmount += 5f;
                
            }
        }
        
        Heal();
    }

    public void Heal()
    {
        stats.currentHealth += healAmount;
        stats.currentHealth = Mathf.Min(stats.currentHealth, stats.modifiedMaxHealth);
        healAmount = 0f;
    }
}

