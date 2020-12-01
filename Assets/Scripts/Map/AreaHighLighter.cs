using UnityEngine;

public class AreaHighLighter : MonoBehaviour
{
    [SerializeField] private MapSlot[] slots = null;

    private void OnEnable()
    {
        transform.position = slots[MapData.currentLevel].transform.position;
    }       
}
