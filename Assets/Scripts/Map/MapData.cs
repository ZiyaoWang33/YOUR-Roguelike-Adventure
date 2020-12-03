using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField] public static int currentLevel = 0;

    [SerializeField] private MapUI ui = null;
    [SerializeField] private MapSlot[] slots = null;

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
}
