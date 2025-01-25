using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;
    [SerializeField] private bool _isDead;
    [SerializeField] private float _targetSize = 1;
    public bool IsDead { get => _isDead; }
    public float TargetSize { get => _targetSize; }

    public event Action OnDamageReceived;
    public event Action OnDeath;

    public void GetDamage(int damageAmount)
    {
        if (_isDead) return;

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
