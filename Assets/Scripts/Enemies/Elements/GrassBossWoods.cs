using UnityEngine;

public class GrassBossWoods : MonoBehaviour, IBossElement
{
    [SerializeField] private Boss boss = null;
    private Player player = null;
    [SerializeField] private float abilityDistance = 0;
    [SerializeField] protected float chargeSpeedMultiplier = 1;
    [SerializeField] protected float chargeTime = 1;
    [SerializeField] private GameObject tremor = null;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    protected float chargeTimer = 0;
    protected Vector2 chargeDirection;

    protected virtual void Update()
    {
        chargeTimer -= Time.deltaTime;

        if (chargeTimer > 0)
        {
            boss.SetDirection(chargeDirection);
            boss.speedMultiplier = chargeSpeedMultiplier;
            Tremor newTremor = Instantiate(tremor, boss.transform.position, Quaternion.identity).GetComponent<Tremor>();
            newTremor.player = player;
        }
     
    }

    public void UseAbility(int ability)
    {
        if (ability == 0)
        {
            chargeDirection = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
            boss.SetDirection(chargeDirection);
            boss.speedMultiplier = chargeSpeedMultiplier;
            chargeTimer = chargeTime;
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();
        }

        Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;
        Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation);
    }
}