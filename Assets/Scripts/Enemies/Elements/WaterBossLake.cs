using UnityEngine;

public class WaterBossLake : MonoBehaviour, IBossElement
{
    [SerializeField] private Enemy boss = null;
    private Player player = null;
    [SerializeField] private GameObject explosion = null; 
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;
    [SerializeField] private int shotSpeed = 1;

    private bool usedCloning = false;
    private GameObject partner = null;
    [HideInInspector] public bool isClone = false;

    private void Update()
    {
        GetComponent<WaterBoss>().lastBossToDefeat = !partner;
    }

    protected void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public void UseAbility(int ability)
    {
        if (ability == 0)
        {
            if (!isClone && !usedCloning)
            {
                usedCloning = true;

                WaterBossLake clone = Instantiate(gameObject, transform.position + (player.transform.position - transform.position) / 2, transform.rotation).GetComponent<WaterBossLake>();
                clone.isClone = true;
                clone.partner = gameObject;
                partner = clone.gameObject;
                partner.SetActive(true);
                partner.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
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
        WaterBulletCone bullets = Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation).GetComponent<WaterBulletCone>();
        bullets.mode = "spread";
        bullets.speed = shotSpeed;
    }

    public string GetElement()
    {
        return "lake";
    }
}
