using UnityEngine;

public class GameOver : GameplayUIElement
{
    protected override void OnPlayerEnterEventHandler(Player player)
    {
        this.player = player;
        player.GetComponent<Health>().OnDeath += OnPlayerDeathEventHandler;
    }

    protected override void OnPlayerDeathEventHandler()
    {
        element.SetActive(true);
    }
}
