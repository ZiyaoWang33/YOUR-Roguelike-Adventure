using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    public int health { get { return _health; } }

    [SerializeField] private int baseDmg = 1;
    [SerializeField] private int minDmg = 1;

    private int maxHealth = 1;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        maxHealth = _health;

        if (gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            this.rb = rb;
        }
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _health += amount;
        _health = _health > maxHealth ? maxHealth : _health; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb)
        {
            int combinedVel = Mathf.RoundToInt(Mathf.Abs(this.rb.velocity.x) + Mathf.Abs(this.rb.velocity.y));
            combinedVel += collision.gameObject.TryGetComponent(out Rigidbody2D rb) ? Mathf.RoundToInt(Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)) : 0;
            int damage = combinedVel * baseDmg;

            if (damage >= minDmg)
            {
                TakeDamage(combinedVel * baseDmg);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
