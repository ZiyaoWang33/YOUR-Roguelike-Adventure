using UnityEngine;
using System.Collections.Generic;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private RoomActivator activator = null;
    [SerializeField] private GameObject trapToAdd = null;
    [SerializeField] private Vector2 roomSize = Vector2.zero;

    private int amountToAdd = 0;
    private List<GameObject> currentTraps = new List<GameObject>();

    private void OnEnable()
    {
        PlayerPerformanceManager.OnPerformanceChange += OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered += OnAnyRoomEnteredEventHandler;
    }

    private void AddTraps()
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            GameObject newTrap = Instantiate(trapToAdd, transform.position + new Vector3(Random.Range(-1f, 1f) * roomSize.x / 2, Random.Range(-1f, 1f) * roomSize.y / 2, -1), Quaternion.identity);
            newTrap.SetActive(false);
            currentTraps.Add(newTrap);
        }
    }

    private void RemoveTraps()
    {
        currentTraps.RemoveRange(0, amountToAdd);
    }

    private void OnPerformanceChangeEventHandler(int performanceValue)
    {
        amountToAdd = performanceValue;
    }

    private void OnAnyRoomEnteredEventHandler(RoomActivator room)
    {
        if (activator.Equals(room))
        {
            if (amountToAdd > 0)
            {
                AddTraps();
            }
            else if (amountToAdd < 0)
            {
                RemoveTraps();
            }
        }

        foreach (GameObject trap in currentTraps)
        {
            trap.SetActive(true);
        }
    }

    private void OnDisable()
    {
        PlayerPerformanceManager.OnPerformanceChange -= OnPerformanceChangeEventHandler;
        RoomActivator.OnAnyRoomEntered -= OnAnyRoomEnteredEventHandler;
    }
}
