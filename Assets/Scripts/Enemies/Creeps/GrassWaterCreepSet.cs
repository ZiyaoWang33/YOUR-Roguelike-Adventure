using UnityEngine;

public class GrassWaterCreepSet : MonoBehaviour
{
    private bool active = false;
    private GameObject origin = null;
    private Player player = null;

    public void SetTargets(GameObject o, Player p)
    {
        origin = o;
        player = p;
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
