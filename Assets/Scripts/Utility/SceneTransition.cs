using UnityEngine;

public abstract class SceneTransition : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        GameStateManager.OnGameStateChange += OnGameStateChangeEventHandler;
    }

    protected virtual void OnGameStateChangeEventHandler(GameStateManager.GameState previous, GameStateManager.GameState current)
    {
        if (previous == GameStateManager.GameState.RUNNING && current == GameStateManager.GameState.RUNNING)
        {
            SceneController.Instance.UnloadLevel();
        }
    }

    protected virtual void OnDisable()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChangeEventHandler;
    }
}
