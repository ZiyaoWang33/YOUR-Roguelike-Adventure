using UnityEngine;

public class VolcanoCreep : Enemy
{
    private Player playerObject = null;

    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;
    [SerializeField] private float attackCooldown = 0;
    // Lava bullet will clip the creep's hitbox if it is > 0.8x scale
    [SerializeField] [Range(0, 0.8f)] private float bulletSize = 0.8f;

    protected override void Awake()
    {
        base.Awake();

        inRange = true;
        attackTimer = attackCooldown;
    }

    protected override void Attack()
    {
        base.Attack();

        if (playerObject == null)
        {
            playerObject = player.gameObject.GetComponent<Player>();
        }

        Vector2 direction = ((playerObject.transform.position + playerObject.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;
        Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation).GetComponent<LavaBullet>().SetUp(bulletSize);

        attackTimer = attackCooldown;       
    }
}
