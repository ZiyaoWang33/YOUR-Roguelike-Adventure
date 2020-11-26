﻿using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [HideInInspector] public float speedMultiplier = 1;

    [SerializeField] protected PlayerStats stats = null;
    [SerializeField] protected PlayerInput input = null;
    [SerializeField] protected Transform rotator = null;
    [SerializeField] protected SpriteRenderer character = null;

    protected float attackTimer = 0;

    protected virtual void Update()
    {
        if (input.attack)
        {
            Attack();
        }

        attackTimer -= Time.deltaTime;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += (Vector3)input.movement * stats.speed * speedMultiplier * Time.deltaTime;

        Vector2 direction = (input.mousePos - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        character.flipX = angle > 90 || angle < -90;
    }

    protected abstract void Attack();

    public Vector3 GetDirection()
    {
        return (Vector3)input.movement;
    }
}
