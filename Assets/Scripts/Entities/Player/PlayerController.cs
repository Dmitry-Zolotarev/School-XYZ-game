using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HPComponent))]
public class PlayerController : EntityController
{
    private static PlayerController instance;
    
    private static PlayerData backup = new PlayerData(0, 0, 0, 0);
    private new void Awake()
    {
        base.Awake();  
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
            damage = attackComponent.damage
        };
    }
    public void LoadSession()
    {
        if (backup.Sum() == 0) return;
        health.HP = backup.HP;
        health.maxHP = backup.maxHP;
        attackComponent.damage = backup.damage;
    }
}
