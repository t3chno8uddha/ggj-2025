using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;
    [SerializeField] private bool _isDead;
    [SerializeField] private float _targetSize = 1;
    public bool IsDead { get => _isDead; }
    public float TargetSize { get => _targetSize; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHealth { get => _health; set => _health = value; }

    private int _currentHealth;

    public event Action OnDamageReceived;
    public event Action OnDeath;
    public event Action OnReset;

    private void Awake()
    {
        _currentHealth = _health;
    }

    public void GetDamage(int damageAmount)
    {
        if (_isDead) return;

        _currentHealth -= damageAmount;

        if (_currentHealth > 0)
        {
            OnDamageReceived?.Invoke();
        }
        else
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    public void ResetHp()
    {
        _currentHealth = _health;

        _isDead = false;

        OnReset?.Invoke();
    }
}
