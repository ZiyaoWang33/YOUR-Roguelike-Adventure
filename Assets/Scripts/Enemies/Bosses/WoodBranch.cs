using UnityEngine;

public class WoodBranch : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int damageMultiplier = 1;
    [SerializeField] private int healingAmount = 10;

    private GameObject origin = null;
    private bool healing = false;
    private bool poison = false;
    private bool slowing = false;

    private const string playerTag = "Player";

    public void SetStats(GameObject _origin, bool _healing = false, bool _poison = false, bool _slowing = false)
    {
        origin = _origin;
        healing = _healing;
        poison = _poison;
        slowing = _slowing;
    }

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
            DotDamage debuff = player.GetComponent<DotDamage>();
            if (debuff)
            {
                debuff.Reapply();
            }
            else
            {
                player.AddComponent<DotDamage>().SetStats(1, damageMultiplier, 1f, Random.Range(3f, 5f));
            }
        }

        if (slowing)
        {
            player.AddComponent<SlowingEffect>().SetStats(Random.Range(0f, 0.3f), Random.Range(1f, 2f));
        }
    }
}
