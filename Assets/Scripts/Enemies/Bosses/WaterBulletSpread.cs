using UnityEngine;

public class WaterBulletSpread : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;
    [SerializeField] private float rotationSpeed = 0;
    [SerializeField] private float bulletSpeed = 1;

    private bool bulletVelocityUpdated = false;

    private void Update()
    {
        if (!bulletVelocityUpdated)
        {
            SetBulletVelocity();
        }

        transform.RotateAround(transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetBulletVelocity()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<Rigidbody2D>().velocity *= bulletSpeed;
            bullet.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        bulletVelocityUpdated = true;
    }
}
