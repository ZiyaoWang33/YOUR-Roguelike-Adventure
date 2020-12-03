using UnityEngine;
using System.Collections;

public class AreaHighLighter : MonoBehaviour
{
    [SerializeField] private MapSlot[] slots = null;

    private void OnEnable()
    {
        StartCoroutine(NextLevel(0.001f));
    }

    IEnumerator NextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = slots[MapData.currentLevel].transform.position;
    }
}
