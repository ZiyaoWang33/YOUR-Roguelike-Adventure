using UnityEngine;

public class LowerDefense : Curse
{
    [SerializeField] private float hurtMultiplier = 1.75f;
    [SerializeField] private float damageMultiplier = 1.25f;

    public override string GetDescription()
    {
        return "Less Defense";
    }

    public override void ChangePlayerStats()
    {
        player.GetComponent<Health>().damageMultiplier = hurtMultiplier;
        player.damageMultipier = damageMultiplier;
    }
}
