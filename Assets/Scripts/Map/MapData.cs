using UnityEngine;

public class MapData : MonoBehaviour
{
    public static int currentLevel = 0;

    [SerializeField] private MapUI ui = null;
    [SerializeField] private MapSlot[] slots = null;

    private void Update()
    {
        CheckSlotBeforeProceeding();
    }


    public void LockEntities()
    {
        foreach (MapSlot slot in slots)
        {
            if (slot.entity)
            {
                slot.entity.LockEntity();
            }
        }
    }

    private void CheckSlotBeforeProceeding()
    {   
        if (ui.loaded)
            ui.gameObject.SetActive(slots[currentLevel].entity);
    }
}
