using UnityEngine;

public class GrassBullet : Bullet
{
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private Transform graphic = null;
    [SerializeField] private float turnSpeed = 0;

    private Vector3 originalPos = Vector3.zero;
 
    protected override void Awake()
    {
        base.Awake();
        originalPos = transform.position;
    }

    private void Update()
    {
        //transform.Rotate(Vector3.up * (turnSpeed * Time.deltaTime));
    }
}