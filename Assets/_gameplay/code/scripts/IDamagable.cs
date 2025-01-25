using System;

public interface IDamagable
{
    bool IsDead { get; }
    event Action OnDamageReceived;
    event Action OnDeath;
    event Action OnReset;
    void GetDamage(int damageAmount);
}
