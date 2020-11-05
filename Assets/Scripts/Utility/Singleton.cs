using UnityEngine;

public class Singleton<Object> : MonoBehaviour where Object : Singleton<Object>
{
    public static Object Instance { private set; get; }

    public static bool isInitalized
    {
        get { return Instance != null; }
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Second instance of type Singleton cannot be created.");
        }
        else
        {
            Instance = (Object)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance.Equals(this))
        {
            Instance = null;
        }
    }
}
