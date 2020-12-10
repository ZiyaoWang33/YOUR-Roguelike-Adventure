using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public event Action OnDeath;
    [HideInInspector] public Health player = null;
    public Health health = null;

    [SerializeField] protected EnemyStats stats = null;
    [SerializeField] protected SpriteRenderer sprite = null;
    [SerializeField] protected int difficultyMultiplier = 1;
    [SerializeField] protected int speedMultiplier = 1;

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
        Move();
    }

    protected virtual void Move()
    {
        if (player)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector3.zero;
        }

        sprite.flipX = direction.x > 0;
        transform.position += direction * stats.speed * speedMultiplier * Time.deltaTime;
    }

    protected virtual void Attack()
    {
        attackTimer = stats.attackSpeed;
    }

    public virtual void AnimateSpawn()
    {
        
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

    protected virtual void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
