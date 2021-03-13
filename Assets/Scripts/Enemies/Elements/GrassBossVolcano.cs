using UnityEngine;

public class GrassBossVolcano : MonoBehaviour, IBossElement
{
    [SerializeField] private GrassBoss boss = null;
    private Player player = null;
    [SerializeField] private GameObject whip = null;
    [SerializeField] private GameObject seed = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [SerializeField] private float whipLifeTime = 0;
    [SerializeField] private float whipRotationSpeed = 0;
    [SerializeField] private float seedLifeTime = 0;
    [SerializeField] private int seeds = 0;

    public void UseAbility(int ability)
    {
        if (ability == 0)
        {
            Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotator.eulerAngles = Vector3.forward * angle;

            WoodBranchWhip newWhip = Instantiate(whip, shootPoint.position, shootPoint.rotation).GetComponent<WoodBranchWhip>();
            newWhip.SetStats(gameObject, whipRotationSpeed, whipLifeTime);
        }
        else if (ability == 1)
        {
            for (int i = 0; i < seeds; i++)
            {
                float angle = Random.Range(0, 360);
                rotator.eulerAngles = Vector3.forward * angle;

                SeedBullet newSeed = Instantiate(seed, shootPoint.position, shootPoint.rotation).GetComponent<SeedBullet>();
                newSeed.SetStats(gameObject, Random.Range(0.1f, 1f));
            }
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
        return "volcano";
    }
}