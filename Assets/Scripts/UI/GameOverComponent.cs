using System.Collections;
using UnityEngine;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private float gameOverLatency = 0.5f;
    public void GameOver() => StartCoroutine(OpenMenu());
    private IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(gameOverLatency);
        if (gameOverWindow != null)
        {
            gameOverWindow.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
