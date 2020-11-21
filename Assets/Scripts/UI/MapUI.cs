using UnityEngine;
using System;

public class MapUI : SceneTransition
{
    [HideInInspector] public event Action OnMapExit;

    // For use in an OnClick event on a UI button/component
    public void Exit()
    {
        OnMapExit?.Invoke();

        SceneController.Instance.LoadLevel("Forest");
    }
}
