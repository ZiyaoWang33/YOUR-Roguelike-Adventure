using System;
using System.Collections;
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

    // Check and update on normal room clear to unlock boss room
    private int roomsToClear = 3;
    public RoomActivator endRoom = null;

    [SerializeField] protected LevelExit exit = null;
    [SerializeField] protected Spawner spawner = null; 

    protected bool roomComplete = false;
    protected const string playerTag = "Player";

    protected virtual void OnEnable()
    {
        spawner.OnAllEnemiesKilled += OnAllEnemiesKilledEventHandler;
        StartCoroutine(LockBossRoom(0.01f));
    }  

    private void OnAllEnemiesKilledEventHandler()
    {
        if (closedWhenEntered)
        {
            roomComplete = true;
            OnAnyRoomComplete?.Invoke(this);
            endRoom?.NormalRoomCleared();
            OpenDoors();

            if (exit)
            {
                exit.active = true;
            }
        }
    }

    public void NormalRoomCleared()
    {
        roomsToClear -= 1;
        if (roomsToClear == 0)      
            OpenDoors();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            CameraController.instance.ChangeTarget(transform);

            if (!roomComplete && closedWhenEntered)
            {
                CloseDoors();
                OnAnyRoomEntered?.Invoke(this);
                OnRoomEntered?.Invoke(other.GetComponent<Health>());
            }
        }
    }
 
    protected virtual void OnDisable()
    {
        spawner.OnAllEnemiesKilled -= OnAllEnemiesKilledEventHandler;
    }

    private void OpenDoors()
    {
        foreach (Door door in doors)
        {
            door.Open();
        }
    }

    private void CloseDoors()
    {
        foreach (Door door in doors)
        {
            door.Close();
        }
    }

    IEnumerator LockBossRoom(float time)
    {
        yield return new WaitForSeconds(time);
        if (endRoom == null)
            CloseDoors();
    }
}
