using UnityEngine;

public class Poison : MonoBehaviour
{
    [HideInInspector] public int damageMultiplier = 1;

    [SerializeField] private int damage = 5;

    private Health health = null;

    private float damageTimer = 0;
    [SerializeField] private float damageCooldown = 1;
    private float time = 0;
    private float timer = 0;

    private void Awake()
    {
        health = gameObject.GetComponent<Health>();

        damageTimer = damageCooldown;
        time = Random.Range(3.0f, 5.0f);
        timer = time;
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
