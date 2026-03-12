using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    //Health related player stats
    public float baseMaxHealth = 100f;
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
        //RegenItem item = new RegenItem();
        //items.Add(new ItemList(item, item.GiveName(), 1));
        //Stake item2 = new Stake();
        //items.Add(new ItemList(item2, item2.GiveName(), 1));

        StartCoroutine(ItemUpdate());

        //Stats initialization to modified stats
        modifiedMaxHealth = baseMaxHealth;
        currentHealth = modifiedMaxHealth;

        modifiedAttackDamage = baseAttackDamage;
        modifiedFireRate = baseFireRate;
        modifiedAttackRange = baseAttackRange;
        modifiedArmor = baseArmor;

        modifiedMoveSpeed = baseMoveSpeed;
        modifiedHealthRegen = baseHealthRegen;
    }

    //public void CallItemOnPickup()
    //{
    //    foreach (ItemList i in items)
    //    {
    //        i.item.onPickup(this, i.amount);
    //    }
    //}

    IEnumerator ItemUpdate()
    {
        foreach (ItemList i in items)
        {
            i.item.Update(this, i.amount);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ItemUpdate());
    }

    private void FixedUpdate()
    {
        maxHealthCalculation();
        healthRegenCalculation();
        attackdamageCalculation();
    }

    public void maxHealthCalculation()
    {
        modifiedMaxHealth = baseMaxHealth;
        foreach (ItemList i in items)
        {
            if (i.name == "Blood Vial")
            {
                modifiedMaxHealth += 10 * i.amount;
            }
        }
    }

    public void healthRegenCalculation()
    {
        modifiedHealthRegen = baseHealthRegen;
        foreach (ItemList i in items)
        {
            if (i.name == "Regen Item")
            {
                modifiedHealthRegen += 1 * i.amount;
            }
        }
    }

    public void attackdamageCalculation()
    {
        modifiedAttackDamage = baseAttackDamage;
        foreach (ItemList i in items)
        {
            if (i.name == "Stake")
            {
                modifiedAttackDamage += 5 + ((i.amount - 1) * 2);
            }
        }
    }

    public void CallItemOnPickup()
    {
        foreach (ItemList i in items)
        {
            i.item.onPickup(this, i.amount);
        }
    }
}
