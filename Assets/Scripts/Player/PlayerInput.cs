using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 movement = Vector2.zero;
    [HideInInspector] public Vector3 mousePos = Vector3.zero;
    [HideInInspector] public bool attack = false;

    [SerializeField] private float smoothRate = 1;
    [SerializeField] private Camera cam = null;

    private void Update()
    {
        movement.x = Mathf.SmoothStep(movement.x, Input.GetAxisRaw("Horizontal"), smoothRate);
        movement.y = Mathf.SmoothStep(movement.x, Input.GetAxisRaw("Vertical"), smoothRate);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attack = Input.GetMouseButton(0);
    }
}
