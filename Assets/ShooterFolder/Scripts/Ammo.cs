using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 20;
    
    [SerializeField] private float rotationSpeed = 60f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = FindFirstObjectByType<PlayerStats>();
            if (playerStats == null)
                return;

            playerStats.AddAmmo(ammoAmount);

            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
