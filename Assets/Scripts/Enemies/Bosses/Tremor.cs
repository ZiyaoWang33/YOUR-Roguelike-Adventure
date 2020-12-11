using UnityEngine;

public class Tremor : MonoBehaviour
{
    [HideInInspector] public Player player = null;
    [SerializeField] protected int damage = 1;
    [SerializeField] [Range(0, 1)] private float slowingEffect = 0;

    protected float slowTimer = 0;

    protected virtual void Update()
    {
        slowTimer -= Time.deltaTime;
        Destroy(gameObject);

        if (slowTimer <= 0)
        {
            player.speedMultiplier = 1;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(player.gameObject) && collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);

            if (player.speedMultiplier >= 1)
            {
                player.speedMultiplier -= slowingEffect;
                slowTimer = 3;
            }
        }
    }

}
