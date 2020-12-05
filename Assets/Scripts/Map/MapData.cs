using UnityEngine;

public class MapData : MonoBehaviour
{
    public static int currentLevel = 0;

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
