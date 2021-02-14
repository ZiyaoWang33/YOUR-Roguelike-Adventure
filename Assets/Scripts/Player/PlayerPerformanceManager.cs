using UnityEngine;

public class PlayerPerformanceManager : MonoBehaviour
{
    [HideInInspector] public float timeToComplete = 0;
    [HideInInspector] public float healthLost = 0;

    private int currentHealth = 0;
    private Health health = null;
    private bool startTimer = false;

    private void OnEnable()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
    }

    private void OnPlayerEnterEventHandler(Player player)
    {
        player.OnPlayerExit += OnPlayerExitEventHandler;

        health = player.GetComponent<Health>();
        currentHealth = health.health;
        health.OnDamageTaken += OnDamageTakenEventHandler;

        startTimer = true;
    }

    private void OnPlayerExitEventHandler()
    {
        startTimer = false;
    }

    private void OnDamageTakenEventHandler()
    {
        healthLost = currentHealth - health.health;
        currentHealth = health.health;
    }

    private void Update()
    {
        if (startTimer)
        {
            timeToComplete += Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
    }
}
