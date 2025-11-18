using UnityEngine;
using UnityEngine.InputSystem;

public class PauseComponent : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenu;

    void Start()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
    }
    public void OnPausePerformed(InputAction.CallbackContext context)
    {
        if(context.performed) Pause();
    }

    public void Pause()
    {
        if (pauseMenu == null) return;
        
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1f;
            
        }
        else
        {
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
