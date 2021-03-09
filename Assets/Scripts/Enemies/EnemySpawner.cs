using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;
    [SerializeField] protected GameObject eliteToAdd = null;

    protected List<RoomActivator> possibleRooms = null;
    protected PlayerPerformanceManager.Performance current = PlayerPerformanceManager.Performance.NEUTRAL;
    protected Dictionary<PlayerPerformanceManager.Performance, int> previousPerformances = new Dictionary<PlayerPerformanceManager.Performance, int>();

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerPerformanceManager.OnPerformanceChange += OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered += OnAnyRoomEnteredEventHandler;
    }

    protected void AddEnemies(int amount, int stage)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(i == amount - 1 && stage > 1 ? eliteToAdd : enemyToAdd,
                transform.position + new Vector3(Random.Range(-1f, 1f) * transform.localScale.x / 2, Random.Range(-1f, 1f) * transform.localScale.y / 2), Quaternion.identity);
            enemy.SetActive(false);
            enemyObjs.Add(enemy);
            this.enemies.Add(enemy, enemy.GetComponent<Enemy>());
            this.enemies[enemy].OnDeath += OnEnemyDeathEventHandler;
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
        if (activator.Equals(room) && possibleRooms != null && possibleRooms.Contains(room))
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
        Debug.Log("Last Performance: " + previous.ToString());
        Debug.Log("Current Performance: " + current.ToString());
        possibleRooms = room.adjacent;
        this.current = current;
        
        if (this.previousPerformances.TryGetValue(previous, out int count))
        {
            this.previousPerformances[previous] = 1 + count;
        }
        else
        {
            this.previousPerformances[previous] = 1;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPerformanceManager.OnPerformanceChange -= OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered -= OnAnyRoomEnteredEventHandler;
    }
}
