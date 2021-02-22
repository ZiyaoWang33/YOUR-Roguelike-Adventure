using UnityEngine;

public class VolcanoCreep : Enemy
{
    protected override void Attack()
    {
        base.Attack();

        // Add burned debuff: player takes more dmg, stacks up to 5 times
    }
}
