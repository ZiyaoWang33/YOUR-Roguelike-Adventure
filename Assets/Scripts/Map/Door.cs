using UnityEngine;

public class Door : MonoBehaviour
{
    private void Start()
    {
        if (!gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}
