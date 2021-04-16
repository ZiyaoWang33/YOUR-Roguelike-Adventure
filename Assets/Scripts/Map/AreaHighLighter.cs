using UnityEngine;
using System.Collections;

public class AreaHighLighter : MonoBehaviour
{
    [SerializeField] private MapSlot[] slots = null;
    [SerializeField] private int distanceFromCamera = 5;

    private void OnEnable()
    {
        StartCoroutine(NextLevel(0.001f));
    }

    IEnumerator NextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 slotPos = slots[MapData.currentLevel].transform.position;
        transform.position = new Vector3(slotPos.x, slotPos.y, slotPos.z + distanceFromCamera);
    }
}
