using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public static event Action OnQuit;

    [HideInInspector] public string previousLevel = string.Empty;
    public string currentLevel = string.Empty;
    [SerializeField] private GameStateManager gameState = null;
    private bool transitionActive = false;

    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();

    private void OnEnable()
    {
        GameStateManager.OnGameStateChange += OnGameStateChangeEventHandler;
    }

    private void OnGameStateChangeEventHandler(GameStateManager.GameState previous, GameStateManager.GameState current)
    {
        if (transitionActive && previous == GameStateManager.GameState.RUNNING && current == GameStateManager.GameState.RUNNING)
        {
            UnloadLevel();
        }
    }

    public void SetTransitionActive(bool enabled)
    {
        transitionActive = enabled;
    }

    private void Start()
    {
        LoadLevel(currentLevel);
        SetTransitionActive(true);
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
            Debug.Log("Loading in progress: " + Mathf.Clamp(ao.progress / 0.9f, 0, 1) * 100 + "%");
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

        if (ao == null)
        {
            Debug.LogError("Unable to unload " + lvl);
            return;
        }

        ao.completed += OnUnloadComplete;
    }

    public void UnloadLevel()
    {
        UnloadLevel(previousLevel);
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
        OnQuit?.Invoke();
        LoadLevel("Menu");
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChangeEventHandler;
    }
}
