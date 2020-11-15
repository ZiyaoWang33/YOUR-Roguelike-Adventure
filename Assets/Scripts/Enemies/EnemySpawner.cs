using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RoomActivator activator = null;

    private void OnEnable()
    {
        activator.OnRoomEntered += OnRoomEnteredEventHandler;
    }

    private void OnRoomEnteredEventHandler()
    {
        foreach (Enemy enemy in activator.enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.AnimateSpawn();
        }
    }

    private void OnDisable()
    {
        activator.OnRoomEntered -= OnRoomEnteredEventHandler;
    }
}
