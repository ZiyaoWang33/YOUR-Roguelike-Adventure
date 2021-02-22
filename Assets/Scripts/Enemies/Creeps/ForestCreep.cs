using UnityEngine;

public class ForestCreep : Enemy
{
    private Player playerObject = null;
    [SerializeField] private GameObject whip = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [SerializeField] private float whipLifeTime = 0;
    [SerializeField] private float whipRotationSpeed = 0;
    [SerializeField] private float attackCooldown = 0;

    protected override void Awake()
    {
        base.Awake();

        inRange = true;
        attackTimer = Random.Range(attackCooldown / 3, attackCooldown / 2);
    }

    protected override void Attack()
    {
        base.Attack();

        if (playerObject == null)
        {
            playerObject = player.gameObject.GetComponent<Player>();
        }

        Vector2 direction = ((player.transform.position + playerObject.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        WoodBranchWhip newWhip = Instantiate(whip, shootPoint.position, shootPoint.rotation).GetComponent<WoodBranchWhip>();
        newWhip.origin = gameObject;
        newWhip.rotationSpeed = whipRotationSpeed;
        newWhip.lifeTime = whipLifeTime;
        newWhip.sizeMultiplier = 0.5f;

        attackTimer = Random.Range(attackCooldown / 2, attackCooldown);
    }
}
