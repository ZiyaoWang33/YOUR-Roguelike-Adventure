using UnityEngine;

public class WaterBossVolcano : MonoBehaviour, IBossElement
{
    [SerializeField] private WaterBoss boss = null;
    private Player player = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    public void UseAbility(int ability)
    {

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