using UnityEngine;

public class IceTrap : Trap
{
    private float distance = 5; // in tiles, not used but could be useful in the future
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float slidingTime = 1;
    [SerializeField] private float speedLimit = 5;

    protected override void EnterEffect()
    {
        SlidingEffect debuff = player.GetComponent<SlidingEffect>();
        if (debuff)
        {
            Destroy(player.GetComponent<SlidingEffect>());
        }
        player.AddComponent<SlidingEffect>().SetStats(acceleration, slidingTime, speedLimit, true);
    }

    protected override void ExitEffect()
    {

    }
}