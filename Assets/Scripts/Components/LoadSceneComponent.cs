using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}