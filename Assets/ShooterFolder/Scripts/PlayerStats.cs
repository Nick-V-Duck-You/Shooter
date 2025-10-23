using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int health = 10;
    [SerializeField] public int ammo = 10;
    [SerializeField] public int damage = 10;

    [Header("UI Elements")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealthUI();
        UpdateAmmoUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText)
            healthText.text = $"HP: {health}";
    }

    private void UpdateAmmoUI()
    {
        if (ammoText)
            ammoText.text = $"Ammo: {ammo}";
    }

    public void UseAmmo(int count)
    {
        ammo = Mathf.Max(0, ammo - count);
        UpdateAmmoUI();
    }
    public void GetDamage()
    {
        health -= damage;
        if (health < 0)
            health = 0;

        UpdateHealthUI();

        if (health == 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
        GameManager.Instance.GameOver();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(100, health + amount);
        UpdateHealthUI();
    }
    public int GetCurrentHealth()
    {
        return health;
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        UpdateAmmoUI();
    }
}
