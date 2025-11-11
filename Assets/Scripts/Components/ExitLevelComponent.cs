using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelComponent : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void Exit(GameObject entity)
    {
        try
        {
            var player = entity.GetComponent<PlayerController>();
            if (player != null) player.SaveSession();
            SceneManager.LoadScene(levelName);
        }
        catch(Exception e){ Debug.Log(e.Message); }
    }
}
