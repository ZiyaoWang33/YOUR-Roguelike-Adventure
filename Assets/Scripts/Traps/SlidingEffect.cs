using UnityEngine;

public class SlidingEffect : Debuff
{
    private Rigidbody2D rb = null;
    private PlayerInput input = null;
    private float acceleration = 1;
    private float speedLimit = 1;

    // private const float unitDistance = 0.13f; // Approximate length of a tile

    public void SetStats(float acceleration, float speedLimit, bool lockControls = false)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        input = gameObject.GetComponent<PlayerInput>();
        this.acceleration = acceleration;
        this.speedLimit = speedLimit;

        // lifeTime = distance * unitDistance / (player.GetDirection().magnitude * player.speedMultiplier); Constant distance formula
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        bool playerIsSliding = gameObject.GetComponent<PlayerInput>().sliding;
        rb.velocity = playerIsSliding ? playerVelocity : (Vector2)(player.GetDirection() * player.speedMultiplier);
        input.sliding = lockControls;
    }

    protected override void Update()
    {
        Vector2 playerVelocity = rb.velocity;
        lifetime -= Time.deltaTime;
        if (playerVelocity.magnitude * acceleration <= speedLimit)
        {
            rb.velocity *= acceleration;
        }

        if (lifetime < 0 || playerVelocity == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
            input.sliding = false;
            Destroy(this);
        }
    }
}
