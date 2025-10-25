using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public GameObject playerObject;
    public Transform[] waypoints;

    private float speed;
    public float viewDistance = 20f;
    public float viewAngle = 180f;
    public float eyeHeight = 0.3f;

    private int currentWaypointIndex = 0;
    private int direction = 1;
    private bool chasingPlayer = false;

    public float memoryDuration = 3f;
    private float lastTimePlayerSeen = -Mathf.Infinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        playerObject = GameObject.FindWithTag("PlayerBody");
        speed = this.gameObject.GetComponent<EnemyStats>().speed;
    }

    void Update()
    {
        if (playerObject == null)
            return;

        Vector3 eyes = transform.position + Vector3.up * eyeHeight;
        Vector3 toPlayer = playerObject.transform.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        bool playerVisible = false;

        // Check if the NPC can see the player
        if (distanceToPlayer <= viewDistance)
        {
            Vector3 dirToPlayer = toPlayer.normalized;
            float dot = Vector3.Dot(transform.forward, dirToPlayer);

            if (dot > Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
            {
                if (Physics.Raycast(eyes, dirToPlayer, out RaycastHit hit, viewDistance))
                {
                    if (hit.collider.CompareTag("PlayerBody") || hit.collider.CompareTag("Player"))
                    {
                        playerVisible = true;
                        lastTimePlayerSeen = Time.time;
                    }
                }
            }
        }

        bool recentlySeen = Time.time - lastTimePlayerSeen <= memoryDuration;

        // Start chasing the player when detected
        if (!chasingPlayer && (playerVisible || recentlySeen))
        {
            chasingPlayer = true;
            Debug.Log($"[{name}] Начал преследовать игрока!");
        }

        if (chasingPlayer && !playerVisible && !recentlySeen)
        {
            chasingPlayer = false;
            Debug.Log($"[{name}] Потерял игрока из виду.");
        }

        if (chasingPlayer)
            MoveTo(playerObject.transform.position);
        else
            Patrol();
    }

    // Handles waypoint patrol movement
    void Patrol()
    {
        if (waypoints == null || waypoints.Length == 0) return;

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

    //visible only in scene mode
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 eyes = transform.position + Vector3.up * eyeHeight;

        // Central ray
        Gizmos.DrawLine(eyes, eyes + transform.forward * viewDistance);

        // Edges of the angle
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;
        Gizmos.DrawLine(eyes, eyes + leftBoundary * viewDistance);
        Gizmos.DrawLine(eyes, eyes + rightBoundary * viewDistance);

        // Radius
        Gizmos.color = new Color(1, 1, 0, 0.1f);
        Gizmos.DrawWireSphere(eyes, viewDistance);
    }
}
