using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [HideInInspector] public string previousLevel = string.Empty;

    [SerializeField] private string currentLevel = string.Empty;
    [SerializeField] private GameStateManager gameState = null;

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
            Debug.LogError("Unable to unload " + lvl);
            return;
        }
    }

    public void UnloadLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(previousLevel);
        ao.completed += OnUnloadComplete;

        if (ao == null)
        {
            Debug.LogError("Unable to unload " + previousLevel);
            return;
        }
    }

    private void OnUnloadComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete.");
    }

    public string SwitchLevel(string lvl)
    {
        string result = previousLevel;

        previousLevel = currentLevel;
        gameState.UpdateState(GameStateManager.GameState.RUNNING);
        currentLevel = lvl;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(lvl));

        return result;
    }

    public void Quit()
    {
        LoadLevel("Menu");
        gameState.UpdateState(GameStateManager.GameState.PREGAME);
        UnloadLevel("Main");
    }
}
