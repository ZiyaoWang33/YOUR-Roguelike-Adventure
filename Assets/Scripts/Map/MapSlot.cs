using UnityEngine;

public class MapSlot : MonoBehaviour
{
    public string areaElement;      // Manually set area element in inspector
    public string monsterElement;
    public MapEntity entity;

    private Rigidbody2D rb2d;
    [SerializeField] private bool slotted = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out MapEntity enemy))
        {
            // Note: Still need to handle locking map slots that have been slotted after the player leaves
            // the map building phase
            if (slotted)
            {
                // Player can de-slot map monster by re-selecting
                if (enemy.slotted && enemy.selected)
                {
                    slotted = false;
                    enemy.slotted = false;
                    monsterElement = null;
                }
            }
            else
            {
                // Player can move map monster over slot without it auto-slotting in
                if (!enemy.selected)
                {
                    slotted = true;
                    enemy.slotted = true;
                    monsterElement = enemy.element;
                    entity = enemy;
                    enemy.transform.position = transform.position;
                }
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
            }
        }
    }
}
