using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 0;

    private Health player = null;
    private bool damageActive = false;

    private const string playerTag = "Player";

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        if (damageActive)
        {
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player == null && other.tag.Equals(playerTag))
        {
            player = other.GetComponent<Health>();
        }

        if (player)
        {
            damageActive = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        damageActive = false;
    }
}
