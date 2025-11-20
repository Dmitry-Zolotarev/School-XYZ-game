using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelComponent : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void Exit()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if (player != null) player.SaveSession();


        SceneManager.LoadScene(levelName);
    }
}