using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(WalletComponent))]
public class PlayerController : EntityController
{
    private static PlayerController instance;
    private static HPComponent health;
    private static WalletComponent wallet;
    private static PlayerData backup = new PlayerData(0, 0, 0, 0);
    private static string path => Path.Combine(Application.persistentDataPath, "playerdata.json");

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
        string json = JsonUtility.ToJson(backup, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }

    public void LoadSession()
    {
        if (!File.Exists(path)) return;
        backup = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
    }
}

