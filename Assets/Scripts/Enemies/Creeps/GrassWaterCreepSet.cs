using UnityEngine;

public class GrassWaterCreepSet : MonoBehaviour
{
    private bool active = false;
    private GameObject origin = null;
    private Player player = null;

    public void SetTargets(GameObject origin, Player player)
    {
        this.origin = origin;
        this.player = player;
    }

    private void Update()
    {
        if (!active)
        {
            InitializeCreeps();
        }

        if (origin == null)
        {
            Destroy(gameObject);
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
