using UnityEngine;

public class ButtonPrompt : SceneTransition
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneController.Instance.LoadLevel("MapPhase");
        }
    }
}
