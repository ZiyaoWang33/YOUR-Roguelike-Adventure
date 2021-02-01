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
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
