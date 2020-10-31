using UnityEngine;

public class MapEntity : MonoBehaviour
{   
    [SerializeField] private Camera cam = null;
    [SerializeField] private bool mouseOver = false;
    [SerializeField] private bool selected = false;
    [SerializeField] private float distFromCam = 1; // How far in front of the camera the object is

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z + distFromCam);
    }

    private void Update()
    {
        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            selected = !selected;
        }

        if (selected)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distFromCam));
        }
    }

    // Requires the object to have a collider
    private void OnMouseEnter() { mouseOver = true; }
    private void OnMouseExit() { mouseOver = false; }
}
