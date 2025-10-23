using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button tryAgainButton;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        tryAgainButton.onClick.AddListener(OnTryAgainClicked);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnTryAgainClicked()
    {
        GameManager.Instance.RestartLevel();
    }
}