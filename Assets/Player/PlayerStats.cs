using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public float modifiedInvincibilityDuration;
    public float baseHealthRegen;
    public float modifiedHealthRegen;
    public float dodgeChance;
    public float modifiedDodgeChance;

    public float goldAmount;

    private float timer;
    public float healFrequency = 1f;
    public List<ItemList> items = new List<ItemList>();
    [SerializeField] private HealthBarUI HealthBarUI;

    void Awake()
    {
        StartCoroutine(ItemUpdate());

        //Stats initialization to modified stats
        attackdamageCalculation();
        armorCalculation();
        
        currentHealth = baseMaxHealth;
        goldAmount = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (currentHealth <= 0)
        {
            Die();
        }

        if (timer >= healFrequency)
        {
            Heal();
            timer = 0f;
        }

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
            if (i.name == "Candle Flamer")
            {
                 modifiedMaxHealth += 5 * i.amount;
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
            if (i.name == "Mace of Redemption")
            {
                modifiedHealthRegen += 0.5f * i.amount;
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
                modifiedAttackDamage += 1 + ((i.amount - 1) * 1);
                //Debug.Log("Modified Attack Damage after " + i.amount + " " + i.name + ": " + modifiedAttackDamage);
            }
            if (i.name == "Silver Bullets")
            {
                modifiedAttackDamage += 5 + ((i.amount - 1) * 3);
                //Debug.Log("Modified Attack Damage after " + i.amount + " " + i.name + ": " + modifiedAttackDamage);
            }
            if (i.name == "Mace of Redemption")
            {
                modifiedAttackDamage += 10 * i.amount;
                //Debug.Log("Modified Attack Damage after " + i.amount + " " + i.name + ": " + modifiedAttackDamage);
            }
            if (i.name == "Candle Flamer")
            {
                modifiedAttackDamage += 1 * i.amount;
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
            if (i.name == "Candle Flamer")
            {
                modifiedMoveSpeed += 2 * i.amount;
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
        //Debug.Log("Armor: " + Armor);
    }

    public void dodgeChanceCalculation()
    {
        modifiedDodgeChance = dodgeChance;
    }

    public void rangeCalculation()
    {
        modifiedAttackRange = baseAttackRange;
        foreach (ItemList i in items)
        {
            if (i.name == "Iron Sights")
            {
                modifiedAttackRange += 5 * i.amount;
            }
        }
    }

    public void invincibilityCalculation()
    {
        modifiedInvincibilityDuration = invincibilityDuration;
        foreach (ItemList i in items)
        {
            if (i.name == "Shield of Redemption")
            {
                modifiedInvincibilityDuration += 0.5f * i.amount;
            }
        }
    }

    public void fireRateCalculation()
    {
        modifiedFireRate = baseFireRate;
        foreach (ItemList i in items)
        {
            if (i.name == "Candle Flamer")
            {
                modifiedFireRate += 0.5f * i.amount;
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        HealthBarUI.UpdateBar(currentHealth, modifiedMaxHealth);
    }

    public void GainGold(float gainedGold)
    {
        goldAmount += gainedGold;
        foreach (ItemList i in items)
        {
            if (i.name == "Gold Purse")
            {
                goldAmount += i.amount;
            }
        }
    }

    public void LoseGold(float lostGold)
    {
        goldAmount -= lostGold;
    }

    public void Die()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void Heal()
    {
        currentHealth += modifiedHealthRegen;;
        currentHealth = Mathf.Min(currentHealth, modifiedMaxHealth);
    }
}


