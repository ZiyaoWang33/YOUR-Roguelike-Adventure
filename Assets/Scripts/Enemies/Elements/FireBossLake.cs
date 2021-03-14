using UnityEngine;

public class FireBossLake : MonoBehaviour, IBossElement
{
    [SerializeField] private FireBoss boss = null;
    private Player player = null;
    [SerializeField] private GameObject steamLaser = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [SerializeField] private float maxBulletSize = 5;
    private float shootTimer = 5;
    private float shootCooldown = 2;

    private void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
            else
            {
                shootTimer = 5;
                shootCooldown = 2;
            }
        }
    }

    public void UseAbility(int ability)
    {
        if (ability == 0 && shootTimer > 0)
        {
            float healthPercent = (float) boss.health.health / boss.health.maxHealth;
            float sizeMultiplier = (maxBulletSize - 1) * (1 - healthPercent) + 1;
            steamLaser.GetComponent<SteamLaser>().sizeMultiplier = sizeMultiplier;

            Vector2 direction = (player.transform.position + player.GetDirection() - rotator.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotator.eulerAngles = Vector3.forward * angle;
            Instantiate(steamLaser, shootPoint.position, shootPoint.rotation);
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();
        }
    }

    public string GetElement()
    {
        return "lake";
    }
}