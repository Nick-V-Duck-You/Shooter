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
    [SerializeField] private Text killText;

    private int killCount = 0;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealthUI();
        UpdateAmmoUI();
        UpdateKillUI();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
                DisableInvincibility();
        }
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

    private void UpdateKillUI()
    {
        if (killText)
            killText.text = $"Kills: {killCount}";
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public void UseAmmo(int count)
    {
        ammo = Mathf.Max(0, ammo - count);
        UpdateAmmoUI();
    }
    public void GetDamage()
    {
        if (isInvincible) return;

        health -= damage;
        if (health < 0)
            health = 0;

        UpdateHealthUI();

        if (health == 0)
            Die();
    }

    public void EnableInvincibility(float duration)
    {
        isInvincible = true;
        invincibleTimer = duration;
        Debug.Log("INVINCIBLE!");
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
        invincibleTimer = 0f;
        Debug.Log("LOH!");
    }
    private void Die()
    {
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
