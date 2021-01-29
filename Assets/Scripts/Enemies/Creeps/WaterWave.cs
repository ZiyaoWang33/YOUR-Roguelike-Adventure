using UnityEngine;

public class WaterWave : Bullet
{
    [HideInInspector] public float sizeMultiplier = 1;
    private float baseDamage = 0;

    protected override void Awake()
    {
        base.Awake();
        baseDamage = damage;
    }

    void Update()
    {
        sizeMultiplier = 1 + Time.deltaTime;
        damage = (int)(baseDamage / transform.localScale.y * 1.5);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * sizeMultiplier, transform.localScale.z - 1);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out Health health);
            health.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
