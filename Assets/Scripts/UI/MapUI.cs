using UnityEngine;
using System.Linq;

public class MapUI : MonoBehaviour
{
    [SerializeField] private MapData data = null;
    [SerializeField] private GameObject[] deactivate = null;
    private static string[] levels = {"Forest", "Lake", "Volcano", "Desert", "Intro"};

    [HideInInspector] public bool unloaded = false;
    [HideInInspector] public bool loaded = false;

    private void OnEnable()
    {
        GameStateManager.OnGameStateChange += OnGameStateChangeEventHandler;
    }

    // For use in an OnClick event on a UI button/component
    public void Exit()
    {
        if (!unloaded) // Prevents repeated clicks from generating "layered" dungeons
        {
            unloaded = true;
            data.LockEntities();

            if (MapData.currentLevel == 0)
            {
                SceneController.Instance.LoadLevel("Forest");
            }
            else if (MapData.currentLevel == 1)
            {
                SceneController.Instance.LoadLevel("Lake");
            }
            else if (MapData.currentLevel == 2)
            {
                SceneController.Instance.LoadLevel("Volcano");
            }
            else if (MapData.currentLevel == 3)
            {
                SceneController.Instance.LoadLevel("Desert");
            }
        }       
    }

    private void OnGameStateChangeEventHandler(GameStateManager.GameState previous, GameStateManager.GameState current)
    {
        if (previous == GameStateManager.GameState.RUNNING && current == GameStateManager.GameState.RUNNING)
        {
            if (levels.Contains(SceneController.Instance.previousLevel))
            {
                SceneController.Instance.ToggleTransitionActive();

                foreach (GameObject obj in deactivate)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                SceneController.Instance.ToggleTransitionActive();

                foreach (GameObject obj in deactivate)
                {
                    obj.SetActive(false);
                }
            }

            loaded = !loaded;
            unloaded = false;
        }
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChangeEventHandler;
    }
}
