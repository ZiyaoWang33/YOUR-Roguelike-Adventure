using System.Collections;
using UnityEngine;

public class SeedBullet : Bullet
{
    private GameObject origin = null;
    private float flightTime = 0;
    private bool active = false;

    [SerializeField] private float lifeTime = 0;

    private const string playerTag = "Player";
    private const string wallTag = "Solid Terrain";

    public void SetStats(GameObject origin, float flightTime)
    {
        this.origin = origin;
        this.flightTime = flightTime;
    }

    private void Update()
    {
        flightTime -= Time.deltaTime;

        if (flightTime <= 0)
        {
            active = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (origin == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.CompareTag(wallTag))
        {
            flightTime = 0;
        }
        else if (active && collision.gameObject.CompareTag(playerTag))
        {
            GameObject player = collision.gameObject;

            player.GetComponent<Health>().TakeDamage(damage);
            SlowingEffect newDebuff = player.AddComponent<SlowingEffect>();
            newDebuff.SetInitial(null, lifeTime);
            newDebuff.SetStats(1, 1);
            origin.GetComponent<Health>().Heal(damage);

            StartCoroutine(delayedDeath());
        }
    }

    IEnumerator delayedDeath()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
