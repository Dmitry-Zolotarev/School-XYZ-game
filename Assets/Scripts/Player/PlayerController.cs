using UnityEngine;

public class PlayerController : EntityController
{
    private static PlayerController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance.SetPosition(transform.position);
            Destroy(gameObject); // уничтожаем дубликат
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

