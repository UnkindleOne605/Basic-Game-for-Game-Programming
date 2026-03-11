using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();

    public virtual void Update(PlayerStats stats, int amount)
    {
        
    }

    //public virtual void onPickup(PlayerStats stats, int amount)
    //{

    //}
}

public class RegenItem : Item
{
    public override string GiveName()
    {
        return "Regen Item";
    }

    public override void Update(PlayerStats stats, int amount)
    {
        stats.currentHealth += 5;
        Debug.Log("Regen applied: " + stats.currentHealth);
    }
}

public class Stake : Item
{
    public override string GiveName()
    {
        return "Stake";
    }

    public override void Update(PlayerStats stats, int amount)
    {
        stats.modifiedAttackDamage += 5 + ((amount - 1) * 2);
        Debug.Log("Stake applied: " + stats.modifiedAttackDamage);
    }
}