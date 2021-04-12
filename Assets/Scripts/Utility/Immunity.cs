using System.Collections;
using UnityEngine;

public class Immunity : MonoBehaviour
{
    [SerializeField] private float immuneTime = 0;
    [SerializeField] private bool _isImmune = false;
    public bool isImmune { get { return _isImmune; } }

    private const int playerLayer = 9;
    private const int enemyLayer = 0;
    private const int bulletLayer = 10;

    private void Awake()
    {
        DisablePlayerImmunity();
    }

    private void Update()
    {
        if (_isImmune)
        {
            StartCoroutine(Blinking(0.05f));
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            StopAllCoroutines();
        }
    }

    public void EnablePlayerImmunity()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, true);
        _isImmune = true;
        StartCoroutine(IFrames());
    }

    private void DisablePlayerImmunity()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, false);
        _isImmune = false;
    }

    IEnumerator IFrames()
    {
        yield return new WaitForSeconds(immuneTime);
        DisablePlayerImmunity();
    }

    IEnumerator Blinking(float time)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
