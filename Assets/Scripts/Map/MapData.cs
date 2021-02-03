﻿using UnityEngine;

public class MapData : MonoBehaviour
{
    public static int currentLevel = 0; // Remember to set to 0 after changing for testing
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

    public void SetElement(string element, PlaytestController test)
    {
        if (test)
            currentElement = element;
    }

    public void SetLevel(int level, PlaytestController test)
    {
        if (test)
            currentLevel = level;
    }

    private void CheckSlotBeforeProceeding()
    {   
        if (ui && ui.loaded)
            ui.gameObject.SetActive(slots[currentLevel].entity);
    }
}
