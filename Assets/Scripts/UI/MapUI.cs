using UnityEngine;
using System;

public class MapUI : MonoBehaviour
{
    [HideInInspector] public event Action OnMapExit;

    // For use in an OnClick event on a UI button/component
    private void Exit()
    {
        OnMapExit?.Invoke();
    }
}
