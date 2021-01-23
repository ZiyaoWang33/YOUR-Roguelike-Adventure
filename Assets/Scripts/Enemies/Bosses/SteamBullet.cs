using UnityEngine;

public class SteamBullet : Bullet
{
    [HideInInspector] public float sizeMultiplier = 1;
    
    protected override void Awake()
    {
        base.Awake();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * sizeMultiplier, transform.localScale.z);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.AddComponent<SlowingEffect>().SetStats(Random.Range(0f, 0.3f), Random.Range(1f, 2f));
        }
    }
}
