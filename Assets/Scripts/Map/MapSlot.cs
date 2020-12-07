using UnityEngine;

public class MapSlot : MonoBehaviour
{
    public string areaElement;      // Manually set area element in inspector
    public string monsterElement;
    public MapEntity entity;

    private Rigidbody2D rb2d;
    [SerializeField] private bool slotted = false;
    [SerializeField] private int level = 0;     // Manually set in inspector

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out MapEntity enemy))
        {
            if (MapData.currentLevel == level)
            {
                if (slotted)
                {
                    // Player can de-slot map monster by re-selecting
                    if (enemy.slotted && enemy.selected)
                    {
                        slotted = false;
                        enemy.slotted = false;
                        monsterElement = null;
                        entity = null;
                    }
                    // Player can swap slotted map monsters without having to select the slotted one
                    else if (!enemy.slotted && !enemy.selected)
                    {
                        entity.slotted = false;
                        entity.ReturnToStart();

                        enemy.slotted = true;
                        enemy.transform.position = transform.position;
                        monsterElement = enemy.element;
                        entity = enemy;
                    }
                }
                else
                {
                    // Player can move map monster over slot without it auto-slotting in
                    if (!enemy.selected)
                    {
                        slotted = true;
                        enemy.slotted = true;
                        enemy.transform.position = transform.position;
                        monsterElement = enemy.element;
                        entity = enemy;
                    }
                }
            }
            // Trying to slot an enemy into an invalid slot resets the enemy position
            else if (!enemy.selected)
            {
                enemy.ReturnToStart();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out MapEntity enemy))
        {
            // Player can de-slot map monster through the right-click trick
            if (enemy.slotted)
            {
                slotted = false;
                enemy.slotted = false;
                monsterElement = null;
                entity = null;
            }
        }
    }
}
