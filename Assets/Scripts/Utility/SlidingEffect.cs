using UnityEngine;

public class SlidingEffect : MonoBehaviour
{
    private Player player = null; 
    private float lifeTime = 0;

    private const float unitDistance = 0.13f; // Approximate length of a tile

    public void SetStats(float distance, bool lockControls = false)
    {
        player = gameObject.GetComponent<Player>();
        lifeTime = distance * unitDistance / (player.GetDirection().magnitude * player.speedMultiplier);

        gameObject.GetComponent<PlayerInput>().sliding = lockControls;
        gameObject.GetComponent<Rigidbody2D>().velocity = player.GetDirection() * player.speedMultiplier;
    }
   
    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0 || player.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<PlayerInput>().sliding = false;
            Destroy(this);
        }
    }
}
