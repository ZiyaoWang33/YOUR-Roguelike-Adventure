using UnityEngine;

public class IceTrap : Trap
{
    [SerializeField] private float distance = 5;

    protected override void EnterEffect()
    {
        SlidingEffect debuff = player.GetComponent<SlidingEffect>();
        if (debuff)
        {
            Destroy(player.GetComponent<SlidingEffect>());
        }
        player.AddComponent<SlidingEffect>().SetStats(distance, true);
    }

    protected override void ExitEffect()
    {

    }
}