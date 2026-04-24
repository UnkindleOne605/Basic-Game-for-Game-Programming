using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();
    public int dropChance;
    

    public virtual void Update(PlayerStats stats, int amount)
    {
        
    }

    public virtual void onPickup(PlayerStats stats, int amount)
    {

    }
}

public class RegenItem : Item
{
    public override string GiveName()
    {
        return "Regen Item";
    }

    //public override void Update(PlayerStats stats, int amount)
    //{
    //    stats.currentHealth += 5;
    //    Debug.Log("Regen applied: " + stats.currentHealth);
    //}
}

public class Stake : Item
{
    public override string GiveName()
    {
        return "Stake";
    }

    //public override void Update(PlayerStats stats, int amount)
    //{
    //    stats.modifiedAttackDamage += 5 + ((amount - 1) * 2);
    //    Debug.Log("Stake applied: " + stats.modifiedAttackDamage);
    //}
}

public class BloodVial : Item
{
    public override string GiveName()
    {
        return "Blood Vial";
    }

}

public class Boots : Item
{
    public override string GiveName()
    {
        return "Boots";
    }
}

public class ScaleMailArmor : Item
{
    public override string GiveName()
    {
        return "Scale Mail Armor";
    }
}

public class MaceofRedemption : Item
{
    public override string GiveName()
    {
        return "Mace of Redemption";
    }
}

public class ShieldofRedemption : Item
{
    public override string GiveName()
    {
        return "Shield of Redemption";
    }
}

public class Cloak : Item
{
    public override string GiveName()
    {
        return "Cloak";
    }
}

public class CandleFlamer : Item
{
    public override string GiveName()
    {
        return "Candle Flamer";
    }
}