using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(WalletComponent))]
public class PlayerController : EntityController
{
    private static PlayerController instance;
    private static HPComponent health;
    private static WalletComponent wallet;
    private GameSession gameSession;
    private void Awake()
    {
        health = GetComponent<HPComponent>();
        wallet = GetComponent<WalletComponent>();
        gameSession = FindAnyObjectByType<GameSession>();
        if (instance != null && instance != this)
        {
            instance.SetPosition(transform.position);
            instance.SaveSession(gameSession);
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void SaveSession(GameSession session)
    {
        gameSession = session;
        session.HP = health.HP;
        session.maxHP = health.maxHP;
        session.coins = wallet.coinAmount;
        session.damage = damage;
    }
    public void LoadSession(GameSession session)
    {
        gameSession = session;
        health.HP = session.HP;
        health.maxHP = session.maxHP;
        wallet.coinAmount = session.coins;
        damage = session.damage;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameSession != null) LoadSession(gameSession);

    }
}

