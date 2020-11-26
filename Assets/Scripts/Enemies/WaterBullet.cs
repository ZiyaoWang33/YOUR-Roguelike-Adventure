using UnityEngine;

public class WaterBullet : Bullet
{
    [SerializeField] private float maxDistance = 0;
    [SerializeField] private float explosionRadius = 0;
    [SerializeField] private int explosionDamage = 0;
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private GameObject graphic = null;

    private Vector3 originalPos = Vector3.zero;
    private RaycastHit2D explodeHit = new RaycastHit2D();

    protected override void Awake()
    {
        base.Awake();
        originalPos = transform.position;
    }

    private void AnimateExplosion()
    {
        graphic.transform.localScale = new Vector2(explosionRadius, explosionRadius);
    }

    private void Explode()
    {
        AnimateExplosion();

        if (Physics2D.CircleCast(transform.position, explosionRadius, Vector2.zero, explosionRadius, playerLayer))
        {
            explodeHit = Physics2D.CircleCast(transform.position, explosionRadius, Vector2.zero, explosionRadius, playerLayer);
            explodeHit.transform.gameObject.GetComponent<Health>().TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(originalPos, transform.position) >= maxDistance)
        {
            Explode();
        }
    }
}
