using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;

    private bool _isDead;

    public bool IsDead { get => _isDead; }

    public event Action OnDamageReceived;
    public event Action OnDeath;

    public void GetDamage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health > 0)
        {
            OnDamageReceived?.Invoke();
        }
        else
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}
