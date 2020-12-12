using UnityEngine;

public class Root : MonoBehaviour
{
    [HideInInspector] public Player player = null;

    [SerializeField][Range(0, 1)] private float slowingEffect = 0;

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(player.gameObject) && player.speedMultiplier >= 1)
        {
            player.speedMultiplier -= slowingEffect;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(player.gameObject))
        {
            player.speedMultiplier = 1;
        }
    }
}
