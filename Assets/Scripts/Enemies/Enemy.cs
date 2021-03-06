﻿using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public event Action<float> OnMove;
    public event Action OnAttack;

    [HideInInspector] public Health player = null;
    public Health health = null;
    public EnemyStats stats = null;

    [SerializeField] protected int difficultyMultiplier = 1;

    protected Vector3 direction = Vector3.right;
    protected float attackTimer = 0;
    protected bool inRange = false;

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0 && inRange)
        {
            Attack();
        }
    }

    protected virtual void FixedUpdate()
    {
        ChangeDirection();
        Move();
    }

    protected virtual void ChangeDirection()
    {
        if (player)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector3.zero;
        }

        OnMove?.Invoke(direction.x);
    }

    protected virtual void Move()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * stats.speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    protected virtual void Attack()
    {
        OnAttack?.Invoke();
        attackTimer = stats.attackSpeed;
    }

    // Default method for melee enemies; override completely for different cases
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (player && other.gameObject.Equals(player.gameObject))
        {
            inRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (player && other.gameObject.Equals(player.gameObject))
        {
            inRange = false;
        }
    }
}
