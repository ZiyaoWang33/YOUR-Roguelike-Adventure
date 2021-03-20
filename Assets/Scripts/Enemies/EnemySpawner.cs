using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;
    [SerializeField] protected GameObject eliteToAdd = null;

    protected static int performanceValue = 0;
    protected static Dictionary<PlayerPerformanceManager.Performance, int> performanceIndex = new Dictionary<PlayerPerformanceManager.Performance, int>
    {
        { PlayerPerformanceManager.Performance.BAD, -1 }, { PlayerPerformanceManager.Performance.NEUTRAL, 0 }, {PlayerPerformanceManager.Performance.GOOD, 1 }
    };

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerPerformanceManager.OnPerformanceChange += OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered += OnAnyRoomEnteredEventHandler;
    }

    protected void AddEnemies(int amount, int stage)
    {
        int eliteAmount = stage - 1;
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(i >= amount - eliteAmount ? eliteToAdd : enemyToAdd,
               transform.position + new Vector3(Random.Range(-1f, 1f) * transform.localScale.x / 2, Random.Range(-1f, 1f) * transform.localScale.y / 2), Quaternion.identity);
            enemy.SetActive(false);
            enemyObjs.Add(enemy);
            enemies.Add(enemy, enemy.GetComponent<Enemy>());
            enemies[enemy].health.OnDeath += OnEnemyDeathEventHandler;
        }
    }

    protected void RemoveEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = enemyObjs[i];
            enemyObjs.Remove(enemy);
            enemies.Remove(enemy);
        }
    }

    protected void OnAnyRoomEnteredEventHandler(RoomActivator room)
    {
        if (activator.Equals(room))
        {
            if (performanceValue > 0)
            {
                AddEnemies(amountToChange, performanceValue);
            }
            else if (performanceValue < 0)
            {
                RemoveEnemies(amountToChange);
            }
        }
    }

    protected void OnPerformanceChangeEventHandler(PlayerPerformanceManager.Performance previous, PlayerPerformanceManager.Performance current, RoomActivator room)
    {
        if (activator.Equals(room))
        {
            Debug.Log("Last Performance: " + previous.ToString());
            Debug.Log("Current Performance: " + current.ToString());

            performanceValue += performanceIndex[current];
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPerformanceManager.OnPerformanceChange -= OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered -= OnAnyRoomEnteredEventHandler;
    }
}
