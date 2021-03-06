﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb = null;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected int damage = 1;

    public delegate void OnHitEffect(Collision2D collision);
    public static OnHitEffect OnHit = null;

    protected virtual void Awake()
    {
        rb.velocity = transform.right * speed;
    }

    public void SetDamage(float damageMultiplier)
    {
        damage *= (int)damageMultiplier;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            OnHit?.Invoke(collision);
        }

        Destroy(gameObject);
    }
}
