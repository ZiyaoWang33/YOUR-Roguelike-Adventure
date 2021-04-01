using UnityEngine;

public class SlowingEffect : MonoBehaviour
{
    private Player player = null;
    private float slowingCap = 0.5f;
    private float timer = 0;

    public void SetStats(float slowingEffect, float lifetime, float effectCap = 0.5f)
    {
        timer = lifetime;
        slowingCap = effectCap;

        player = gameObject.GetComponent<Player>();
        player.speedMultiplier -= (player.speedMultiplier > player.baseSpeedMultiplier * (1 - slowingCap)) ? slowingEffect : 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            player.speedMultiplier = player.baseSpeedMultiplier;
            Destroy(this);
        }
    }
}
