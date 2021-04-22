using UnityEngine;

public class DotDamage : Debuff
{
    private int damage = 5;
    private int damageMultiplier = 1;

    private Health health = null;

    private float damageTimer = 0;
    private float damageCooldown = 1;
    private float baseTimer = 1;

    public void SetStats(int damage, int damageMultiplier, float cooldown)
    {
        this.damage = damage;
        this.damageMultiplier = damageMultiplier;
        damageCooldown = cooldown;
        baseTimer = lifetime;

        health = gameObject.GetComponent<Health>();
        damageTimer = damageCooldown;
    }

    protected override void Update()
    {
        base.Update();
        damageTimer -= Time.deltaTime;

        if (damageTimer <= 0)
        {
            health.TakeDamage(damage * damageMultiplier, true);
            damageTimer = damageCooldown;
        }
    }

    public void Reapply()
    {
        lifetime = baseTimer;
    }
}
