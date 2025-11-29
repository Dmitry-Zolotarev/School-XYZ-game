using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Input : MonoBehaviour
{
    private StatsWindow statsWindow;
    private PauseComponent pauseComponent;
    private void Start()
    {
        statsWindow = GetComponent<StatsWindow>();
        pauseComponent = GetComponent<PauseComponent>();
    }
    public void ToggleStatsWindow(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale > 0) statsWindow.ToggleStatsWindow();
    }
    public void ToggleInventoryWindow(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale > 0) statsWindow.ToggleInventoryWindow();
    }
    public void TogglePerksWindow(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale > 0) statsWindow.TogglePerksWindow();
    }
    public void ToggleEscape(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (statsWindow != null && statsWindow.statsWindow.gameObject.activeSelf)
            {
                statsWindow.CloseWindow();
            }
            else pauseComponent?.Pause();
        } 
    }
}
