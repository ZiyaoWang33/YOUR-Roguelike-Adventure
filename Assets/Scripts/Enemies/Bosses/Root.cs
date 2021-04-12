using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float slowingEffect = 0;
    [SerializeField] [Range(0, 1)] private float slowingCap = 0.5f;
    private Player player = null;
    private GameObject playerObject = null;
    private bool permanentEffect = false;

    private SlowingEffect debuff = null; // Exiting will only remove the slowing effect of the specific root
    private const float forever = 1000000; // Slowing effect is removed when exiting the root anyways

    public void SetUp(Player player, bool permanentEffect = false, float lifeTime = 0, float slowingEffect = 0)
    {
        this.slowingEffect = slowingEffect > 0 ? slowingEffect : this.slowingEffect;
        slowingCap = slowingEffect < 1 ? slowingCap : 1;

        this.player = player;
        playerObject = player.gameObject;

        this.permanentEffect = permanentEffect;

        if (lifeTime > 0)
        {
            StartCoroutine(WaitThenDestroy(lifeTime));
        }
    }


    // The player can avoid being slowed by roots 
    // if they i-frame into them but they cannot i-frame out.

    // There is a slight quirk where i-framing at the same
    // time as the root is destroyed which causes the debuff
    // component to not be removed but its effects are removed.
    //
    // Doesn't really cause problems except cluttering the inspector, so whatever.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(playerObject))
        {
            // Adds a slow debuff to the player if the effect is either:
            // Temporary, OR
            // Permanent AND the player does not already have a slow debuff
            if (!permanentEffect || (permanentEffect && playerObject.GetComponent<SlowingEffect>() == null))
            {
                debuff = playerObject.AddComponent<SlowingEffect>();
                debuff.SetStats(slowingEffect, forever, slowingCap);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool playerIsImmune = playerObject.GetComponent<Immunity>().isImmune;

        // Removes slow debuff on exit if:
        // The effect is NOT permanent, AND either
        // The effect is not a 100% slow, OR
        // The player is not i-framing
        if (!permanentEffect && other.gameObject.Equals(playerObject))
        {
            if (slowingEffect < 1 || !playerIsImmune)
            {
                debuff?.RemoveDebuff();
            }
        }
    }

    IEnumerator WaitThenDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        debuff?.RemoveDebuff();
        Destroy(gameObject);
    }
}
