using UnityEngine;

public class GrassWaterCreepSet : MonoBehaviour
{
    private bool active = false;
    [HideInInspector] public Player player = null;

    private void Update()
    {
        if (!active)
        {
            InitializeCreeps();
        }
    }

    private void InitializeCreeps()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<Enemy>().player = player.GetComponent<Health>();
        }
        active = true;
    }
}
