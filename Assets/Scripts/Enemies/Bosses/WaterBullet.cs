using UnityEngine;

public class WaterBullet : Bullet
{
    [HideInInspector] public bool explode = false;
    [HideInInspector] public bool spread = false;

    [SerializeField] private float maxDistance = 0;
    [SerializeField] private float lifeTime = 0;
    [SerializeField] private float explosionRadius = 0;
    [SerializeField] private int explosionDamage = 0;
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private Transform graphic = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject bulletSpread = null;

    private Vector3 originalPos = Vector3.zero;
    private RaycastHit2D explodeHit = new RaycastHit2D();
    private float time = 0;

    private const string playerTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        originalPos = transform.position;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            collision.gameObject.TryGetComponent(out Health health);
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void AnimateExplosion()
    {
        anim.enabled = true;
    }

    private void ExplosionAnimationEventHandler(int frame)
    {
        switch (frame)
        {
            case 0:
                graphic.localScale *= explosionRadius / 2;
                break;
            case 1:
                graphic.localScale *= 2;
                break;
            case 2:
                Destroy(gameObject);
                break;
        }
    }

    private void Explode()
    {
        AnimateExplosion();

        if (Physics2D.CircleCast(transform.position, explosionRadius, Vector2.zero, explosionRadius, playerLayer))
        {
            explodeHit = Physics2D.CircleCast(transform.position, explosionRadius, Vector2.zero, explosionRadius, playerLayer);
            explodeHit.transform.gameObject.GetComponent<Health>().TakeDamage(explosionDamage);
        }
    }

    private void Spread()
    {
        Instantiate(bulletSpread, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (explode && Vector3.Distance(originalPos, transform.position) >= maxDistance)
        {
            Explode();
        }

        if (spread && time >= lifeTime)
        {
            Spread();
        }
    }
}
