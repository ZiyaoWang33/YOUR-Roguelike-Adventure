using UnityEngine;

public class WeakenDefense : MonoBehaviour
{
    private Health health = null;
    private float timer = 0;

    private float bonusDamage = 0;
    private int stackCap = 1;
    private float baseTime = 1;
    private float baseDamageMultiplier = 1;

    public void SetStats(float multiplier, int maxStacks, float lifetime)
    {
        bonusDamage = multiplier;
        stackCap = maxStacks;
        baseTime = lifetime;
        baseDamageMultiplier = gameObject.GetComponent<Health>().damageMultiplier;
        health = gameObject.GetComponent<Health>();

        StackEffect();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            health.damageMultiplier = 1;
            Destroy(this);
        }
    }

    public void StackEffect()
    {
        timer = baseTime;
        if ((health.damageMultiplier - baseDamageMultiplier) / bonusDamage <= stackCap)
        {
            health.damageMultiplier += bonusDamage;
        }
    }
}
