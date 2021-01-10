using UnityEngine;

public class Pause : GameplayUIElement, IMenuButton
{
    [SerializeField] private GameStateManager gameState = null;
    [SerializeField] private GameObject pauseMenu = null;

    public void OnClick()
    {
        gameState.TogglePause();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
