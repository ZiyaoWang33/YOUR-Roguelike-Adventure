using UnityEngine;

public class IceTrap : Trap
{
    private const float distance = 5; // in tiles, not used but could be useful in the future

    [SerializeField] private GameObject slidingVFX = null;
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

        SlidingEffect newDebuff = player.AddComponent<SlidingEffect>();
        newDebuff.SetInitial(slidingVFX, slidingTime);
        newDebuff.SetStats(acceleration, speedLimit, true);
    }

    protected override void ExitEffect()
    {

    }
}