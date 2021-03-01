using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;

    protected override void OnPerformanceChangeEventHandler(PlayerPerformanceManager.Performance previous, PlayerPerformanceManager.Performance current)
    {
        previousEnemyChange = previous;
        print(current);

        if (current == PlayerPerformanceManager.Performance.GOOD)
        {
            AddEnemies(amountToChange);
        }
        else if (current == PlayerPerformanceManager.Performance.BAD)
        {
            RemoveEnemies(amountToChange);
        }
    }

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

    protected override void OnAnyRoomCompleteEventHandler(RoomActivator room)
    {
        if (room != activator)
        {
            if (previousEnemyChange == PlayerPerformanceManager.Performance.GOOD)
            {
                RemoveEnemies(amountToChange);
            }
            else if (previousEnemyChange == PlayerPerformanceManager.Performance.BAD)
            {
                AddEnemies(amountToChange);
            }
        }
    }
}
