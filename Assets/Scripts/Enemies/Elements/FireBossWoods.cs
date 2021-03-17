using UnityEngine;

public class FireBossWoods : MonoBehaviour, IBossElement
{
    [SerializeField] private FireBoss boss = null;
    [SerializeField] private Collider2D col = null;
    private Player player = null;
    [SerializeField] private GameObject fire = null;
    [SerializeField] private float chargeSpeedMultiplier = 1;
    [SerializeField] private float chargeDuration = 0;
    [SerializeField] private GameObject missile = null;
    [SerializeField] private float missileSpread = 0;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;
    [SerializeField] private GameObject spitfire = null;

    Vector3 chargeDirection = Vector3.zero;
    private float chargeTimer = 0;
    private float chargeCooldown = 0;
    private GameObject flower = null;

    private void Awake()
    {
        chargeCooldown = boss.stats.attackSpeed;
    }

    private void Update()
    {
        chargeTimer -= Time.deltaTime;

        if (chargeTimer > 0)
        {
            if (chargeTimer >= chargeCooldown - chargeDuration)
            {
                float angle = 90 - Mathf.Atan2(chargeDirection.y, chargeDirection.x) * Mathf.Rad2Deg;
                Instantiate(fire, transform.position, Quaternion.Euler(Vector3.forward * angle));
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
        Vector2 direction = (player.transform.position + player.GetDirection() * Random.Range(0, 2) - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        switch (ability)
        {
            case 0:
                Instantiate(missile, shootPoint.position - shootPoint.up * missileSpread, shootPoint.rotation).GetComponent<Missile>().SetUp(col, boss.player.transform);
                Instantiate(missile, shootPoint.position + shootPoint.up * missileSpread, shootPoint.rotation).GetComponent<Missile>().SetUp(col, boss.player.transform);
                
                break;
            case 1:
                if (flower == null)
                {
                    flower = Instantiate(spitfire, transform.position + (Vector3)direction * transform.localScale.x, shootPoint.rotation);
                }
                break;
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();
        }

        boss.SetDirection((player.transform.position - transform.position).normalized);
        boss.keepDirection = true;
        boss.speedMultiplier = chargeSpeedMultiplier;

        chargeTimer = chargeCooldown;
    }

    public string GetElement()
    {
        return "woods";
    }
}
