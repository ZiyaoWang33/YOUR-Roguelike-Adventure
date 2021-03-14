using UnityEngine;

public class LavaBullet : Bullet
{
    private const string playerTag = "Player";

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            GameObject player = collision.gameObject;
            WeakenDefense debuff = player.GetComponent<WeakenDefense>();

            if (debuff)
            {
                debuff.StackEffect();
            }
            else
            {
                player.AddComponent<WeakenDefense>().SetStats(0.25f, 5, 3);
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
