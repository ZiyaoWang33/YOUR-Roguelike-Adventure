using System.Collections;
using UnityEngine;

public class WaterExplosion : MonoBehaviour
{
    [SerializeField] private int damage = 0;
    [SerializeField] private float lifeTime = 0;
    [SerializeField] private float sizeMultiplier = 1;

    private const string playerTag = "Player";

    private void Awake()
    {
        transform.localScale = new Vector3(transform.localScale.x * sizeMultiplier, transform.localScale.y * sizeMultiplier, transform.localScale.z);
        StartCoroutine(Explode());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
