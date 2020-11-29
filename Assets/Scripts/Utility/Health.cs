using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamageTaken;

    [SerializeField] private int _health = 1;
    public int health { get { return _health; } }

    private int maxHealth = 1;

    private void Awake()
    {
        maxHealth = _health;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        OnDamageTaken?.Invoke();

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _health += amount;
        _health = _health > maxHealth ? maxHealth : _health; 
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
