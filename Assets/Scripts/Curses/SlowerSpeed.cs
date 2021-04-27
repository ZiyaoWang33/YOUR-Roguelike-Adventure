using UnityEngine;

public class SlowerSpeed : Curse
{
    [SerializeField] private float baseSpeedMultiplier = 0.9f;
    [SerializeField] private float speedIncrement = 0.05f;

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
        Debug.Log("Speed Curse Reset");
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
        player.baseSpeedMultiplier *= speedMultiplier;
        base.BuffPlayer();
    }
}
