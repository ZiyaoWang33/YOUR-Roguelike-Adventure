using UnityEngine;

public class Quit : MonoBehaviour, IMenuButton
{
    public void OnClick()
    {
        Application.Quit();
    }
}
