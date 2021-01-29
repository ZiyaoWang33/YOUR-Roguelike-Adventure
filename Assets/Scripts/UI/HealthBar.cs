using UnityEngine;
using UnityEngine.UI;

public class HealthBar : GameplayUIElement
{
    [SerializeField] private Slider slider = null;

    private Health health = null;

    protected override void OnPlayerEnterEventHandler(Player player)
    {
        base.OnPlayerEnterEventHandler(player);

        health = player.gameObject.GetComponent<Health>();
        health.OnDamageTaken += OnDamageTakenEventHandler;

        slider.maxValue = health.health;
        slider.value = slider.maxValue;
    }

    private void OnDamageTakenEventHandler()
    {
        slider.value = health.health;
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
