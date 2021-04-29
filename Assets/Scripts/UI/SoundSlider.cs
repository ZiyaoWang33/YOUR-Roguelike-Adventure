using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private string channel = string.Empty;

    private void OnEnable()
    {
        slider.value = AudioController.Instance.GetVolume(channel + "Volume");
    }

    public void OnValueChanged()
    {
        switch (channel)
        {
            case "Master":
                AudioController.Instance.ChangeMasterVolume(slider.value);
                break;
            case "Music":
                AudioController.Instance.ChangeMusicVolume(slider.value);
                break;
            case "SFX":
                AudioController.Instance.ChangeSFXVolume(slider.value);
                break;
        }
    }
}
