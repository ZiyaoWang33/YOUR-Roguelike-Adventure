using UnityEngine;

public class LevelExit : SceneTransition
{
    [HideInInspector] public bool active = false;
    private bool inRange = false;
    
    private const string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(playerTag))
        {
            inRange = true;
        }
    }

    private void Update()
    {
        if (active && inRange && Input.GetButtonDown("Use"))
        {
            SceneController.Instance.SwitchLevel("MapPhase");
            MapData.currentLevel++;
        }

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = active;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals(playerTag))
        {
            inRange = false;
        }
    }
}
