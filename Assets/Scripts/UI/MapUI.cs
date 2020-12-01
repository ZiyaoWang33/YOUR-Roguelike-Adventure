using UnityEngine;
using System;

public class MapUI : SceneTransition
{
    [HideInInspector] public event Action OnMapExit;

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
}
