using UnityEngine;

public abstract class Curse : MonoBehaviour
{
    [SerializeField] private float buffDamageMultiplier = 1.1f;
    [SerializeField] private float buffSpeedMultiplier = 1.2f;
    [SerializeField] private int healAmount = 10;

    protected Player player = null;
    protected Health health = null;

    protected void OnEnable()
    {
        SceneController.OnQuit += ResetDrawback;
        Player.OnPlayerEnter += SetPlayer;
    }

    protected void SetPlayer(Player player)
    {
        if (this.player != null)
        {
            OnDisable();
        }

        this.player = player;
        health = player.GetComponent<Health>();
        health.OnDeath += ResetDrawback;
        health.OnDeath += ResetBuff;
    }

    public abstract string GetDescription();

    protected virtual void ResetBuff()
    {
        if (Bullet.OnHit != null)
        {
            Bullet.OnHit = null;
        }
    }

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

    protected virtual void BuffPlayer()
    {
        string bossElement = MapData.currentElement;

        void LifeSteal(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy _))
            {
                health.Heal(healAmount);
            }
        }

        switch (bossElement)
        {
            case "fire":
                player.damageMultiplier *= buffDamageMultiplier;
                break;

            case "water":
                player.baseSpeedMultiplier *= buffSpeedMultiplier;
                break;

            case "wood":
                Bullet.OnHit = LifeSteal;
                break;
        }
    }

    protected virtual void OnDisable()
    {
        health.OnDeath -= ResetDrawback;
        health.OnDeath -= ResetBuff;
        SceneController.OnQuit -= ResetDrawback;
    }
}
