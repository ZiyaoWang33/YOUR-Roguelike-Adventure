using UnityEngine;

public class LavaBulletCone : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
