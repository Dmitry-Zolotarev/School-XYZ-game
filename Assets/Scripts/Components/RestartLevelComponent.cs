using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene().name;
        var session = FindAnyObjectByType<GameSession>();
        var player = FindAnyObjectByType<PlayerController>();
        player.LoadSession(session);
        SceneManager.LoadScene(scene);
    }
}
