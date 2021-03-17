using UnityEngine;

public class Gunner : Player
{
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform gun = null;
    [SerializeField] private GameObject bullet = null;

    protected override void Attack()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Bullet>().SetDamage(damageMultipier);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        gun.localScale = character.flipX ? Vector3.one - Vector3.up * 2 : Vector3.one;
    }
}
