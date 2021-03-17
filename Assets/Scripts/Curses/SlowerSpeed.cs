using UnityEngine;

public class SlowerSpeed : Curse
{
    [SerializeField] private float speedMultiplier = 0.75f;
    [SerializeField] private float damageMultiplier = 1.5f;

    public override string GetDescription()
    {
        return "Slower Speed";
    }

    public override void ChangePlayerStats()
    {
        player.damageMultipier = damageMultiplier;
        player.speedMultiplier = speedMultiplier;
    }
}
