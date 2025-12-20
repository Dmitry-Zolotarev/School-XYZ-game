using UnityEngine;

public class SingletonComponent : MonoBehaviour
{
    protected static SingletonComponent instance;
    private void Awake()
    {
        if (instance != null && instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
