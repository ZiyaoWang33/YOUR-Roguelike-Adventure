using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamageTaken;

    [SerializeField] private int _health = 1;
    public int health { get { return _health; } }
    [SerializeField] private int _maxHealth = 1; // Serializable for convenience, should always be set to the same value as _health in the inspector.
    public int maxHealth { get { return _maxHealth; } }

    public float damageMultiplier = 1;
    public float defense = 0;

    public void TakeDamage(int amount)
    {
        _health -= (int)Math.Round((amount * damageMultiplier) - defense, MidpointRounding.AwayFromZero);
        OnDamageTaken?.Invoke();      

        if (_health <= 0)
        {
            Die();
        }

        StartCoroutine(damageBlink(0.05f));
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

    IEnumerator damageBlink(float time)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
