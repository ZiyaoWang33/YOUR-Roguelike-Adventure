using UnityEngine;

public class ForestCreep : Enemy
{
    protected override void Attack()
    {
        base.Attack();

        player.gameObject.AddComponent<DotDamage>().SetStats(1, difficultyMultiplier, 1f, Random.Range(3f, 4f));
    }
}
