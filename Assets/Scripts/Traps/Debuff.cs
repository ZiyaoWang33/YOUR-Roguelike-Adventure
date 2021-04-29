using UnityEngine;

public abstract class Debuff : MonoBehaviour
{
    protected Player player = null;
    protected GameObject effectPrefab = null;
    protected float lifetime = 0;

    protected GameObject vfx = null;

    // Use this and a SetStats method on this after declaring it as a variable
    // If vfx for the player are unecessary call with null
    public virtual void SetInitial(GameObject effectPrefab, float lifetime)
    {
        player = gameObject.GetComponent<Player>();
        this.effectPrefab = effectPrefab;
        this.lifetime = lifetime;

        AddEffect();
    }

    protected virtual void AddEffect()
    {
        if (effectPrefab != null)
        {
            GameObject newEffect = Instantiate(effectPrefab, player.transform.position, Quaternion.identity);
            newEffect.transform.parent = player.transform;
            vfx = newEffect;
        }
    }

    protected virtual void RemoveEffect()
    {
        if (vfx != null)
        {
            Destroy(vfx);
        }
    }

    protected virtual void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        RemoveEffect();
    }
}
