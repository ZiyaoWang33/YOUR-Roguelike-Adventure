using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;
    [SerializeField] protected GameObject eliteToAdd = null;

    // protected List<RoomActivator> possibleRooms = null;
    protected static PlayerPerformanceManager.Performance current = PlayerPerformanceManager.Performance.NEUTRAL;
    protected static Dictionary<PlayerPerformanceManager.Performance, int> previousPerformances = new Dictionary<PlayerPerformanceManager.Performance, int>();

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerPerformanceManager.OnPerformanceChange += OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered += OnAnyRoomEnteredEventHandler;
    }

    protected void AddEnemies(int amount, int stage)
    {
        print(stage);
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(i == amount - 1 && stage > 2 ? eliteToAdd : enemyToAdd,
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
            if (current.Equals(PlayerPerformanceManager.Performance.GOOD))
            {
                AddEnemies(amountToChange, previousPerformances[current]);
            }
            else if (current.Equals(PlayerPerformanceManager.Performance.BAD))
            {
                RemoveEnemies(amountToChange);
            }
        }
    }

    protected void OnPerformanceChangeEventHandler(PlayerPerformanceManager.Performance previous, PlayerPerformanceManager.Performance current, RoomActivator room)
    {
        if (room == activator)
        {
            Debug.Log("Last Performance: " + previous.ToString());
            Debug.Log("Current Performance: " + current.ToString());
            EnemySpawner.current = current;

            if (previousPerformances.TryGetValue(previous, out int count))
            {
                previousPerformances[previous] = 1 + count;
            }
            else
            {
                previousPerformances[previous] = 1;
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPerformanceManager.OnPerformanceChange -= OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered -= OnAnyRoomEnteredEventHandler;
    }
}
