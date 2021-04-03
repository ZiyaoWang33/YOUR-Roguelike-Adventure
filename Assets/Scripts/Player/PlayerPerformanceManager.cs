using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerPerformanceManager : MonoBehaviour
{
    public static event Action<int> OnPerformanceChange;
    public static event Action<Performance> OnFinalPerformancChange;
    public enum Performance { GOOD, NEUTRAL, BAD };

    [SerializeField] private int damageThreshold = 0; 
    [SerializeField] private int healthGoal = 0;
    // Health metrics described as the ratio of current health to the previous current health

    private int performanceValue = 0;
    private static Dictionary<Performance, int> performanceIndex = new Dictionary<Performance, int>
    {
        { Performance.BAD, -1 }, { Performance.NEUTRAL, 0 }, {Performance.GOOD, 1 }
    };
    private Performance current = Performance.NEUTRAL;
    private int healthLost = 0;
    private float percentLoss = 0;
    private int currentHealth = 0;
    private int previousCurrentHealth = 0;
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
        currentHealth = health.maxHealth;
        health.OnDamageTaken += OnDamageTakenEventHandler;
        ResetPerformance();
    }

    private void OnAnyRoomCompleteEventHandler(RoomActivator room)
    {
        Performance previous = current;
        if (percentLoss >= damageThreshold)
        {
            current = Performance.BAD;
        }
        else if (percentLoss <= healthGoal)
        {
            current = Performance.GOOD;
        }    
        else
        {
            current = Performance.NEUTRAL;
        }
        addLostHealth = false;

        performanceValue += performanceIndex[current];
        OnPerformanceChange?.Invoke(performanceValue);
        Debug.Log("Last Performance: " + previous.ToString());
        Debug.Log("Current Performance: " + current.ToString());

        Performance final = Performance.NEUTRAL;
        if (currentHealth >= damageThreshold)
        {
            final = Performance.GOOD;
        }
        else if (currentHealth <= healthGoal)
        {
            final = Performance.BAD;
        }
        OnFinalPerformancChange?.Invoke(final);
    }

    private void OnDamageTakenEventHandler()
    {
        if (addLostHealth)
        {
            healthLost += (currentHealth - health.health);
            percentLoss = (float)healthLost / previousCurrentHealth * 100;
        }
        else
        {
            healthLost = currentHealth - health.health;
            previousCurrentHealth = currentHealth;
            addLostHealth = true;
        }
        currentHealth = health.health;
    }

    private void ResetPerformance()
    {
        performanceValue = 0;
        current = Performance.NEUTRAL;
        healthLost = 0;
        percentLoss = 0;
        previousCurrentHealth = 0;
        addLostHealth = false;
        Debug.Log("Successfully Resetted Performance");
    }

    private void OnDisable()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        RoomActivator.OnAnyRoomComplete -= OnAnyRoomCompleteEventHandler;
        health.OnDamageTaken -= OnDamageTakenEventHandler;
    }
}
