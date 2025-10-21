using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int damage;
    private string healthStr;
    public Text healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthStr=""+health;
        healthText.text = healthStr;
    }

    public void GetDamage()
    {
        health -= damage;
    }
}
