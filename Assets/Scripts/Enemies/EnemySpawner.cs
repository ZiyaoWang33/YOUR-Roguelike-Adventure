using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] protected GameObject enemyToAdd = null;
    [SerializeField] protected int amountToChange = 0;
    [SerializeField] protected GameObject eliteToAdd = null;
    [SerializeField] protected Vector2 roomSize = Vector2.zero;

    protected int difficultyStage = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerPerformanceManager.OnPerformanceChange += OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered += OnAnyRoomEnteredEventHandler;
    }

    protected void AddEnemies(int amount)
    {
        int eliteAmount = difficultyStage - 1;

        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(i >= amount - eliteAmount ? eliteToAdd : enemyToAdd,
               transform.position + new Vector3(Random.Range(-1f, 1f) * roomSize.x / 2, Random.Range(-1f, 1f) * roomSize.y / 2), Quaternion.identity);
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
            if (difficultyStage > 0)
            {
                AddEnemies(amountToChange);
            }
            else if (difficultyStage < 0)
            {
                RemoveEnemies(amountToChange);
            }
        }
    }

    protected void OnPerformanceChangeEventHandler(int performanceValue)
    {
        difficultyStage = performanceValue;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPerformanceManager.OnPerformanceChange -= OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered -= OnAnyRoomEnteredEventHandler;
    }
}
