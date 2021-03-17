using UnityEngine;
using System;

public abstract class Boss : Enemy
{
    public static event Action<int> OnAnyBossDefeated;

    [HideInInspector] public float speedMultiplier = 1;

    // Change this element based on the environment that the boss is in, attached as a component.
    protected IBossElement element = null;
    [SerializeField] protected float waitTime = 0; // Ensure that this value is lower than all cooldowns
    [SerializeField] protected float[] abilityCooldowns = null;
    // set value as true or false per each ability used above depending on if it should be used in the second stage
    [SerializeField] protected bool[] useInSecondStage = null;

    protected float[] abilityTimers = null;
    protected int maxHealth = 0;
    protected bool secondStage = false;

    protected virtual void OnEnable()
    {
        health.OnDamageTaken += OnDamageTakenEventHandler;
        health.OnDeath += OnDeathEventHandler;
    }

    protected override void Awake()
    {
        base.Awake();

        attackTimer = waitTime;
        element = gameObject.GetComponent<IBossElement>();
        maxHealth = health.health;

        abilityTimers = new float[abilityCooldowns.Length];
        for (int i = 0; i < abilityTimers.Length; i++)
        {
            abilityTimers[i] = waitTime;
        }
    }

    protected override void Update()
    {
        attackTimer -= Time.deltaTime;

        if (inRange)
        {
            Attack();
        }

        for (int i = 0; i < abilityTimers.Length; i++)
        {
            abilityTimers[i] -= Time.deltaTime;
        }
    }

    protected virtual void UseAbility(int ability)
    {
        element.UseAbility(ability);
    }

    protected override void Move()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * stats.speed * speedMultiplier * Time.deltaTime;
    }

    protected override void Attack()
    {
        if (attackTimer < 0)
        {
            element.Attack();
            attackTimer = stats.attackSpeed;
        }

        for (int i = 0; i < abilityTimers.Length; i++)
        {
            if (abilityTimers[i] <= 0)
            {
                if (useInSecondStage[i] && !secondStage)
                {
                    continue;
                }
                
                UseAbility(i);
                abilityTimers[i] = abilityCooldowns[i];
            }
        }
    }

    protected virtual void OnDamageTakenEventHandler()
    {
        if (health.health < maxHealth / 2)
        {
            secondStage = true;
        }
    }
    private void OnDeathEventHandler()
    {
        OnAnyBossDefeated?.Invoke(MapData.currentLevel);
    }


    protected virtual void OnDisable()
    {
        health.OnDamageTaken -= OnDamageTakenEventHandler;
        health.OnDeath -= OnDeathEventHandler;
    }
}
