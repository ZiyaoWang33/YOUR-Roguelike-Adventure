using UnityEngine;

public class GameOver : GameplayUIElement
{
    protected override void OnPlayerEnterEventHandler(Player player)
    {
        this.player = player;
        player.OnPlayerExit += OnPlayerExitEventHandler;
    }

    protected override void OnPlayerExitEventHandler()
    {
        element.SetActive(true);
    }
}
