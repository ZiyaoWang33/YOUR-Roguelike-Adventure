using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Scrollbar slider = null;

    public void OnValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                AudioController.Instance.ChangeMasterVolume(slider.value);
                break;
            case 1:
                AudioController.Instance.ChangeMusicVolume(slider.value);
                break;
            case 2:
                AudioController.Instance.ChangeSFXVolume(slider.value);
                break;
        }
    }
}
