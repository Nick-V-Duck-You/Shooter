using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject particleSystemObject;
    public Transform[] waypoints;

    public float speed = 5.0f;
    public float viewDistance = 10f;
    public float viewAngle = 60f;

    private int currentWaypointIndex = 0;
    private int direction = 1;
    private bool chasingPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        playerObject = GameObject.FindWithTag("PlayerBody");
    }

    void Update()
    {
        if (playerObject == null)
            return;

        Vector3 toPlayer = playerObject.transform.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        bool playerVisible = false;

        // Check if the NPC can see the player
        if (distanceToPlayer < viewDistance)
        {
            Vector3 dirToPlayer = toPlayer.normalized;
            float dot = Vector3.Dot(transform.forward, dirToPlayer);
            if (dot > Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
                playerVisible = true;
        }

        // Start chasing the player when detected
        if (!chasingPlayer && playerVisible)
        {
            chasingPlayer = true;
            Debug.Log($"[{name}] Player detected!");
        }
        // Stop chasing when the player is out of sight
        else if (chasingPlayer && !playerVisible && distanceToPlayer > viewDistance)
        {
            chasingPlayer = false;
            Debug.Log($"[{name}] Lost sight of the Player.");
        }

        // Move depending on the current state
        if (chasingPlayer)
            MoveTo(playerObject.transform.position);
        else
            Patrol();
    }

    // Handles waypoint patrol movement
    void Patrol()
    {
        if (waypoints.Length == 0) return;

        MoveTo(waypoints[currentWaypointIndex].position);

        // Check if NPC reached the current waypoint
        if ((transform.position - waypoints[currentWaypointIndex].position).sqrMagnitude < 0.09f)
        {
            currentWaypointIndex += direction;

            // Reverse direction when reaching the last waypoint
            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                direction *= -1;
                currentWaypointIndex += direction * 2;
            }
        }
    }

    // Move NPC towards the target and rotate
    void MoveTo(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;

        if (toTarget.sqrMagnitude > 0.0001f)
        {
            Vector3 dir = toTarget.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }



    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pos = transform.position;
            Quaternion customRotation = Quaternion.Euler(-90f, 0f, 0f);
            Destroy(this.gameObject);
            Destroy(Instantiate(particleSystemObject, this.gameObject.transform.position, customRotation), 5);
            return;
        }
    }
}
