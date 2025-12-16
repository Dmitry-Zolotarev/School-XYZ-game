using UnityEngine;

public class ResumeButtonComponent : MonoBehaviour
{
    private PauseComponent pauseComponent;
    private void Start()
    {
        pauseComponent = FindAnyObjectByType<PauseComponent>();
    }
    public void Resume() => pauseComponent?.Pause();
}
