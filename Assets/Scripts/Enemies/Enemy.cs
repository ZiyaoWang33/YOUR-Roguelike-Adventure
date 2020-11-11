using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public event Action OnDeath;
    public Health health = null;

    [SerializeField] protected EnemyStats stats = null;
    [SerializeField] protected SpriteRenderer sprite = null;
    [SerializeField] protected int difficultyMultiplier = 1;

    protected RaycastHit2D patrolHit = new RaycastHit2D();
    protected Vector3 direction = Vector3.right;
    protected Health player = null;
    protected float attackTimer = 0;

    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Physics2D.BoxCast(transform.position, stats.detectionRange, 0, Vector2.zero, stats.detectionRange.x, stats.targetLayer) && !player)
        {
            patrolHit = Physics2D.BoxCast(transform.position, stats.detectionRange, 0, Vector2.zero, stats.detectionRange.x, stats.targetLayer);
            player = patrolHit.collider.TryGetComponent(out Health health) ? health : player;
        }

        Move();
    }

    protected virtual void Move()
    {
        if (player)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
 
        sprite.flipX = direction.x > 0;
        transform.position += direction * stats.speed * Time.deltaTime;
    }

    protected virtual void Attack()
    {
        player.TakeDamage(stats.damage * difficultyMultiplier);
        attackTimer = stats.attackSpeed;
    }
    

    // Default method for melee enemies; override completely for different cases
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (player)
        {
            if (collider.gameObject.Equals(player.gameObject) && attackTimer <= 0)
            {
                Attack();
            }
        }
    }

    protected virtual void OnCollisionEnter2D()
    {
        if (player == null)
        {
            direction.x = -direction.x;
        }
    }

    protected virtual void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
