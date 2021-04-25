using UnityEngine;
using System;

public abstract class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerEnter;
    public event Action<float> OnAim;
    public event Action OnPlayerExit, OnAttack;

    [HideInInspector] public float baseSpeedMultiplier = 1;
    [HideInInspector] public float speedMultiplier = 1;
    [HideInInspector] public float damageMultiplier = 1;

    [SerializeField] protected PlayerStats stats = null;
    [SerializeField] protected PlayerInput input = null;
    [SerializeField] protected Rigidbody2D rb = null;
    [SerializeField] protected Transform rotator = null;

    protected float attackTimer = 0;

    private const string collisionTag = "Solid Terrain";

    protected virtual void Awake()
    {
        OnPlayerEnter?.Invoke(this);
        PersistentPlayerStats.Instance.SetPlayerStats(this);
        speedMultiplier = baseSpeedMultiplier;
    }

    protected virtual void Update()
    {
        if (input.attack)
        {
            TryAttack();
        }

        attackTimer -= Time.deltaTime;
    }

    protected virtual void FixedUpdate()
    {
        Move();
        Aim();
    }

    protected virtual void Move()
    {
        transform.position += (Vector3)input.movement * stats.speed * speedMultiplier * Time.deltaTime;
    }

    protected virtual void Aim()
    {
        Vector2 direction = (input.mousePos - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        OnAim?.Invoke(angle);
    }

    protected abstract void Attack();

    protected virtual void TryAttack()
    {
        if (attackTimer <= 0)
        {
            attackTimer = stats.attackSpeed;
            Attack();
            OnAttack?.Invoke();
        }
    }

    public Vector3 GetDirection()
    {
        return input.movement;
    }

    // Can only set the value of a component variable if accessing directly
    // Doesn't work: Rigidbody2D rb = GetComponenet<RigidBody2D>(); -> rb.mass = <insert value>
    // Works: GetComponent<RigidBody2D>().mass = <insert value>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(collisionTag))
        {
            GetComponent<Rigidbody2D>().mass = 100; // Set to large enough mass to resist any collisions
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(collisionTag))
        {
            GetComponent<Rigidbody2D>().mass = 1; // Reset to default player mass
        }
    }

    protected virtual void OnDestroy()
    {
        OnPlayerExit?.Invoke();
    }
}
