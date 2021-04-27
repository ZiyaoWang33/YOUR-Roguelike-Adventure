using UnityEngine;

public class LowerDefense : Curse
{
    [SerializeField] private float baseHurtMultiplier = 1.15f;
    [SerializeField] private float hurtIncrement = 0.1f;

    private float hurtMultiplier;

    private void Awake()
    {
        ResetDrawback();
    }

    public override string GetDescription()
    {
        return "Less Defense";
    }

    protected override void ResetDrawback()
    {
        hurtMultiplier = baseHurtMultiplier;
        Debug.Log("Defense Curse Reset");
    }

    protected override void IncreaseDrawback()
    {
        hurtMultiplier += hurtIncrement;
    }

    protected override void DecreaseDrawback()
    {
        hurtMultiplier -= hurtIncrement;
    }

    public override void ChangePlayerStats()
    {
        player.GetComponent<Health>().damageMultiplier *= hurtMultiplier;
        base.BuffPlayer();
    }
}
