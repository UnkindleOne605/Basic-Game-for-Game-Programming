using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float attackDamage;
    public float attackCooldown;
    public float moveSpeed;
    public float healthRegen;
    public float armor;
    public float attackRange;
    public float invincibilityDuration;

    public List<ItemList> items = new List<ItemList>();

    void Start()
    {
        RegenItem item = new RegenItem();
        items.Add(new ItemList(item, item.GiveName(), 1));

        StartCoroutine(ItemUpdate());
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
    }

    IEnumerator ItemUpdate()
    {
        foreach (ItemList i in items)
        {
            i.item.Update(this, i.amount);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ItemUpdate());
    }
}
