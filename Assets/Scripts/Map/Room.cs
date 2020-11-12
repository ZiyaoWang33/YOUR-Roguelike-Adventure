using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public bool closedWhenEntered = true;

    [SerializeField] private RoomActivator activator = null;

    private const string playerTag = "Player";

    private void Awake()
    {
        if (closedWhenEntered)
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