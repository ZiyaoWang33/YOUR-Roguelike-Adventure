using UnityEngine;

public class Gunner : Player
{
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform gun = null;
    [SerializeField] private GameObject bullet = null;

    private void OnEnable()
    {
        OnAim += OnAimEventHandler;
    }

    protected override void Attack()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Bullet>().SetDamage(damageMultipier);
    }

    private void OnAimEventHandler(float angle)
    {
        gun.localScale = angle > 90 || angle < -90 ? Vector3.one - Vector3.up * 2 : Vector3.one;
    }

    private void OnDisable()
    {
        OnAim += OnAimEventHandler;
    }
}
