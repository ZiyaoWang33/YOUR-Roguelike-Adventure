using UnityEngine;

public class MapData : MonoBehaviour
{
    public static int currentLevel = 0;
    public static string currentElement = string.Empty;

    [SerializeField] private MapUI ui = null;
    [SerializeField] private MapSlot[] slots = null; // Should correspond in the inspector to the order of progressing through the levels

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

        currentElement = slots[currentLevel].monsterElement;
    }

    private void CheckSlotBeforeProceeding()
    {   
        if (ui.loaded)
            ui.gameObject.SetActive(slots[currentLevel].entity);
    }
}
