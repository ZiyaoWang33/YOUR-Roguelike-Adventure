using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    [HideInInspector] public Door[] doors = null;
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    [HideInInspector] public bool closedWhenEntered = false;
    public event Action OnRoomEntered;

    [SerializeField] private LevelExit exit = null;

    [SerializeField] protected GameObject enemyContainer = null;

    protected List<GameObject> enemySets = new List<GameObject>();
    protected bool active = false;
    protected int deathCount = 0;

    protected const string playerTag = "Player";

    protected virtual void Awake()
    {
        AddChildren(enemyContainer, enemySets);

        // Avoids error caused by starting rooms having no enemies
        GameObject setToUse = null;
        int indexToUse = SetIndexToUse();

        if (enemyContainer != null)
        {
            setToUse = enemySets[indexToUse];
        }

        List<GameObject> enemyObjects = new List<GameObject>();
        AddChildren(setToUse, enemyObjects);

        foreach (GameObject enemy in enemyObjects)
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }

        foreach (GameObject enemySet in enemySets)
        {
            if (enemySet != setToUse)
            {
                Destroy(enemySet);
            }
        }
    }

    protected virtual int SetIndexToUse()
    {
        return UnityEngine.Random.Range(0, enemySets.Count);
    }

    protected void AddChildren(GameObject obj, List<GameObject> listToGrow)
    {
        if (null == obj) 
        { 
            return; 
        }

        foreach (Transform child in obj.transform)
        {
            if (child == null) 
            { 
                continue;
            }

            listToGrow.Add(child.gameObject);
        }
    }

    protected virtual void OnEnable()
    {
        if (closedWhenEntered)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.OnDeath += OnEnemyDeathEventHandler;
            }
        }
    }

    private void OnEnemyDeathEventHandler()
    {
        if (active)
        {
            deathCount++;
        }

        if (deathCount >= enemies.Count)
        {
            foreach(Door door in doors)
            {
                door.Open();
            }

            if (exit)
            {
                exit.active = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            CameraController.instance.ChangeTarget(transform);
            active = true;

            if (deathCount < enemies.Count)
            {
                OnRoomEntered?.Invoke();

                foreach (Door door in doors)
                {
                    door.Close();
                }

                foreach (Enemy enemy in enemies)
                {
                    enemy.player = other.GetComponent<Health>();
                }
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            active = false;
        }
    }

    protected virtual void OnDisable()
    {
        if (closedWhenEntered)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.OnDeath -= OnEnemyDeathEventHandler;
            }
        }
    }
}
