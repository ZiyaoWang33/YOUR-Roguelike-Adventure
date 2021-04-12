using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public string element;  // Manually set element in inspector
    private bool locked = false;

    [SerializeField] private MapSlot[] slots = null;
    [SerializeField] private GameObject entityHighlight = null;

    public void SlotEntity()
    {
        entityHighlight.SetActive(true);
        entityHighlight.transform.position = transform.position;
    }

    public void DeslotEntity()
    {
        entityHighlight.SetActive(false);
    }

    public void LockEntity()
    {
        locked = true;
        GetComponent<SpriteRenderer>().enabled = true;
        DeslotEntity();
    }

    private void OnMouseDown()
    {
        if (!locked)
        {
            Debug.Log(gameObject.name + " placed into " + slots[MapData.currentLevel].gameObject.name);
            slots[MapData.currentLevel].ChooseEntity(this);            
        }
    }   
}
