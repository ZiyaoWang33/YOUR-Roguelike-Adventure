using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Spawner : MonoBehaviour
{
    public event Action OnAllEnemiesKilled;

    [HideInInspector] protected List<GameObject> enemyObjs = new List<GameObject>();
    protected Dictionary<GameObject, Enemy> enemies = new Dictionary<GameObject, Enemy>();
    [SerializeField] protected GameObject enemyContainer = null;
    [SerializeField] protected RoomActivator activator = null;

    protected int deathCount = 0;

    protected virtual void Awake()
    {
        List<GameObject> enemySets = new List<GameObject>();
        AddSet(enemyContainer, enemySets);

        // Avoids error caused by starting rooms having no enemies
        GameObject setToUse = null;
        int indexToUse = SetIndexToUse(enemySets.Count);

        if (enemyContainer != null)
        {
            setToUse = enemySets[indexToUse];
        }

        List<GameObject> enemyObjects = new List<GameObject>();
        AddSet(setToUse, enemyObjects);

        foreach (GameObject enemy in enemyObjects)
        {
            enemyObjs.Add(enemy);
            enemies.Add(enemy, enemy.GetComponent<Enemy>());
        }

        foreach (GameObject enemySet in enemySets)
        {
            if (enemySet != setToUse)
            {
                Destroy(enemySet);
            }
        }
    }

    protected virtual int SetIndexToUse(int end)
    {
        return UnityEngine.Random.Range(0, end);
    }

    protected void AddSet(GameObject enemySet, List<GameObject> enemyList)
    {
        if (null == enemies)
        {
            return;
        }

        foreach (Transform enemy in enemySet.transform)
        {
            if (enemy == null)
            {
                continue;
            }

            enemyList.Add(enemy.gameObject);
        }
    }

    protected virtual void OnEnable()
    {
        activator.OnRoomEntered += OnRoomEnteredEventHandler;

        foreach (GameObject enemy in enemyObjs)
        {
            enemies[enemy].OnDeath += OnEnemyDeathEventHandler;
        }
    }

    protected void OnEnemyDeathEventHandler()
    {
        deathCount++;

        if (deathCount >= enemies.Count)
        {
            OnAllEnemiesKilled?.Invoke();
            gameObject.SetActive(false);
        }
    }

    protected void OnRoomEnteredEventHandler(Health player)
    {
        foreach (GameObject enemy in enemyObjs)
        {
            enemies[enemy].player = player;
            enemy.SetActive(true);
            enemies[enemy].AnimateSpawn();
        }
    }

    protected virtual void OnDisable()
    {
        activator.OnRoomEntered -= OnRoomEnteredEventHandler;

        foreach (GameObject enemy in enemyObjs)
        {
            enemies[enemy].OnDeath -= OnEnemyDeathEventHandler;
        }
    }
}
