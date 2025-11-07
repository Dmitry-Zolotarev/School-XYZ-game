using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}
