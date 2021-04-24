using UnityEngine;

public class SlowerSpeed : Curse
{
    [SerializeField] private float speedMultiplier = 0.75f;
    [SerializeField] private float speedIncrement = 0.15f;
    [SerializeField] private float damageMultiplier = 1.5f;

    public override string GetDescription()
    {
        return "Slower Speed";
    }

    protected override void IncreaseDrawback()
    {
        speedMultiplier -= speedIncrement;
    }

    protected override void DecreaseDrawback()
    {
        speedMultiplier += speedIncrement;
    }

    public override void ChangePlayerStats()
    {           
        player.damageMultipier = damageMultiplier;
        player.baseSpeedMultiplier = speedMultiplier;
        player.speedMultiplier = speedMultiplier;
    }
}
