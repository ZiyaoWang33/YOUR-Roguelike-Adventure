using UnityEngine;

public class FireBossVolcano : MonoBehaviour, IBossElement
{
    [SerializeField] private FireBoss boss = null;
    private Player player = null;
    [SerializeField] private GameObject fire = null;
    [SerializeField] private float chargeSpeedMultiplier = 1;
    [SerializeField] private float chargeDuration = 0;
    [SerializeField] private float timeBetweenTicks = 1;
    [SerializeField] private int damagePerTick = 1;
    [SerializeField] private GameObject rock = null;
    [SerializeField] private int rocks = 0;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    Vector3 chargeDirection = Vector3.zero;
    private float chargeTimer = 0;
    private float chargeCooldown = 0;

    private const int forever = 1000000;

    private void Awake()
    {
        boss.health.OnDeath += RemoveDebuff;
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
        Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
        float angle = 0;

        switch (ability)
        {
            case 0:
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rotator.eulerAngles = Vector3.forward * angle;
                LavaBulletCone bullets = Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation).GetComponent<LavaBulletCone>();
                break;

            case 1:
                for (int i = 0; i < rocks; i++)
                {
                    angle = Random.Range(-60, 60) + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rotator.eulerAngles = Vector3.forward * angle;

                    LavaRock newRock = Instantiate(rock, shootPoint.position, shootPoint.rotation).GetComponent<LavaRock>();
                    newRock.SetStats(gameObject, Random.Range(0.3f, 1f));
                }
                break;
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();
            DotDamage newDebuff = player.gameObject.AddComponent<DotDamage>();
            newDebuff.SetInitial(null, forever);
            newDebuff.SetStats(damagePerTick, 1, timeBetweenTicks);
            return;
        }

        boss.SetDirection((player.transform.position - transform.position).normalized);
        boss.keepDirection = true;
        boss.speedMultiplier = chargeSpeedMultiplier;

        chargeTimer = chargeCooldown;
    }

    private void RemoveDebuff()
    {
        Destroy(player.GetComponent<DotDamage>());
    }

    public string GetElement()
    {
        return "volcano";
    }
}