using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject playerObject;
    public GameObject particleSystemObject;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject =  GameObject.FindWithTag("PlayerBody");
    }

    void Update()
    {
      //Vector3 targetPosition = playerObject.transform.position;

      //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
      
      
      if (currentWaypointIndex < waypoints.Length)

     {
            // current waypoint
            Transform target = waypoints[currentWaypointIndex];

            // move to waypoint
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // rotate to movement direction
            Vector3 direction = target.position - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            }

            // chek if npc reach current waypoint
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
     }
     
     else{
         while (currentWaypointIndex != 0)
         {
             // current waypoint
            Transform target = waypoints[currentWaypointIndex];

            // move to waypoint
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // rotate to movement direction
            Vector3 direction = target.position - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            }

            // chek if npc reach current waypoint
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex -=1 ;
            }
         }
     }


    }

    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Quaternion customRotation = Quaternion.Euler(-90f, 0f, 0f);
            Destroy(this.gameObject);
            Destroy(Instantiate(particleSystemObject, this.gameObject.transform.position, customRotation), 5);
        }
    }
}
