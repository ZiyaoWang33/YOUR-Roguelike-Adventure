using UnityEngine;

public class SteamBullet : Bullet
{
    [HideInInspector] public float sizeMultiplier = 1;

    private const string playerTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * sizeMultiplier, transform.localScale.z);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(playerTag))
        {
            SlowingEffect newDebuff = collision.gameObject.AddComponent<SlowingEffect>();
            newDebuff.SetInitial(null, Random.Range(1f, 2f));
            newDebuff.SetStats(Random.Range(0f, 0.3f));
        }
    }
}
