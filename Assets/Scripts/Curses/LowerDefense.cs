using UnityEngine;

public class LowerDefense : Curse
{
    [SerializeField] private float hurtMultiplier = 1.75f;
    [SerializeField] private float hurtIncrement = 0.25f;
    [SerializeField] private float damageMultiplier = 1.25f;

    public override string GetDescription()
    {
        return "Less Defense";
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
        player.GetComponent<Health>().damageMultiplier = hurtMultiplier;
        player.damageMultipier = damageMultiplier;
    }
}
