using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneController.Instance.LoadLevel("MapPhase");
        }
    }
}
