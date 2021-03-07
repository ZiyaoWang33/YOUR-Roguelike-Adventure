using UnityEngine;
using System;

public class PlayerPerformanceManager : MonoBehaviour
{
    public static event Action<Performance, Performance, RoomActivator> OnPerformanceChange; // previous, current
    public static event Action<Performance> OnFinalPerformancChange;
    public enum Performance { GOOD, NEUTRAL, BAD };

    [SerializeField] private int damageThreshold = 0;

    private Performance current = Performance.NEUTRAL;
    private int healthLost = 0;
    private int currentHealth = 0;
    private Health health = null;
    private bool addLostHealth = false;

    private void OnEnable()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
        RoomActivator.OnAnyRoomComplete += OnAnyRoomCompleteEventHandler;
    }

    private void OnPlayerEnterEventHandler(Player player)
    {
        health = player.GetComponent<Health>();
        currentHealth = health.health;
        health.OnDamageTaken += OnDamageTakenEventHandler;
    }

    private void OnAnyRoomCompleteEventHandler(RoomActivator room)
    {
        Performance previous = current;
        if (currentHealth < damageThreshold)
        {
            current = Performance.NEUTRAL;
        }
        else if (healthLost > damageThreshold)
        {
            current = Performance.BAD;
        }    
        else if (healthLost == 0)
        {
            current = Performance.GOOD;
        }
        else
        {
            current = Performance.NEUTRAL;
        }

        addLostHealth = false;
        OnPerformanceChange?.Invoke(previous, current, room);
    }

    private void OnDamageTakenEventHandler()
    {
        if (addLostHealth)
        {
            healthLost = healthLost + (currentHealth - health.health);
        }
        else
        {
            healthLost = currentHealth - health.health;
            addLostHealth = true;
        }
        currentHealth = health.health;
    }

    private void OnDisable()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
    }
}
