using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 movement = Vector2.zero;
    [HideInInspector] public Vector3 mousePos = Vector3.zero;
    [HideInInspector] public bool attack = false;

    [SerializeField] private GameObject checkAbove, checkBelow, checkLeft, checkRight;
    [SerializeField] private float smoothRate = 1;
    [SerializeField] private Camera cam = null;

    private int groundLayer = 8; // Layer of wall tiles (or other solid terrain)
    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private void Update()
    {
        movement.x = InputManager.Instance.GetAxis("Horizontal");
        movement.y = InputManager.Instance.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attack = InputManager.Instance.GetButton("Shoot");

        PreventMovingIntoWalls();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == groundLayer)
        {
            CheckSurroundings(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == groundLayer)
        {
            CheckSurroundings(collision);
        }
    }

    public void PreventMovingIntoWalls()
    {
        movement.x = (movement.x > 0 && !canMoveRight) || (movement.x < 0 && !canMoveLeft) ? 0 : movement.x;
        movement.y = (movement.y > 0 && !canMoveUp) || (movement.y < 0 && !canMoveDown) ? 0 : movement.y;
    }

    public void CheckSurroundings(Collider2D collision)
    {
        canMoveUp = checkAbove.GetComponent<BoxCollider2D>().IsTouching(collision) ? false : true;
        canMoveDown = checkBelow.GetComponent<BoxCollider2D>().IsTouching(collision) ? false : true;
        canMoveLeft = checkLeft.GetComponent<BoxCollider2D>().IsTouching(collision) ? false : true;
        canMoveRight = checkRight.GetComponent<BoxCollider2D>().IsTouching(collision) ? false : true;
    }
}
