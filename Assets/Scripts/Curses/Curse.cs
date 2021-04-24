using UnityEngine;

public abstract class Curse : MonoBehaviour
{
    protected Player player = null;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public abstract string GetDescription();

    protected abstract void IncreaseDrawback();

    protected abstract void DecreaseDrawback();

    public virtual void SetDrawback(PlayerPerformanceManager.Performance performance)
    {
        switch (performance)
        {
            case PlayerPerformanceManager.Performance.BAD:
                DecreaseDrawback();
                break;
            case PlayerPerformanceManager.Performance.NEUTRAL:
                return;
            case PlayerPerformanceManager.Performance.GOOD:
                IncreaseDrawback();
                break;
        }
    }

    public abstract void ChangePlayerStats();
}
