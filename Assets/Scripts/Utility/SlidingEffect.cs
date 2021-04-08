using UnityEngine;

public class SlidingEffect : MonoBehaviour
{
    private Player player = null;
    private float acceleration = 1;
    private float lifeTime = 0;
    private float speedLimit = 1;

    private const float unitDistance = 0.13f; // Approximate length of a tile

    public void SetStats(float acceleration, float lifeTime, float speedLimit, bool lockControls = false)
    {
        player = gameObject.GetComponent<Player>();        
        this.acceleration = acceleration;
        this.lifeTime = lifeTime;
        this.speedLimit = speedLimit;

        // lifeTime = distance * unitDistance / (player.GetDirection().magnitude * player.speedMultiplier); Constant distance formula
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        bool playerIsSliding = gameObject.GetComponent<PlayerInput>().sliding;
        gameObject.GetComponent<Rigidbody2D>().velocity = playerIsSliding ? playerVelocity : (Vector2)(player.GetDirection() * player.speedMultiplier);
        gameObject.GetComponent<PlayerInput>().sliding = lockControls;
    }

    private void Update()
    {
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        lifeTime -= Time.deltaTime;
        if (playerVelocity.magnitude * acceleration <= speedLimit)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity *= acceleration;
        }

        if (lifeTime < 0 || playerVelocity == Vector2.zero)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<PlayerInput>().sliding = false;
            Destroy(this);
        }
    }
}
