using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameComponent : MonoBehaviour
{
    public void RestartGame() => SceneManager.LoadScene(0);
}
