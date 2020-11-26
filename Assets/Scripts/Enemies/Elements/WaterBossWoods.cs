using UnityEngine;

public class WaterBossWoods : MonoBehaviour, IBossElement
{
    [SerializeField] private Enemy boss = null;
    private Player player = null;
    [SerializeField][Range(0, 0.5f)] private float abilityDampener = 0;
    [SerializeField] private GameObject root = null;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    public void UseAbility()
    {
        Root newRoot = Instantiate(root, player.transform.position + player.GetDirection() * abilityDampener, Quaternion.identity).GetComponent<Root>();
        newRoot.player = player;
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();
        }

        Vector2 direction = (player.transform.position - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;
        Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation);
    }
}
