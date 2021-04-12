using UnityEngine;

public class GrassBossLake : MonoBehaviour, IBossElement
{
    [SerializeField] private GrassBoss boss = null;
    private Player player = null;
    [SerializeField] private GameObject root = null;
    [SerializeField] private GameObject summon = null;
    [SerializeField] private GameObject whip = null;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private float whipLifeTime = 0;
    [SerializeField] private float whipRotationSpeed = 0;

    private float timer = 0;

    private float slowingEffect = 0.5f;

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void UseAbility(int ability)
    {
        switch (ability)
        {
            case 0:
                GrassWaterCreepSet summonedCreeps = Instantiate(summon, shootPoint.position, transform.rotation).GetComponent<GrassWaterCreepSet>();
                summonedCreeps.SetTargets(gameObject, player);
                break;

            case 1:
                if (boss.health.health <= boss.health.maxHealth / 4)
                {
                    Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rotator.eulerAngles = Vector3.forward * angle;

                    WoodBranchWhip newWhip = Instantiate(whip, shootPoint.position, shootPoint.rotation).GetComponent<WoodBranchWhip>();
                    newWhip.SetStats(gameObject, whipRotationSpeed, whipLifeTime, 1, true);
                }
                break;
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();

            // Covers the room in roots and applies a slowing effect on the player
            // The slowing is independent of the roots so they can be replaced by
            // one object that covers the whole room  
            int roomLength = 26;
            int roomHeight = 10;
            for (int x = 2; x < roomLength - 1; x++)
            {
                for (int y = 2; y < roomHeight - 1; y++)
                {
                    Root newRoot = Instantiate(root, transform.position + new Vector3(x - roomLength / 2, y - roomHeight / 2, -1), Quaternion.identity).GetComponent<Root>();
                    newRoot.SetUp(player, true);
                }
            }
            player.speedMultiplier -= slowingEffect;
        }

        if (timer <= 0)
        {
            Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotator.eulerAngles = Vector3.forward * angle;
            Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation);

            timer = attackCooldown;
        }
    }

    public string GetElement()
    {
        return "lake";
    }
}