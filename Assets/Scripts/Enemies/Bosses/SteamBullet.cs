using UnityEngine;

public class SteamBullet : Bullet
{
    [HideInInspector] public float sizeMultiplier = 1;
    
    protected override void Awake()
    {
        base.Awake();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * sizeMultiplier, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Slow player for some time
    }
}
