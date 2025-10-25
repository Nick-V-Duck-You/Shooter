using UnityEngine;
using UnityEngine.UI;

public class MissionCompletedUIController : MonoBehaviour
{
    [SerializeField] private GameObject missionCompletedPanel;
    [SerializeField] private Button tryAgainButton;

    private void Start()
    {
        missionCompletedPanel.SetActive(false);
        tryAgainButton.onClick.AddListener(OnTryAgainClicked);
    }

    public void ShowMissionCompleted()
    {
        missionCompletedPanel.SetActive(true);
    }

    private void OnTryAgainClicked()
    {
        GameManager.Instance.RestartLevel();
    }
}
