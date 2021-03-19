using System.Collections;
using UnityEngine;

public class LavaRock : Bullet
{
    private GameObject origin = null;
    private float flightTime = 0;
    private bool active = false;

    [SerializeField] private float lifeTime = 0;

    private const string playerTag = "Player";
    private const string wallTag = "Solid Terrain";

    private const int mass = 1000000;

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
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            active = true;
            rb.velocity = Vector2.zero;
            rb.mass = mass;
            rb.angularVelocity = 0;
            transform.Find("Graphic").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(delayedDeath());
        }

        if (origin == null)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active && collision.gameObject.CompareTag(wallTag))
        {
            flightTime = 0;
        }
        else if (active && collision.gameObject.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    IEnumerator delayedDeath()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
