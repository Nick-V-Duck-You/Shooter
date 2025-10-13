using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject =  GameObject.FindWithTag("PlayerBody");
    }

    void Update()
    {
      float speed = 5f;
      Vector3 targetPosition = playerObject.transform.position;

      transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


    }

    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
