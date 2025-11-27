using UnityEngine;

public class UI_InputLegacy : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private PauseComponent pauseComponent;
    private StatsWindow statsWindow;

    private void Start()
    {
        pauseComponent = GetComponent<PauseComponent>();
        statsWindow = GetComponent<StatsWindow>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(statsWindow.statsWindow.gameObject.activeSelf)
            {
                statsWindow.CloseWindow();
            }
            else pauseComponent?.Pause();
        }
        if (Input.GetKeyDown(KeyCode.K)) statsWindow.ToggleStatsWindow();
        if (Input.GetKeyDown(KeyCode.I)) statsWindow.ToggleInventoryWindow();
        if (Input.GetKeyDown(KeyCode.P)) statsWindow.TogglePerksWindow();
    }   
}
