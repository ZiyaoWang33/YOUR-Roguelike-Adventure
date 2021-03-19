using UnityEngine;
using UnityEngine.UI;

public class HealthBar : GameplayUIElement
{
    [SerializeField] private Slider slider = null;

    private Health health = null;
    private int currentHealth = 0;

    protected override void OnPlayerEnterEventHandler(Player player)
    {
        base.OnPlayerEnterEventHandler(player);

        health = player.gameObject.GetComponent<Health>();
        health.TakeDamage(currentHealth <= 0 ? 0 : health.maxHealth - currentHealth);
        health.OnDamageTaken += OnDamageTakenEventHandler;

        slider.maxValue = health.maxHealth;
        slider.value = health.health;
    }

    private void OnDamageTakenEventHandler()
    {
        slider.value = health.health;
        currentHealth = health.health;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (health)
        {
            health.OnDamageTaken -= OnDamageTakenEventHandler;
        }
    }
}
