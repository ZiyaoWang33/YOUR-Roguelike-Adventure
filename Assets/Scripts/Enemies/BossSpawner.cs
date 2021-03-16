using UnityEngine;
using System.Collections.Generic;

public class BossSpawner : Spawner
{
    // Provides that the Boss sets in the container object should follow this order.
    private Dictionary<string, int> indexes = new Dictionary<string, int>() { { "fire", 0 }, { "water", 1 }, { "wood", 2 } };

    // Set up each boss set in the room prefab so that: 1) each element is in a set, 2) each set has 3 difficult variants in order from easiest to hardest.
    private Dictionary<PlayerPerformanceManager.Performance, Enemy> bossChanges = null;

    protected override void Awake()
    {
        base.Awake();

        Enemy[] enemiesToAdd = new Enemy[enemies.Count];
        enemies.Values.CopyTo(enemiesToAdd, 0);

        bossChanges = new Dictionary<PlayerPerformanceManager.Performance, Enemy>() 
        { { PlayerPerformanceManager.Performance.BAD, enemiesToAdd[0] }, { PlayerPerformanceManager.Performance.NEUTRAL, enemiesToAdd[1] }, { PlayerPerformanceManager.Performance.GOOD, enemiesToAdd[2] } };

        for (int i = 0; i < enemyObjs.Count; i++)
        {
            GameObject enemy = enemyObjs[i];
            enemyObjs.Remove(enemy);
            enemies.Remove(enemy);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerPerformanceManager.OnFinalPerformancChange += OnFinalPerformanceChangeEventHandler;
    }

    protected override int SetIndexToUse(int end)
    {
        return indexes[MapData.currentElement];
    }

    protected void OnFinalPerformanceChangeEventHandler(PlayerPerformanceManager.Performance final)
    {
        GameObject currentBoss = enemyObjs[0];
        Enemy nextBoss = bossChanges[final];
        enemyObjs.Remove(currentBoss);
        enemies.Remove(currentBoss);
        enemyObjs.Add(nextBoss.gameObject);
        enemies.Add(nextBoss.gameObject, nextBoss);
        Debug.Log("Final Performance: " + final.ToString());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerPerformanceManager.OnFinalPerformancChange -= OnFinalPerformanceChangeEventHandler;
    }
}
