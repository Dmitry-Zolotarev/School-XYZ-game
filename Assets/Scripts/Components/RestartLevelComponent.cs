using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene().name;
        var player = FindAnyObjectByType<PlayerController>();
        player.LoadSession();
        SceneManager.LoadScene(scene);
    }
}
