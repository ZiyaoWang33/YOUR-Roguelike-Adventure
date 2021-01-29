using UnityEngine;

public class LakeCreep : Enemy
{
    private Player playerObject = null;

    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    protected override void Awake()
    {
        base.Awake();

        inRange = true;
        attackTimer = 1f;
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
        Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation);
    }
}
