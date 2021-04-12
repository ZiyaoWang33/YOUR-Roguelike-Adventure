using UnityEngine;

public class MapChest : MonoBehaviour
{
    [SerializeField] private GameObject orbMenu = null;

    private bool orbMenuIsOpen = false;

    private void OpenAnimation()
    {
        // Shake treasure chest
    }

    private void OnMouseDown()
    {
        if (!orbMenuIsOpen)
            OpenAnimation();

        orbMenuIsOpen = !orbMenuIsOpen;
        orbMenu.SetActive(orbMenuIsOpen);
    }
}
