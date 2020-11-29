using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Attack()
    {
        base.Attack();
        player.TakeDamage(stats.damage * difficultyMultiplier);
        // add animation or etc.
    }
}
