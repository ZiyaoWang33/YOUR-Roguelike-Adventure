using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public string element;  // Manually set element in inspector
    public bool selected = false;
    public bool slotted = false;
    public bool locked = false;
    [HideInInspector] public Vector3 lockedPos = Vector3.zero;

    [SerializeField] private Camera cam = null;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private bool mouseOver = false;
    [SerializeField] private float distanceFromCam; // How far in front of the camera the object is

    private void Start()
    {
        distanceFromCam = transform.position.z - cam.transform.position.z - 1;

        if (locked)
        {
            // Set sprite to (an explosion?) here
            startPos = lockedPos;
        }
        else
        {
            startPos = new Vector3(transform.position.x, transform.position.y, distanceFromCam);
        }
    }

    private void Update()
    {
        if (!locked)
        {
            // Right-click to return map monster to its initial spot
            if (Input.GetMouseButtonDown(1) && mouseOver)
            {
                ReturnToStart();
            }

            if (selected)
            {
                transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCam));
            }
        }
    }

    public void ReturnToStart()
    {
        selected = false;
        transform.position = startPos;
    }

    public void LockEntity()
    {
        locked = true;
        lockedPos = transform.position;
    }

    // Left-click to select/de-select map monster
    private void OnMouseDown()
    {
        selected = !selected;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
