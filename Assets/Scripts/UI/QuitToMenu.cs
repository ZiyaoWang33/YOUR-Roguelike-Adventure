﻿using UnityEngine;

public class QuitToMenu : MonoBehaviour, IMenuButton
{
    [SerializeField] private PersistentPlayerStats playerStats = null;

    public void OnClick()
    {
        playerStats.ResetPlayerStats();
        SceneController.Instance.UnloadLevel("MapPhase");
        SceneController.Instance.Quit();
        SceneController.Instance.SetTransitionActive(true);
    }
}
