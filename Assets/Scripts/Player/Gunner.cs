using UnityEngine;

public class Gunner : Player
{
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform gun = null;
    [SerializeField] private GameObject bullet = null;

    protected override void Attack()
    {
        if (attackTimer <= 0)
        {
            GameObject bullet = Instantiate(this.bullet, shootPoint.position, shootPoint.rotation);
            attackTimer = stats.attackSpeed;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        gun.localScale = character.flipX ? Vector3.one - Vector3.up * 2 : Vector3.one;
    }
}
