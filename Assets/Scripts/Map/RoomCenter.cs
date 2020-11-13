using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;
    [HideInInspector] public Room theRoom;

    private const string playerTag = "Player";

    void Start()
    {
        if (openWhenEnemiesCleared)
        {
            theRoom.closedWhenEntered = true;
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
