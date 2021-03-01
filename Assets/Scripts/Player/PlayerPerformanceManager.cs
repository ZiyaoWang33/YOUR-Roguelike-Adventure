using UnityEngine;
using System;

public class PlayerPerformanceManager : MonoBehaviour
{
    public static event Action<Performance, Performance> OnPerformanceChange; // previous, current
    public enum Performance { GOOD, NEUTRAL, BAD };

    [SerializeField] private float timeThreshold = 0;
    [SerializeField] private int damageThreshold = 0;

    private Performance current = Performance.NEUTRAL;
    private float timeToComplete = 0;
    private int healthLost = 0;
    private int currentHealth = 0;
    private Health health = null;
    private bool startTimer = false;

    private void OnEnable()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
        RoomActivator.OnAnyRoomComplete += OnAnyRoomCompleteEventHandler;
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

    private void OnAnyRoomCompleteEventHandler(RoomActivator room)
    {
        Performance previous = current;
        if (timeToComplete > timeThreshold && healthLost > damageThreshold)
        {
            current = Performance.BAD;
        }    
        else if (timeToComplete < timeThreshold && healthLost == 0)
        {
            current = Performance.GOOD;
        }
        else
        {
            current = Performance.NEUTRAL;
        }

        OnPerformanceChange?.Invoke(previous, current);
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
