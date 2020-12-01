using UnityEngine;
using System.Collections.Generic;

public class MapData : MonoBehaviour
{
    [HideInInspector] public Dictionary<MapSlot, MapEntity> entities = null;
    [SerializeField] public static int currentLevel = 0;

    [SerializeField] private MapUI ui = null;
    [SerializeField] private MapSlot[] slots = null;

    private void OnEnable()
    {
        OnMapEnterEventHandler();
        ui.OnMapExit += OnMapExitEventHandler;
        DebugLogEntities();
    }

    // References the global dictionary to know which monsters need to be locked
    private void OnMapEnterEventHandler()
    {
        entities = SceneController.Instance.LockedEntities;
        foreach (KeyValuePair<MapSlot, MapEntity> entity in entities)
        {
            entity.Value.transform.position = entity.Key.transform.position;
            entity.Value.LockEntity();
        }
    }

    // Updates the global dictionary on which monsters need to be locked
    private void OnMapExitEventHandler()
    {
        foreach (MapSlot slot in slots)
        {
            if (slot.entity)
            {
                if (!entities.ContainsKey(slot))
                {
                    entities.Add(slot, slot.entity);
                }
            }
        }
    }

    private void OnDisable()
    {
        ui.OnMapExit -= OnMapExitEventHandler;
        DebugLogEntities();
    }

    // Prints the contents of the global dictionary.
    // Data is preserved up until OnUnloadComplete() in SceneController. Don't know a fix, yet.
    private void DebugLogEntities()
    {
        foreach (KeyValuePair<MapSlot, MapEntity> entity in SceneController.Instance.LockedEntities)
        {
            Debug.Log(entity.Key + ": " + entity.Value);
        }
    }
}
