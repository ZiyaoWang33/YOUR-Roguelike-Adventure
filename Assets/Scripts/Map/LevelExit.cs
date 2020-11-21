using UnityEngine;

public class LevelExit : SceneTransition
{
    private bool active = false;
    
    private const string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(playerTag))
        {
            active = true;
        }
    }

    private void Update()
    {
        if (active && Input.GetButtonDown("Use"))
        {
            SceneController.Instance.LoadLevel("MapPhase");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals(playerTag))
        {
            active = false;
        }
    }
}
