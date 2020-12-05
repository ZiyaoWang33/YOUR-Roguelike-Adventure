using UnityEngine;

public class WaterBulletCone : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    private WaterBullet waterBullet = null;

    private void Awake()
    {
        int index = Random.Range(0, bullets.Length);
        waterBullet = bullets[index].GetComponent<WaterBullet>();
        waterBullet.explode = true;
        waterBullet.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
}
