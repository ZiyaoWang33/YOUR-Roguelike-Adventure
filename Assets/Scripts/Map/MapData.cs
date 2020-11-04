using UnityEngine;
using System.Collections.Generic;

public class MapData : MonoBehaviour
{
    [HideInInspector] public Dictionary<MapSlot, MapEntity> entities = null;

    [SerializeField] private MapUI ui = null;
    [SerializeField] private MapSlot[] slots = null;

    private void OnEnable()
    {
        ui.OnMapExit += OnMapExitEventHandler;
    }

    private void OnMapExitEventHandler()
    {
        foreach (MapSlot slot in slots)
        {
            if (slot.entity)
            {
                entities[slot] = slot.entity;
                slot.entity.locked = true;
            }
        }
    }

    private void OnDisable()
    {
        ui.OnMapExit -= OnMapExitEventHandler;
    }
}
