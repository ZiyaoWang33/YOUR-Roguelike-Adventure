using UnityEngine;

public class GrassBulletBranches : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    private GrassBullet grassBullet = null;

    private void Awake()
    {

    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
