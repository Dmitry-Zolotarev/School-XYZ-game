using UnityEngine;

public class SingletonComponent : MonoBehaviour
{
    private static SingletonComponent instance;
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
