using UnityEngine;

public class SlowingEffect : MonoBehaviour
{
    private float slowingCap = 0.5f;

    private Player player = null;

    private float timer = 0;

    public void SetStats(float slowingEffect, float lifetime)
    {
        timer = lifetime;

        player = gameObject.GetComponent<Player>();
        player.speedMultiplier -= (player.speedMultiplier > 1 - slowingCap) ? slowingEffect : 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            player.speedMultiplier = 1;
            Destroy(this);
        }
    }
}
