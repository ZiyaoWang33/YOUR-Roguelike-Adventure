using UnityEngine;

public class SlowingEffect : Debuff
{
    private float slowingCap = 0.5f;

    public void SetStats(float slowingEffect, float effectCap = 0.5f)
    {
        slowingCap = effectCap;
        player.speedMultiplier -= (player.speedMultiplier > player.baseSpeedMultiplier * (1 - slowingCap)) ? slowingEffect : 0;
    }

    protected override void RemoveEffect()
    {
        base.RemoveEffect();
        player.speedMultiplier = player.baseSpeedMultiplier;
    }
}
