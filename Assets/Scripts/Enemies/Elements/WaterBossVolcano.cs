using System.Collections;
using UnityEngine;

public class WaterBossVolcano : MonoBehaviour, IBossElement
{
    [SerializeField] private WaterBoss boss = null;
    private Player player = null;
    [SerializeField] private GameObject steam = null;
    [SerializeField] private GameObject bulletPattern = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [SerializeField] private int shotSpeed = 1;
    [SerializeField] private float defenseBoost = 0;
    [SerializeField] private float abilityDuration = 0;
    [SerializeField] private float abilityCooldown = 0;
    [SerializeField] private float abilityChance = 0; // 0 = 0%, 1 = 100%

    // Enable in inspector to test effects
    [SerializeField] private bool enableFieldBlur = false;
    [SerializeField] private bool enableSteamState = false;

    private bool steamState = false;
    private float timer = 0;

    private void Awake()
    {
        timer = abilityCooldown;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void UseAbility(int ability)
    {
        switch (ability)
        {
            case 0:
                Transform shield = transform.Find("Shield");
                shield.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                boss.health.defense = defenseBoost;
                break;

            case 1:
                if (enableSteamState && !steamState && timer <= 0 && Random.Range(0, 1f) >= (1 - abilityChance))
                {
                    StartCoroutine(EnterSteamState());
                }
                break;
        }
    }

    public void Attack()
    {
        if (player == null)
        {
            player = boss.player.GetComponent<Player>();

            if (enableFieldBlur)
            {
                BlurredVision fieldBlur = Instantiate(steam).GetComponent<BlurredVision>();
                fieldBlur.SetTargets(player.gameObject, gameObject);
            }
        }

        Vector2 direction = ((player.transform.position + player.GetDirection() * Random.Range(0, 2)) - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;
        WaterBulletCone bullets = Instantiate(bulletPattern, shootPoint.position, shootPoint.rotation).GetComponent<WaterBulletCone>();
        bullets.mode = "explode";
        bullets.speed = shotSpeed;
    }

    public string GetElement()
    {
        return "volcano";
    }

    private void SetTransparency(float alphaVal)
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        Color oldColor = material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        material.SetColor("_Color", newColor);
    }

    IEnumerator EnterSteamState()
    {
        steamState = true;
        SetTransparency(0.5f);
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(abilityDuration);

        steamState = false;
        SetTransparency(1);
        GetComponent<BoxCollider2D>().enabled = true;

        timer = abilityCooldown;
    }
}