using UnityEngine;

public class Tremor : MonoBehaviour
{
    [HideInInspector] public Player player = null;
    [SerializeField] protected int damage = 1;
    [SerializeField] private float lifetime = 0;
    [SerializeField] [Range(0, 1)] private float slowingEffect = 0;

    private Health playerHealth = null;
    protected float slowTimer = 0;
    private bool damageActive = false;

    private const string playerTag = "Player";

    protected virtual void Update()
    {
        lifetime -= Time.deltaTime;
        slowTimer -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        if (slowTimer <= 0)
        {
            player.speedMultiplier = 1;
        }
        if (damageActive)
        {
            playerHealth.TakeDamage(damage);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerHealth == null && other.tag.Equals(playerTag))
        {
            playerHealth = other.GetComponent<Health>();
        }

        if (playerHealth)
        {
            damageActive = true;
        }

        if (other.gameObject.Equals(player.gameObject) && player.speedMultiplier >= 1)
        {
            player.speedMultiplier -= slowingEffect;
            slowTimer = 3;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        damageActive = false;
    }

}
