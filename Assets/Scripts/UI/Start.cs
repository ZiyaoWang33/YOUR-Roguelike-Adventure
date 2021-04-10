using UnityEngine;

public class Start : MonoBehaviour, IMenuButton
{
    public void OnClick()
    {
        SceneController.Instance.LoadLevel("Intro");
    }
}
