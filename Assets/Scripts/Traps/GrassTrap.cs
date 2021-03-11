using UnityEngine;

public class GrassTrap : Trap
{
    [SerializeField] private float debuffTime = 2;

    private const float slowRatio = 1; // Set to 1 for 100% slow 

    protected override void EnterEffect()
    {
        player.AddComponent<SlowingEffect>().SetStats(slowRatio, debuffTime, slowRatio);
    }

    protected override void ExitEffect()
    {

    }
}