using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] private Collider2D col = null;

    private Collider2D boss = null;
    private Transform player = null;

    public void SetUp(Collider2D boss, Transform player)
    {
        this.boss = boss;
        this.player = player;

        IgnoreBoss();
    }

    protected override void Awake()
    {
        col.enabled = false;
    }

    public void IgnoreBoss()
    {
        Physics2D.IgnoreCollision(col, boss);
        col.enabled = true;
    }

    private void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.forward * angle;

        rb.velocity = transform.right * speed;
    }
}
