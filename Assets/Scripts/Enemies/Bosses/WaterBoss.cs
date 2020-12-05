using UnityEngine;

public class WaterBoss : Enemy
{
    private IBossElement element = null; // Change this element based on the environment that the boss is in, attached as a component.
    [SerializeField] private float abilitySpeed = 1;
    [SerializeField] private float secondAttackSpeed = 0;

    private float abilityTimer = 0;
    private int maxHealth = 0;
    private bool secondStage = false;

    private void OnEnable()
    {
        health.OnDamageTaken += OnDamageTakenEventHandler;
    }

    protected override void Awake()
    {
        base.Awake();
        element = gameObject.GetComponent<IBossElement>();
        maxHealth = health.health;
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
        
        if (abilityTimer <= 0 && secondStage)
        {
            UseAbility();
            abilityTimer = abilitySpeed;
        }

        attackTimer = secondStage ? secondAttackSpeed : stats.attackSpeed;
    }

    private void OnDamageTakenEventHandler()
    {
        if (health.health < maxHealth / 2)
        {
            secondStage = true;
        }
    }

    private void OnDisable()
    {
        health.OnDamageTaken -= OnDamageTakenEventHandler;
    }
}
