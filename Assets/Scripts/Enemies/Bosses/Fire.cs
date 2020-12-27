using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageCooldown = 0;
    [SerializeField] private float lifetime = 0;
    [SerializeField] private int damageMultiplier = 1;
    [SerializeField] private float dotLifetime = 0;

    private GameObject player = null;
    private Health playerHealth = null;
    private bool damageActive = false;
    private float damageTimer = 0;

    private const string playerTag = "Player";

    private void Update()
    {
        lifetime -= Time.deltaTime;
        damageTimer -= Time.deltaTime;

        if (damageActive && damageTimer <= 0)
        {
            playerHealth.TakeDamage(damage);
            damageTimer = damageCooldown;
        }

        if (lifetime <= 0)
        {
            if (player && damageActive)
            {
                player.AddComponent<DotDamage>().SetStats(damage, damageMultiplier, damageCooldown, dotLifetime);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null && collision.tag.Equals(playerTag))
        {
            player = collision.gameObject;
            playerHealth = player.GetComponent<Health>();
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
            player.AddComponent<DotDamage>().SetStats(damage, damageMultiplier, damageCooldown, dotLifetime);
        } 
    }
}
