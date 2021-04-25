using UnityEngine;

public class PersistentPlayerStats : Singleton<PersistentPlayerStats>
{
    [SerializeField] private int defaultMaxHealth = 100;
    [SerializeField] private float defaultBaseSpeedMultiplier = 1;
    [SerializeField] private float defaultHurtMultiplier = 1;
    [SerializeField] private float defaultDamageMultiplier = 1;

    private int maxHealth;
    private float baseSpeedMultiplier;
    private float hurtMultiplier;
    private float damageMultiplier;

    private Player player = null;
    private Health playerHealth = null;

    protected override void Awake()
    {
        base.Awake();
        ResetPlayerStats();
    }

    public void SetPlayerStats(Player player)
    {
        this.player = player;
        playerHealth = player.gameObject.GetComponent<Health>();
        playerHealth.OnDeath += ResetPlayerStats;

        player.baseSpeedMultiplier = baseSpeedMultiplier;
        player.gameObject.GetComponent<Health>().damageMultiplier = hurtMultiplier;
        player.damageMultiplier = damageMultiplier;

        Debug.Log("Player Stats Set -> " +
            "Base Speed Multiplier: " + player.baseSpeedMultiplier + ", " +
            "Damage Taken Multiplier: " + player.gameObject.GetComponent<Health>().damageMultiplier + ", " +
            "Damage Dealt Multiplier: " + player.damageMultiplier);
    }

    public void SavePlayerStats()
    {
        baseSpeedMultiplier = player.baseSpeedMultiplier;
        hurtMultiplier = playerHealth.damageMultiplier;
        damageMultiplier = player.damageMultiplier;

        Debug.Log("Player Stats Saved -> " +
            "Base Speed Multiplier: " + baseSpeedMultiplier + ", " +
            "Damage Taken Multiplier: " + hurtMultiplier + ", " + 
            "Damage Dealt Multiplier: " + damageMultiplier);
    }

    public void ResetPlayerStats()
    {
        MapData.currentLevel = 0;
        maxHealth = defaultMaxHealth;
        baseSpeedMultiplier = defaultBaseSpeedMultiplier;
        hurtMultiplier = defaultHurtMultiplier;
        damageMultiplier = defaultDamageMultiplier;

        Debug.Log("Player Stats Resetted");
    }
}
