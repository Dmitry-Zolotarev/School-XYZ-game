using UnityEngine;
using UnityEngine.InputSystem;

public class PauseComponent : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    void Start()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (pauseMenu == null) return;
        
        if (pauseMenu.activeSelf && Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1f;
            
        }
        else if(Time.timeScale == 1f)
        {
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
