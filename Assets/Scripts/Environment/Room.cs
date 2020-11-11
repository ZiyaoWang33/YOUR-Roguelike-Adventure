using System.Diagnostics.Tracing;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public bool closedWhenEntered = true;

    [SerializeField] private Door[] doors = null;
    [SerializeField] private Enemy[] enemies = null;

    private bool active = false;
    private int deathCount = 0;
    private bool completed = false;

    private const string playerTag = "Player";

    private void OnEnable()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDeath += OnEnemyDeathEventHandler;
        }
    }

    private void OnEnemyDeathEventHandler()
    {
        if (active)
        {
            deathCount++;
        }

        if (deathCount >= enemies.Length)
        {
            foreach(Door door in doors)
            {
                door.Open();
                completed = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            CameraController.instance.ChangeTarget(transform);
            active = true;

            if (!completed)
            {
                foreach (Door door in doors)
                {
                    door.Close();
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
        foreach (Enemy enemy in enemies)
        {
            enemy.OnDeath -= OnEnemyDeathEventHandler;
        }
    }
}