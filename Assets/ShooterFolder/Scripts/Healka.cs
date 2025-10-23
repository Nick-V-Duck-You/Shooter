using UnityEngine;

public class Healka : MonoBehaviour
{
    [SerializeField] private int healAmount = 25; 
    [SerializeField] private GameObject pickupEffect;

    [SerializeField] private float rotationSpeed = 60f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = FindFirstObjectByType<PlayerStats>();
            if (playerStats == null)
                return;

            if (playerStats.GetCurrentHealth() >= 100)
                return;

            playerStats.Heal(healAmount);

            if (pickupEffect)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
