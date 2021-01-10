using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private GameObject image = null;

    private Health health = null;

    private void Awake()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
    }

    private void OnPlayerEnterEventHandler(Player player)
    {
        image.SetActive(true);

        health = player.GetComponent<Health>();
        health.OnDamageTaken += OnDamageTakenEventHandler;
        player.OnPlayerExit += OnPlayerExitEventHandler;

        slider.maxValue = health.health;
        slider.value = slider.maxValue;
    }

    private void OnDamageTakenEventHandler()
    {
        slider.value = health.health;
    }

    private void OnPlayerExitEventHandler()
    {
        image.SetActive(false);
    }

    private void OnDestroy()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        health.OnDamageTaken -= OnDamageTakenEventHandler;
        health.gameObject.GetComponent<Player>().OnPlayerExit -= OnPlayerExitEventHandler;
    }
}
