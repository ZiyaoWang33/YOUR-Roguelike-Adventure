using UnityEngine;

public class SlowerSpeed : Curse
{
    [SerializeField] private float baseSpeedMultiplier = 0.75f;
    [SerializeField] private float speedIncrement = 0.15f;
    [SerializeField] private float damageMultiplier = 1.5f;

    private float speedMultiplier;

    private void Awake()
    {
        ResetDrawback();
    }

    public override string GetDescription()
    {
        return "Slower Speed";
    }

    protected override void ResetDrawback()
    {
        speedMultiplier = baseSpeedMultiplier;
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
        player.damageMultiplier = damageMultiplier;
        player.speedMultiplier = speedMultiplier;
    }
}
