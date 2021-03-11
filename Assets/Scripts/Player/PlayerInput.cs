using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 movement = Vector2.zero;
    [HideInInspector] public Vector3 mousePos = Vector3.zero;
    [HideInInspector] public bool attack = false;

    public bool sliding = false; // Player is sliding uncontrollably on ice

    [SerializeField] private float smoothRate = 1;
    [SerializeField] private Camera cam = null;

    private void Update()
    {
        if (!sliding)
        {
            movement.x = InputManager.Instance.GetAxis("Horizontal");
            movement.y = InputManager.Instance.GetAxis("Vertical");
        }    
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attack = InputManager.Instance.GetButton("Shoot");
    }
}
