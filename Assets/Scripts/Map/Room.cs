using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] doors = null;

    private const string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag)
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }
}