using UnityEngine;
using UnityEngine.InputSystem;

public class PauseComponent : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private ChangePitchComponent musicSpeed;
    void Start()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
        musicSpeed = GetComponent<ChangePitchComponent>();
    }

    public void Pause()
    {
        if (pauseMenu == null) return;
        
        if (pauseMenu.activeSelf && Time.timeScale == 0f)
        {
            pauseMenu?.SetActive(false);
            musicSpeed.ChangeMusicPitch(100);
            Cursor.visible = false;
            Time.timeScale = 1f;
            
        }
        else if(Time.timeScale == 1f)
        {
            pauseMenu?.SetActive(true);
            musicSpeed.ChangeMusicPitch(0);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
