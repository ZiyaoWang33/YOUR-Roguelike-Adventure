using UnityEngine;

public class Start : SceneTransition, IMenuButton
{
    public void OnClick()
    {
        SceneController.Instance.LoadLevel("Intro");
    }
}
