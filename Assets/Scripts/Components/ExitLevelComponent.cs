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
            SceneManager.LoadScene(levelName);
        }
        catch(Exception e){ Debug.Log(e.Message); }
    }
}
