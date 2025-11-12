using UnityEngine;

public class PauseComponent : MonoBehaviour
{
    private GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (pauseMenu == null) return;

        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
