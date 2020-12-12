using UnityEngine;

public class GrassBossWoods : MonoBehaviour, IBossElement
{
    [SerializeField] private GrassBoss boss = null;
    private Player player = null;
    [SerializeField] private float chargeSpeedMultiplier = 1;
    [SerializeField] private float chargeDuration = 1;
    [SerializeField] private float chargeCooldown = 0;
    [SerializeField] private GameObject tremor = null;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    protected Vector3 chargeDirection = Vector3.zero;
    protected float chargeTimer = 0;

    private void Update()
    {
        chargeTimer -= Time.deltaTime;

        if (chargeTimer > 0)
        {
            if (chargeTimer >= chargeCooldown - chargeDuration)
            {
                Instantiate(tremor, boss.transform.position, Quaternion.identity).GetComponent<Tremor>().SetUp(player);
            }
            else
            {
                boss.speedMultiplier = 1;
                boss.keepDirection = false;
            }
        }
    }

    public void UseAbility(int ability)
    {
        if (ability == 0)
        {
            chargeDirection = (player.transform.position - transform.position).normalized;
            boss.SetDirection(chargeDirection);
            boss.keepDirection = true;
            boss.speedMultiplier = chargeSpeedMultiplier;
            chargeTimer = chargeCooldown;
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

    public string GetElement()
    {
        return "woods";
    }
}