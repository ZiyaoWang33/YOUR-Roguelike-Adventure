using System.Collections;
using UnityEngine;

public class MapChest : MonoBehaviour
{
    [SerializeField] private GameObject orbMenu = null;
    [SerializeField] private int numberOfShakes = 3;
    [SerializeField] private float shakeAngle = 15;
    [SerializeField] private float timeBetweenShakes = 0.2f;
    [SerializeField] private float menuOpenBufferTime = 0.1f;

    [HideInInspector] public bool orbMenuIsOpen = false;
    [HideInInspector] public bool mouseIsOver = false;

    private bool openingChest = false;

    private void Update()
    {
        CheckMouseHover();
    }   

    private void CheckMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        mouseIsOver = RootIsChest(hit.collider?.transform);                  
    }

    // Returns TRUE if transform is a descendant of the object that this script is attached to
    // Returns FALSE if transform is null or there are no more ancestors to check
    private bool RootIsChest(Transform transform)
    {
        return transform != null && (transform.name == name || RootIsChest(transform.parent));
    }

    private void OnMouseDown()
    {
        if (!openingChest)
        {
            openingChest = true;
            StartCoroutine(ToggleChest());
        }
    }

    public void ToggleOrbMenu()
    {
        orbMenuIsOpen = !orbMenuIsOpen;
        orbMenu.SetActive(orbMenuIsOpen);
    }

    IEnumerator ToggleChest()
    {
        if (!orbMenuIsOpen)
        {
            // Open animation (1 shake = rotate by some angle + rotate back to initial state)
            for (int i = 0; i < numberOfShakes; ++i)
            {
                transform.Rotate(Vector3.forward * shakeAngle);
                yield return new WaitForSeconds(timeBetweenShakes / 2);
                transform.Rotate(Vector3.back * shakeAngle);
                yield return new WaitForSeconds(timeBetweenShakes / 2);
                shakeAngle *= -1;
            }
        }

        yield return new WaitForSeconds(menuOpenBufferTime);
        ToggleOrbMenu();
        openingChest = false;
    }
}
