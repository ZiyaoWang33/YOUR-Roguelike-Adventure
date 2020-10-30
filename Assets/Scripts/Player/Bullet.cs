using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (Mathf.Abs(rb.velocity.x) < speed)
        {
            Destroy(gameObject);
        }
    }

    public void SetAngle(float angle)
    {
        transform.eulerAngles = Vector3.forward * angle;
    }
}
