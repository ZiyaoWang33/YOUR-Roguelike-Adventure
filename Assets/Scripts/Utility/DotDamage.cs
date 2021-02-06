using UnityEngine;

public class DotDamage : MonoBehaviour
{
    private int damage = 5;
    private int damageMultiplier = 1;

    private Health health = null;

    private float damageTimer = 0;
    private float damageCooldown = 1;
    private float timer = 0;

    public void SetStats(int damage, int damageMultiplier, float cooldown, float lifetime)
    {
        this.damage = damage;
        this.damageMultiplier = damageMultiplier;
        damageCooldown = cooldown;
        timer = lifetime;

        health = gameObject.GetComponent<Health>();
        damageTimer = damageCooldown;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (damageTimer <= 0)
        {
            print(true);
            health.TakeDamage(damage * damageMultiplier);
            damageTimer = damageCooldown;
        }

        if (timer <= 0)
        {
            Destroy(this);
        }
    }
}
