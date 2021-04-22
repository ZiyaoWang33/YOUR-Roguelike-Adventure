using UnityEngine;

public class Tremor : MonoBehaviour
{
    [HideInInspector] public Player player = null;

    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 0;
    [SerializeField][Range(0, 1)] private float slowingEffect = 0;
    [SerializeField] private float slowTime = 0;

    private Health playerHealth = null;

    public void SetUp(Player player)
    {
        this.player = player;
        playerHealth = player.gameObject.GetComponent<Health>();
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(player.gameObject) && player.speedMultiplier >= 1)
        {
            playerHealth.TakeDamage(damage);
            SlowingEffect newDebuff = player.gameObject.AddComponent<SlowingEffect>();
            newDebuff.SetInitial(null, slowTime);
            newDebuff.SetStats(slowingEffect);
        }
    }
}
