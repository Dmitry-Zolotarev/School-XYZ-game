using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBackgroundComponent : MonoBehaviour
{
    private Canvas background;

    private void OnEnable()
    {
        background = GetComponent<Canvas>();
        FindCamera();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        FindCamera();
    }

    private void FindCamera()
    {
        if (background == null)
            background = GetComponent<Canvas>();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var cam = player.GetComponentInChildren<Camera>();
            if (cam != null)
            {
                background.worldCamera = cam;
                return;
            }
        }

        background.worldCamera = Camera.main;
    }
}
