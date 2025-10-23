using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int health;
    public int ammo;
    public int damage;
    private string healthStr;
    public Text healthText;

    private string ammoStr;
    public Text ammoText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;

        ammo = 50;
    }

    // Update is called once per frame
    void Update()
    {
        healthStr=""+health;
        healthText.text = healthStr;

        ammoStr=""+ammo;
        ammoText.text = ammoStr;
    }

    public void GetDamage()
    {
        health -= damage;
    }
}
