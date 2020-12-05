using UnityEngine;

public class ForestCreep : Enemy
{
    protected override void Attack()
    {
        base.Attack();

        player.gameObject.AddComponent<Poison>().damageMultiplier = difficultyMultiplier;
    }
}
