using UnityEngine;

public class QuitToMenu : MonoBehaviour, IMenuButton
{
    public void OnClick()
    {
        SceneController.Instance.UnloadLevel("MapPhase");
        SceneController.Instance.Quit();
    }
}
