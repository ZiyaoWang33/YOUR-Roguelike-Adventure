using System.Collections.Generic;
using UnityEngine;

public class BossRoomActivator : RoomActivator
{
    // Provides that the Boss sets in the container object should follow this order.
    private Dictionary<string, int> indexes = new Dictionary<string, int>(){{"fire", 0}, {"water", 1}, {"wood", 2}};

    protected override int SetIndexToUse()
    {
        return indexes[MapData.currentElement];
    }
}