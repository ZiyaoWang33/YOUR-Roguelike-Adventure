using UnityEngine;

public class WaterBulletCone : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    [HideInInspector] public string mode = null;
    [HideInInspector] public float speed = 1;

    private WaterBullet waterBullet = null;
    private bool modeApplied = false;

    private void Awake()
    {
        int index = Random.Range(0, bullets.Length);
        waterBullet = bullets[index].GetComponent<WaterBullet>();
        waterBullet.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    private void Update()
    {
        if (!modeApplied)
        {
            foreach (GameObject bullet in bullets)
            {
                bullet.GetComponent<Rigidbody2D>().velocity *= speed;
            }
            SetMode();
        }

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetMode()
    {
        waterBullet.explode = mode == "explode" ? true : false;
        waterBullet.spread = mode == "spread" ? true : false;      
        modeApplied = true;
    }
}
