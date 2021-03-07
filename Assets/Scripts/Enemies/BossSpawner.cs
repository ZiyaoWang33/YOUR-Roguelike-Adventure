using UnityEngine;
using System.Collections.Generic;

public class BossSpawner : Spawner
{
    // Provides that the Boss sets in the container object should follow this order.
    private Dictionary<string, int> indexes = new Dictionary<string, int>() { { "fire", 0 }, { "water", 1 }, { "wood", 2 } };

    protected override int SetIndexToUse(int end)
    {
        return indexes[MapData.currentElement];
    }

    protected override void OnAnyRoomEnteredEventHandler(RoomActivator room)
    {
        return;
    }

    protected override void OnPerformanceChangeEventHandler(PlayerPerformanceManager.Performance previous, PlayerPerformanceManager.Performance current, RoomActivator room)
    {
        return;
    }
}
