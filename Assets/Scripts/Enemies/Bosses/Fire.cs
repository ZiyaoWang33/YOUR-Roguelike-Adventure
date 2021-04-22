using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject vfx = null;
    [SerializeField] private FireStats stats = null;
    [SerializeField] private int damageMultiplier = 1;

    private GameObject player = null;
    private Health playerHealth = null;
    private bool damageActive = false;
    private float damageTimer = 0;
    private float lifetime = 0;

    private const string playerTag = "Player";

    private void Awake()
    {
        lifetime = stats.lifetime;
    }

    private void AddDotDamage()
    {
        if (player.TryGetComponent(out DotDamage _))
        {
            foreach (DotDamage dotdamage in player.GetComponents<DotDamage>())
            {
                Destroy(dotdamage);
            }
        }
        DotDamage newDot = player.AddComponent<DotDamage>();
        newDot.SetInitial(vfx, stats.dotLifetime);
        newDot.SetStats(stats.damage, damageMultiplier, stats.damageCooldown);
    }

    private void OnDamageTakenEventHandler()
    {
        damageTimer = stats.damageCooldown;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        damageTimer -= Time.deltaTime;

        if (damageActive && damageTimer <= 0)
        {
            playerHealth.TakeDamage(stats.damage);
            damageTimer = stats.damageCooldown;
        }

        if (lifetime <= 0)
        {
            if (player && damageActive)
                AddDotDamage();

            if (transform.parent.gameObject)
                Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null && collision.tag.Equals(playerTag))
        {
            player = collision.gameObject;
            playerHealth = player.GetComponent<Health>();
            playerHealth.OnDamageTaken += OnDamageTakenEventHandler;
        }

        if (player)
        {
            damageActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player)
        {
            damageActive = false;
            AddDotDamage();
        } 
    }

    private void OnDestroy()
    {
        if (player)
            playerHealth.OnDamageTaken -= OnDamageTakenEventHandler;
    }
}
