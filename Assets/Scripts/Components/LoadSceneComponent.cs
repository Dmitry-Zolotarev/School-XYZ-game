using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour
{
    [SerializeField] private string levelName;
    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }
}