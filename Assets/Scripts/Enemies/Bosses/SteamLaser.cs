using UnityEngine;

public class SteamLaser : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;
    [HideInInspector] public float sizeMultiplier = 1;

    private void Awake()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<SteamBullet>().sizeMultiplier = sizeMultiplier;
        }
    }

    void Update()
    {
        int collidedBullets = 0;
        foreach (GameObject bullet in bullets)
        {
            collidedBullets += bullet == null ? 1 : 0;
        }
        if (collidedBullets == bullets.Length)
        {
            Destroy(gameObject);
        }
    }
}
