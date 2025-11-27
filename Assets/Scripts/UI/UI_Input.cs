using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Input : MonoBehaviour
{
    private StatsWindow statsWindow;
    private PauseComponent pauseComponent;
    private void Start()
    {
        statsWindow = GetComponent<StatsWindow>();
        PauseComponent pause = GetComponent<PauseComponent>();
    }
    public void CloseWindow(InputAction.CallbackContext context)
    {
        if (context.performed) statsWindow.CloseWindow();
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
    public void TogglePause(InputAction.CallbackContext context)
    {
        if (context.performed) pauseComponent.Pause();
    }
}
