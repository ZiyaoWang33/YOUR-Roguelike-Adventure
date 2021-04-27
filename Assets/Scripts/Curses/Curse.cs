using UnityEngine;

public abstract class Curse : MonoBehaviour
{
    protected Player player = null;

    protected void OnEnable()
    {
        SceneController.OnQuit += ResetDrawback;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        player.GetComponent<Health>().OnDeath += ResetDrawback;
    }

    public abstract string GetDescription();

    protected abstract void ResetDrawback();

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

    protected virtual void OnDisable()
    {
        player.GetComponent<Health>().OnDeath -= ResetDrawback;
        SceneController.OnQuit -= ResetDrawback;
    }
}
