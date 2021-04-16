using UnityEngine;

public class MapCollider : MonoBehaviour
{
    [SerializeField] private MapChest chest = null;

    private void OnMouseDown()
    {
        if (chest.orbMenuIsOpen && !chest.mouseIsOver)
        {
            chest.ToggleOrbMenu();
        }
    }
}
