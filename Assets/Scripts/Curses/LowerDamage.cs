using UnityEngine;

public class LowerDamage : Curse
{
    [SerializeField] private float baseDamageMultiplier = 0.85f;
    [SerializeField] private float damageIncrement = 0.1f;

    private float damageMultiplier;

    private void Awake()
    {
        ResetDrawback();
    }

    public override string GetDescription()
    {
        return "Less Damage";
    }

    protected override void ResetDrawback()
    {
        damageMultiplier = baseDamageMultiplier;
        Debug.Log("Damage Curse Reset");
    }

    protected override void IncreaseDrawback()
    {
        damageMultiplier -= damageIncrement;
    }

    protected override void DecreaseDrawback()
    {
        damageMultiplier += damageIncrement;
    }

    public override void ChangePlayerStats()
    {
        player.damageMultiplier *= damageMultiplier;
        base.BuffPlayer();
    }
}
