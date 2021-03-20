﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    public static event Action<RoomActivator> OnAnyRoomComplete;
    public static event Action<RoomActivator> OnAnyRoomEntered;

    [HideInInspector] public Door[] doors = null;
    [HideInInspector] public bool closedWhenEntered = false;
    public event Action<Health> OnRoomEntered;
    public List<RoomActivator> adjacent = new List<RoomActivator>();

    [SerializeField] protected LevelExit exit = null;
    [SerializeField] protected Spawner spawner = null;

    protected bool roomComplete = false;
    protected const string playerTag = "Player";

    protected virtual void OnEnable()
    {
        spawner.OnAllEnemiesKilled += OnAllEnemiesKilledEventHandler;
    }

    private void OnAllEnemiesKilledEventHandler()
    {
        if (closedWhenEntered)
        {
            roomComplete = true;
            OnAnyRoomComplete?.Invoke(this);

            foreach (Door door in doors)
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

            if (!roomComplete && closedWhenEntered)
            {
                foreach (Door door in doors)
                {
                    door.Close();
                }

                OnAnyRoomEntered?.Invoke(this);
                OnRoomEntered?.Invoke(other.GetComponent<Health>());
            }
        }
    }

    protected virtual void OnDisable()
    {
        spawner.OnAllEnemiesKilled -= OnAllEnemiesKilledEventHandler;
    }
}
