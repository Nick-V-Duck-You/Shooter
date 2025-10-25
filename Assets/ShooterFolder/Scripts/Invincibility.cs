using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private float rotationSpeed = 60f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = FindFirstObjectByType<PlayerStats>();
            if (playerStats == null) return;

            playerStats.EnableInvincibility(duration);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}