using UnityEngine;

[System.Serializable]
public class GameSession : MonoBehaviour
{
    public int HP, maxHP, coins, damage;
    private static GameSession instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
