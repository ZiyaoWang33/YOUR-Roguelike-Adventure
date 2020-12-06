using UnityEngine;

public class DotDamage : MonoBehaviour
{
    private int damage = 5;
    private int damageMultiplier = 1;

    private Health health = null;

    private float damageTimer = 0;
    private float damageCooldown = 1;
    private float time = 0;
    private float timer = 0;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();

        damageTimer = damageCooldown;
        time = Random.Range(3.0f, 5.0f);
        timer = time;
    }

    public void SetStats(int damage, int damageMultiplier, float cooldown)
    {
        this.damage = damage;
        this.damageMultiplier = damageMultiplier;
        damageCooldown = cooldown;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (damageTimer <= 0)
        {
            health.TakeDamage(damage * damageMultiplier);
            damageTimer = damageCooldown;
        }

        if (timer <= 0)
        {
            Destroy(this);
        }
    }
}
