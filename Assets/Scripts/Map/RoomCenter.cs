using System;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public RoomActivator activator = null;
    public bool openWhenEnemiesCleared = false;
    [HideInInspector] public Room theRoom;

    private const string playerTag = "Player";

    private void Awake()
    {
        if (openWhenEnemiesCleared)
        {
            activator.closedWhenEntered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }
}
