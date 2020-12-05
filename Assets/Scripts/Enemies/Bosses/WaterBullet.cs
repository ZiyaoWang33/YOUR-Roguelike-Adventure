using UnityEngine;

public class WaterBullet : Bullet
{
    [HideInInspector] public bool explode = false;

    [SerializeField] private float maxDistance = 0;
    [SerializeField] private float explosionRadius = 0;
    [SerializeField] private int explosionDamage = 0;
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private Transform graphic = null;
    [SerializeField] private Animator anim = null;

    private Vector3 originalPos = Vector3.zero;
    private RaycastHit2D explodeHit = new RaycastHit2D();

    protected override void Awake()
    {
        base.Awake();
        originalPos = transform.position;
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

    private void FixedUpdate()
    {
        if (explode && Vector3.Distance(originalPos, transform.position) >= maxDistance)
        {
            Explode();
        }
    }
}
