using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameOverUIController gameOverUI;
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverUI = FindFirstObjectByType<GameOverUIController>();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

        if (gameOverUI == null)
            gameOverUI = FindFirstObjectByType<GameOverUIController>();

        if (gameOverUI != null)
            gameOverUI.ShowGameOver();

        IsGameOver = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI?.ShowGameOver();

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            player.SetActive(false);

        gameOverUI?.ShowGameOver();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        IsGameOver = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}