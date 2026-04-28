using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour, ICombatStats
{
    //Health related player stats
    public float baseMaxHealth;
    public float modifiedMaxHealth;
    public float currentHealth;

    //Combat related player stats
    public float baseAttackDamage;
    public float modifiedAttackDamage;
    public float Damage { get; set; }
    public float baseFireRate;
    public float modifiedFireRate;
    public float baseAttackRange;
    public float modifiedAttackRange;
    public float baseArmor;
    public float modifiedArmor;
    public float Armor { get; set; }

    //Movement related player stats
    public float baseMoveSpeed;
    public float modifiedMoveSpeed;
    
    //Misc player stats
    public float invincibilityDuration;
    public float baseHealthRegen;
    public float modifiedHealthRegen;
    public float dodgeChance;
    public float modifieddodgeChance;
    public List<ItemList> items = new List<ItemList>();

    void Awake()
    {
        StartCoroutine(ItemUpdate());

        //Stats initialization to modified stats
        attackdamageCalculation();
        armorCalculation();
    }

    IEnumerator ItemUpdate()
    {
        while (true) {
            foreach (ItemList i in items)
            {
                i.item.Update(this, i.amount);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private void FixedUpdate()
    {
        maxHealthCalculation();
        healthRegenCalculation();
        attackdamageCalculation();
        speedCalculation();
        armorCalculation();
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
        //Debug.Log("base attack damage: " + baseAttackDamage);
        modifiedAttackDamage = baseAttackDamage;
        foreach (ItemList i in items)
        {
            if (i.name == "Stake")
            {
                modifiedAttackDamage += 5 + ((i.amount - 1) * 2);
                //Debug.Log("Modified Attack Damage after " + i.amount + " " + i.name + ": " + modifiedAttackDamage);
            }
        }
        Damage = modifiedAttackDamage;
        //Debug.Log("Attack Damage: " + Damage);
    }

    public void speedCalculation()
    {
        modifiedMoveSpeed = baseMoveSpeed;
        foreach (ItemList i in items)
        {
            if (i.name == "Boots")
            {
                modifiedMoveSpeed += 5 * i.amount;
            }
        }
    }

    public void armorCalculation()
    {
        modifiedArmor = baseArmor;
        foreach (ItemList i in items)
        {
            if (i.name == "Scale Mail Armor")
            {
                modifiedArmor += 5 * i.amount;
            }
        }
        Armor = modifiedArmor;
        Debug.Log("Armor: " + Armor);
    }

    public void dodgeChanceCalculation()
    {
        modifieddodgeChance = dodgeChance;
        foreach (ItemList i in items)
        {
            if (i.name == "Cloak")
            {
                modifieddodgeChance += 5 * i.amount;
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
