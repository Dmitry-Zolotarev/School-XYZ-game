using UnityEngine;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject gameOverWindow;
    public void GameOver()
    {
        if (gameOverWindow != null)
        {
            gameOverWindow.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }       
    }
}
