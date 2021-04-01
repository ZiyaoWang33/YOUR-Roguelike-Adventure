using UnityEngine;

public class SpeedDebuff : MonoBehaviour
{
    private Player player = null;
    private float slowTimer = 0;
    
    public void SetUp(Player player, float slowingEffect, float slowTime)
    {
        this.player = player;
        slowTimer = slowTime;

        player.speedMultiplier -= slowingEffect;
    }

    private void Update()
    {
        slowTimer -= Time.deltaTime;

        if (slowTimer <= 0)
        {
            player.speedMultiplier = player.baseSpeedMultiplier;
            Destroy(this);
        }
    }
}
