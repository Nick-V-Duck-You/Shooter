using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public enum FireMode
    {
        Single,
        Auto
    }

    [Header("Shooting options")]
    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float spawnDistance = 1f;

    [Header("Fire settings")]
    [Range(1, 20)] public float fireRate = 5f;
    public FireMode fireMode = FireMode.Single;

    [Header("Player Rigidbody")]
    public PlayerController playerController;

    private float nextShootTime = 0f;


    void OnValidate()
    {
        fireRate = Mathf.Round(fireRate);
    }

    void Update()
    {
        switch (fireMode)
        {
            case FireMode.Single:
                if (Input.GetMouseButtonDown(0))
                    TryShoot();
                break;

            case FireMode.Auto:
                if (Input.GetMouseButton(0))
                    TryShoot();
                break;
        }
    }

    void TryShoot()
    {
        float shootInterval = 1f / fireRate;

        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 spawnPos = ray.origin + ray.direction * spawnDistance;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Collider playerCollider = playerController.GetComponent<CharacterController>();
        Collider projectileCollider = projectile.GetComponent<Collider>();
        if (playerCollider && projectileCollider)
            Physics.IgnoreCollision(projectileCollider, playerCollider);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb)
        {
            Vector3 velocity = ray.direction * shootForce;

            if (playerController != null)
            {
                Vector3 playerVelocity = playerController.CurrentVelocity;
                float forwardSpeed = Vector3.Dot(playerVelocity, ray.direction);
                velocity += ray.direction * forwardSpeed;
            }

            rb.linearVelocity = velocity;
        }
    }
}