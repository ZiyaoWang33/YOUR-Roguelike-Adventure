using UnityEngine;

public class BlurredVision : MonoBehaviour
{
    private GameObject player = null;
    private GameObject boss = null;

    public void SetTargets(GameObject player, GameObject boss)
    {
        this.player = player;
        this.boss = boss;
    }

    private void Update()
    {
        if (player)
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + 1);
        }      

        if (boss == null)
        {
            Destroy(gameObject);
        }
    }
}
