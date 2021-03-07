using UnityEngine;

public class VolcanoCreep : Enemy
{
    protected override void Attack()
    {
        base.Attack();
        player.TakeDamage(stats.damage);

        WeakenDefense debuff = player.GetComponent<WeakenDefense>();
        if (debuff)
        {
            debuff.StackEffect();
        }
        else
        {
            player.gameObject.AddComponent<WeakenDefense>().SetStats(0.25f, 5, 3);
        }
    }
}
