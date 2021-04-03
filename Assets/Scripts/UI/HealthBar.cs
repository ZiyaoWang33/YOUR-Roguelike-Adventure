using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : GameplayUIElement
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private float percentMissingRestore = 100;

    private Health health = null;
    private int currentHealth = 0;

    protected override void OnPlayerEnterEventHandler(Player player)
    {
        base.OnPlayerEnterEventHandler(player);

        health = player.gameObject.GetComponent<Health>();
        health.TakeDamage(currentHealth <= 0 ? 0 : health.maxHealth - currentHealth);
        health.Heal((int)Math.Round((health.maxHealth - currentHealth) * percentMissingRestore / 100, MidpointRounding.AwayFromZero));
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
