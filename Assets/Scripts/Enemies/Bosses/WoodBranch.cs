using UnityEngine;

public class WoodBranch : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int damageMultiplier = 1;
    [SerializeField] private int healingAmount = 10;

    [HideInInspector] public GameObject origin = null;
    [HideInInspector] public bool healing = false;
    [HideInInspector] public bool poison = false;
    [HideInInspector] public bool slowing = false;

    private const string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Health>().TakeDamage(damage);
            ApplyEffects(player);
        }
    }

    private void ApplyEffects(GameObject player)
    {
        if (healing)
        {
            origin.GetComponent<Health>().Heal(healingAmount);
        }

        if (poison)
        {
            player.AddComponent<DotDamage>().SetStats(1, damageMultiplier, 1f, Random.Range(3f, 4f));
        }

        if (slowing)
        {
            player.AddComponent<SlowingEffect>().SetStats(Random.Range(0f, 0.3f), Random.Range(1f, 2f));
        }
    }
}
