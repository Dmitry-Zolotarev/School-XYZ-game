using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(WalletComponent))]
public class PlayerController : EntityController
{
    private static PlayerController instance;
    private static HPComponent health;
    private static WalletComponent wallet;
    private PlayerData backup = new PlayerData(0, 0, 0, 0);
    private void Awake()
    {
        health = GetComponent<HPComponent>();
        wallet = GetComponent<WalletComponent>();
        if (instance != null && instance != this)
        {
            instance.SetPosition(transform.position);
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            if(backup.Sum() == 0) SaveSession();
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SaveSession()
    {
        backup = new PlayerData
        {
            HP = health.HP,
            maxHP = health.maxHP,
            coins = wallet.coinAmount,
            damage = damage
        };
    }
    public void LoadSession()
    {
        if (backup.Sum() == 0) return;
        health.HP = backup.HP;
        health.maxHP = backup.maxHP;
        wallet.coinAmount = backup.coins;
        damage = backup.damage;
    }
}

