using UnityEngine;

public class LavaTrap : Trap
{
    [SerializeField] private GameObject fireVFX = null;
    [SerializeField] private int damage = 5;
    [SerializeField] private float cooldown = 1;

    private DotDamage current = null;

    private const float forever = 1000000; // Pseudo-eternity

    protected override void EnterEffect()
    {
        player.GetComponent<Health>().TakeDamage(damage);

        if (current)
        {
            current.Reapply();
        }
        else
        {
            current = player.AddComponent<DotDamage>();
            current.SetInitial(fireVFX, forever);
            current.SetStats(damage, difficultyMultiplier, cooldown);
        }
    }

    protected override void ExitEffect()
    {
        Destroy(current);
    }
}