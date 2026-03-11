using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();

    public virtual void Update(PlayerStats stats, int amount)
    {
        
    }
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
    }
}