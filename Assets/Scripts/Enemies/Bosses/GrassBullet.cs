using UnityEngine;

public class GrassBullet : Bullet
{
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private Transform graphic = null;
    [SerializeField] private float turnSpeed = 0;

    private Vector3 originalPos = Vector3.zero;

    private const string playerTag = "Player";
 
    protected override void Awake()
    {
        base.Awake();
        originalPos = transform.position;
    }

    private void Update()
    {
        //transform.Rotate(Vector3.up * (turnSpeed * Time.deltaTime));
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
}