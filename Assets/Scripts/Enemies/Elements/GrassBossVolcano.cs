using UnityEngine;

public class GrassBossVolcano : MonoBehaviour, IBossElement
{
    [SerializeField] private GrassBoss boss = null;
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