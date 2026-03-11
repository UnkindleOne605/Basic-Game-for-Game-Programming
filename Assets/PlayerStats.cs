using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    //Health related player stats
    public float basemaxHealth = 100f;
    public float modifiedMaxHealth;
    public float currentHealth;

    //Combat related player stats
    public float baseAttackDamage = 10f;
    public float modifiedAttackDamage;
    public float baseFireRate = 1f;
    public float modifiedFireRate;
    public float baseAttackRange = 5f;
    public float modifiedAttackRange;
    public float baseArmor = 0f;
    public float modifiedArmor;

    //Movement related player stats
    public float baseMoveSpeed = 5f;
    public float modifiedMoveSpeed;
    
    //Misc player stats
    public float invincibilityDuration =0.5f;
    public float baseHealthRegen = 1f;
    public float modifiedHealthRegen;

    public List<ItemList> items = new List<ItemList>();

    void Start()
    {
        RegenItem item = new RegenItem();
        items.Add(new ItemList(item, item.GiveName(), 1));
        Stake item2 = new Stake();
        items.Add(new ItemList(item2, item2.GiveName(), 1));

        StartCoroutine(ItemUpdate());

        //Stats initialization to modified stats
        modifiedMaxHealth = basemaxHealth;
        currentHealth = modifiedMaxHealth;

        modifiedAttackDamage = baseAttackDamage;
        modifiedFireRate = baseFireRate;
        modifiedAttackRange = baseAttackRange;
        modifiedArmor = baseArmor;

        modifiedMoveSpeed = baseMoveSpeed;
        modifiedHealthRegen = baseHealthRegen;
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
