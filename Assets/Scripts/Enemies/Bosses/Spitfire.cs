using UnityEngine;

public class Spitfire : MonoBehaviour
{
    [SerializeField] private float rotateAmount = 0;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotateAmount * Time.deltaTime);

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
