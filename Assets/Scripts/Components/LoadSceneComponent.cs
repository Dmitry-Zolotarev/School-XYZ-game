using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour
{
    [SerializeField] private string levelName;
    public void LoadScene()
    {       
        SceneManager.LoadScene(levelName);
    }
}