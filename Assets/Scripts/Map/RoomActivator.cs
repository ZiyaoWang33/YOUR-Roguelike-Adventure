using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    [HideInInspector] public Door[] doors = null;
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    [HideInInspector] public bool closedWhenEntered = false;
    public event Action OnRoomEntered;

    [SerializeField] private GameObject enemyContainer = null;

    private bool active = false;
    private int deathCount = 0;

    private const string playerTag = "Player";

    private void Awake()
    {
        List<GameObject> enemySets = new List<GameObject>();
        AddChildren(enemyContainer, enemySets);

        // Avoids error caused by starting rooms having no enemies
        GameObject setToUse = null;

        if (enemyContainer != null)
        {
            setToUse = enemySets[UnityEngine.Random.Range(0, enemySets.Count)];
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

    private void AddChildren(GameObject obj, List<GameObject> listToGrow)
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

    private void OnEnable()
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            active = false;
        }
    }

    private void OnDisable()
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
