using UnityEngine;

public class MissionEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.MissionCompleted();
        }
    }

}