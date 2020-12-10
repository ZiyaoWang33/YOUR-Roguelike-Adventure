using UnityEngine;
using System.Collections.Generic;

public abstract class Boss : Enemy
{
    public float speedMultiplier = 1;

    // Change this element based on the environment that the boss is in, attached as a component.
    protected IBossElement element = null;
    [SerializeField] protected float[] abilityCooldowns = null;
    // set value as true or false per each ability used above depending on if it should be used in the second stage
    [SerializeField] protected bool[] useInSecondStage = null;

    protected float[] abilityTimers = null;
    protected int maxHealth = 0;
    protected bool secondStage = false;

    protected virtual void OnEnable()
    {
        health.OnDamageTaken += OnDamageTakenEventHandler;
    }

    protected override void Awake()
    {
        base.Awake();
        element = gameObject.GetComponent<IBossElement>();
        maxHealth = health.health;

        abilityTimers = new float[abilityCooldowns.Length];
        for (int i = 0; i < abilityTimers.Length; i++)
        {
            abilityTimers[i] = 0;
        }
    }

    protected override void Update()
    {
        base.Update();

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
        transform.position += direction * stats.speed * speedMultiplier * Time.deltaTime;
    }

    protected override void Attack()
    {
        element.Attack();

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

        attackTimer = stats.attackSpeed;
    }

    protected virtual void OnDamageTakenEventHandler()
    {
        if (health.health < maxHealth / 2)
        {
            secondStage = true;
        }
    }

    protected virtual void OnDisable()
    {
        health.OnDamageTaken -= OnDamageTakenEventHandler;
    }
}
