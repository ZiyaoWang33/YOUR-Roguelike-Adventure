using UnityEngine;

public class WoodBranch : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int damageMultiplier = 1;
    [SerializeField] private int healingAmount = 10;

    private DotDamage currentDot = null;
    private GameObject origin = null;
    private bool healing = false;
    private bool poison = false;
    private bool slowing = false;

    private const string playerTag = "Player";

    public void SetStats(GameObject origin, bool healing = false, bool poison = false, bool slowing = false)
    {
        this.origin = origin;
        this.healing = healing;
        this.poison = poison;
        this.slowing = slowing;
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
            if (currentDot)
            {
                currentDot.Reapply();
            }
            else
            {
                currentDot = player.AddComponent<DotDamage>();
                currentDot.SetInitial(null, Random.Range(3f, 5f));
                currentDot.SetStats(1, damageMultiplier, 1f);
            }
        }

        if (slowing)
        {
            SlowingEffect newDebuff = player.AddComponent<SlowingEffect>();
            newDebuff.SetInitial(null, Random.Range(1f, 2f));
            newDebuff.SetStats(Random.Range(0f, 0.3f));
        }
    }
}
