using System;

public interface IDamagable
{
    bool IsDead { get; }
    event Action OnDamageReceived;
    event Action OnDeath;
    void GetDamage(int damageAmount);
}
