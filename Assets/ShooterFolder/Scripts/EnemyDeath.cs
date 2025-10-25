using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject particleSystemObject;

    public GameObject canvas;
    private int damage;
    private int health;

    
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        health = this.gameObject.GetComponent<EnemyStats>().health;

    }

    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Bullet"))
        {
            var playerStats = canvas.GetComponent<PlayerStats>();
            int bulletDamage = playerStats.damage;

            health -= bulletDamage;
            Debug.Log($"{gameObject.name} получил {bulletDamage} урона. Осталось HP: {health}");

            if (health <= 0){
                playerStats.AddKill();
                Destroy(this.gameObject);
            }
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pos = transform.position;
            Quaternion customRotation = Quaternion.Euler(-90f, 0f, 0f);
            Destroy(this.gameObject);
            Destroy(Instantiate(particleSystemObject, this.gameObject.transform.position, customRotation), 5);

            damage = this.gameObject.GetComponent<EnemyStats>().damage;
            var playerHealth = canvas.GetComponent<PlayerStats>();
            canvas.GetComponent<PlayerStats>().damage = damage;
            playerHealth.GetDamage();

            return;
        }
    }
}
