using UnityEngine;

public abstract class GameplayUIElement : MonoBehaviour
{
    [SerializeField] protected GameObject element = null;

    protected Player player = null;

    protected virtual void Awake()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
    }

    protected virtual void OnPlayerEnterEventHandler(Player player)
    {
        element.SetActive(true);

        this.player = player;
        player.OnPlayerExit += OnPlayerDeathEventHandler;
    }

    protected virtual void OnPlayerDeathEventHandler()
    {
        element.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;

        if (player)
        {
            player.OnPlayerExit -= OnPlayerDeathEventHandler;
        }
    }
}
