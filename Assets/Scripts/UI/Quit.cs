using UnityEngine;

public class Quit : MonoBehaviour, IMenuButton
{
    public void OnClick()
    {
        SceneController.Instance.Quit();
    }
}
