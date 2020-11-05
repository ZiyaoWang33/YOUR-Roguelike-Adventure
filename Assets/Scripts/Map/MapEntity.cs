using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public string element;  // Manually set element in inspector
    public bool selected = false;
    public bool slotted = false;
    public bool locked = false;

    [SerializeField] private Camera cam = null;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private bool mouseOver = false;
    [SerializeField] private float distanceFromCam; // How far in front of the camera the object is

    private void Start()
    {
        distanceFromCam = transform.position.z - cam.transform.position.z;
        startPos = new Vector3(transform.position.x, transform.position.y, distanceFromCam);
    }

    private void Update()
    {
        if (!locked)
        {
            // Right-click to return map monster to its initial spot
            if (Input.GetMouseButtonDown(1) && mouseOver)
            {
                selected = false;
                transform.position = startPos;
            }

            if (selected)
            {
                transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCam));
            }
        }
    }

    // Left-click to select/de-select map monster
    private void OnMouseDown()
    {
        selected = !selected;
    }

    private void OnMouseEnter() { mouseOver = true; }
    private void OnMouseExit() { mouseOver = false; }
}
