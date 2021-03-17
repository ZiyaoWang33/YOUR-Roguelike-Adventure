using UnityEngine;
using System;

public abstract class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerEnter;
    public event Action OnPlayerExit;

    [HideInInspector] public float speedMultiplier = 1;
    [HideInInspector] public float damageMultipier = 1;

    [SerializeField] protected PlayerStats stats = null;
    [SerializeField] protected PlayerInput input = null;
    [SerializeField] protected Transform rotator = null;
    [SerializeField] protected SpriteRenderer character = null;
    [SerializeField] protected AudioSource sfx = null;

    protected float attackTimer = 0;

    private const string collisionTag = "Solid Terrain";

    protected virtual void Awake()
    {
        OnPlayerEnter?.Invoke(this);
        AudioController.Instance.ChangeSFXTrack(sfx);
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
        transform.position += (Vector3)input.movement * stats.speed * speedMultiplier * Time.deltaTime;

        Vector2 direction = (input.mousePos - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        character.flipX = angle > 90 || angle < -90;
    }

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

    protected abstract void Attack();

    protected virtual void TryAttack()
    {
        if (attackTimer <= 0)
        {
            sfx.PlayOneShot(sfx.clip);
            attackTimer = stats.attackSpeed;
            Attack();
        }
    }

    public Vector3 GetDirection()
    {
        return input.movement;
    }

    protected virtual void OnDisable()
    {
        OnPlayerExit?.Invoke();
    }
}
