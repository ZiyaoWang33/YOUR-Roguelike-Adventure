using UnityEngine;

public class QuitToMenu : SceneTransition, IMenuButton
{
    public void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
        SceneController.Instance.UnloadLevel("MapPhase");
        SceneController.Instance.LoadLevel("Menu");
    }
}
