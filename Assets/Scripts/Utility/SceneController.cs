using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public Dictionary<MapSlot, MapEntity> LockedEntities = new Dictionary<MapSlot, MapEntity>();

    [SerializeField] private string currentLevel = string.Empty;
    [SerializeField] private GameStateManager gameState = null;

    private string previousLevel = string.Empty;
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(string lvl)
    {
        previousLevel = currentLevel;
        currentLevel = lvl;
        StartCoroutine(LevelProgress(lvl));
    }

    public IEnumerator LevelProgress(string lvl)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(lvl, LoadSceneMode.Additive);
        loadOperations.Add(ao);
        ao.completed += OnLoadComplete;

        if (ao == null)
        {
            Debug.LogError("Unable to load " + lvl);
            yield break;
        }

        while (!ao.isDone)
        {
            Debug.Log(Mathf.Clamp(ao.progress / 0.9f, 0, 1));
            yield return null;
        }
    }

    private void OnLoadComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        if (loadOperations.Count < 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevel));
            gameState.UpdateState(GameStateManager.GameState.RUNNING);
        }

        Debug.Log("Load complete.");
    }

    public void UnloadLevel(string lvl)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(lvl);
        ao.completed += OnUnloadComplete;

        if (ao == null)
        {
            Debug.LogError("Unable to load " + lvl);
            return;
        }
    }

    public void UnloadLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(previousLevel);
        ao.completed += OnUnloadComplete;

        if (ao == null)
        {
            Debug.LogError("Unable to load " + previousLevel);
            return;
        }
    }

    private void OnUnloadComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete.");
    }

    public void Quit()
    {
        LoadLevel("Menu");
        gameState.UpdateState(GameStateManager.GameState.PREGAME);
        UnloadLevel("Main");
    }
}
