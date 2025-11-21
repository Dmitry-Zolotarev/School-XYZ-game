using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HPComponent))]
public class PlayerController : EntityController
{
    private static PlayerController instance;
    private static HPComponent health;
    private static PlayerData backup = new PlayerData(0, 0, 0, 0);
    private void Awake()
    {
        health = GetComponent<HPComponent>();
        if (instance != null && instance != this)
        {
            instance.SetPosition(transform.position);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            if (backup.Sum() == 0) SaveSession();
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SaveSession()
    {
        backup = new PlayerData
        {
            HP = health.HP,
            maxHP = health.maxHP,
            damage = damage
        };
    }
    public void LoadSession()
    {
        if (backup.Sum() == 0) return;
        health.HP = backup.HP;
        health.maxHP = backup.maxHP;
        damage = backup.damage;
    }
}
