using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;

    protected List<RoomActivator> possibleRooms = null;
    protected PlayerPerformanceManager.Performance current = PlayerPerformanceManager.Performance.NEUTRAL;

    protected void AddEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(enemyToAdd, transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f) * transform.localScale.x / 2, UnityEngine.Random.Range(-1f, 1f) * transform.localScale.y / 2), Quaternion.identity);
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

    protected override void OnAnyRoomEnteredEventHandler(RoomActivator room)
    {
        if (activator.Equals(room) && possibleRooms != null && possibleRooms.Contains(room))
        {
            if (current.Equals(PlayerPerformanceManager.Performance.GOOD))
            {
                AddEnemies(amountToChange);
            }
            else if (current.Equals(PlayerPerformanceManager.Performance.BAD))
            {
                RemoveEnemies(amountToChange);
            }
        }
    }

    protected override void OnPerformanceChangeEventHandler(PlayerPerformanceManager.Performance previous, PlayerPerformanceManager.Performance current, RoomActivator room)
    {
        Debug.Log("Last Performance: " + previous.ToString());
        Debug.Log("Current Performance: " + current.ToString());
        possibleRooms = room.adjacent;
        this.current = current;
    }
}
