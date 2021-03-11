using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [HideInInspector] public event Action OnEnter = null;
    [HideInInspector] public event Action OnExit = null;
    [HideInInspector] public GameObject player = null;

    [SerializeField] protected SpriteRenderer sprite = null;
    [SerializeField] protected int difficultyMultiplier = 1;

    private const string playerTag = "Player";

    protected virtual void Awake()
    {
        OnEnter += EnterEffect;
        OnExit += ExitEffect;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (player)
        {
            if (collision.gameObject == player)
            {
                OnEnter?.Invoke();
            }
        }
        else if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject;
            OnEnter?.Invoke();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            OnExit?.Invoke();
        }
    }

    protected abstract void EnterEffect();
    protected abstract void ExitEffect();
}