using UnityEngine;

public class LavaTrap : Trap
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float cooldown = 1;

    private const float forever = 1000000; // Pseudo-eternity

    protected override void EnterEffect()
    {
        player.GetComponent<Health>().TakeDamage(damage);
        player.AddComponent<DotDamage>().SetStats(damage, difficultyMultiplier, cooldown, forever);
    }

    protected override void ExitEffect()
    {
        Destroy(player.GetComponent<DotDamage>());
    }
}