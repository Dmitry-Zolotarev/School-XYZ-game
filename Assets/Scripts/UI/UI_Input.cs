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
        if (context.performed) statsWindow.ToggleStatsWindow();
    }
    public void ToggleInventoryWindow(InputAction.CallbackContext context)
    {
        if (context.performed) statsWindow.ToggleInventoryWindow();
    }
    public void TogglePerksWindow(InputAction.CallbackContext context)
    {
        if (context.performed) statsWindow.TogglePerksWindow();
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
