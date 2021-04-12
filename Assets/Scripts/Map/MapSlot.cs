using UnityEngine;

public class MapSlot : MonoBehaviour
{
    public string areaElement;      // Manually set area element in inspector
    public string monsterElement;
    public MapEntity entity;

    [SerializeField] private int level = 0;     // Manually set in inspector

    public void ChooseEntity(MapEntity entity)
    {
        this.entity?.DeslotEntity();
        this.entity = entity;
        this.entity.SlotEntity();
        monsterElement = this.entity.element;
    }
}
