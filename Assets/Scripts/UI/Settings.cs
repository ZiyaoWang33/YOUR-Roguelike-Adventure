using UnityEngine;

public class Settings : MonoBehaviour, IMenuButton
{
    [SerializeField] private GameObject settingsParent = null;

    public void OnClick()
    {
        settingsParent.SetActive(true);
    }
}
