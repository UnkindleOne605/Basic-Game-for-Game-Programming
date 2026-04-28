using UnityEngine;

public interface IDamageable<T>
{
    void Damaage(T damageTaken);
}

public interface IHealable
{
    
}

public interface ICombatStats
{
    float Damage { get; }
    float Armor { get; }
}

public interface IKillable
{
    void kill();
}

