using UnityEngine;

public class WaterBoss : Enemy
{
    private IBossElement element = null; // Change this element based on the environment that the boss is in, attached as a component.
    [SerializeField] private float abilitySpeed = 1;
    private float abilityTimer = 0;

    protected override void Awake()
    {
        base.Awake();
        element = gameObject.GetComponent<IBossElement>();
    }

    protected override void Update()
    {
        base.Update();
        abilityTimer -= Time.deltaTime;
    }

    protected virtual void UseAbility()
    {
        element.UseAbility();
    }

    protected override void Attack()
    {
        element.Attack();
        
        if (abilityTimer <= 0)
        {
            UseAbility();
            abilityTimer = abilitySpeed;
        }

        attackTimer = stats.attackSpeed;
    }
}
