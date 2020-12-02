﻿using UnityEngine;
using System;
using System.Linq;

public class MapUI : SceneTransition
{
    [HideInInspector] public event Action OnMapExit;

    [SerializeField] private GameObject[] deactivate = null;
    private static string[] levels = {"Forest", "Lake", "Volcano", "Desert"};

    // For use in an OnClick event on a UI button/component
    public void Exit()
    {
        OnMapExit?.Invoke();

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

    protected override void OnGameStateChangeEventHandler(GameStateManager.GameState previous, GameStateManager.GameState current)
    {
        if (previous == GameStateManager.GameState.RUNNING && current == GameStateManager.GameState.RUNNING)
        {
            if (levels.Contains(SceneController.Instance.previousLevel))
            {
                foreach (GameObject obj in deactivate)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject obj in deactivate)
                {
                    obj.SetActive(false);
                }
            }

        }
    }
}
