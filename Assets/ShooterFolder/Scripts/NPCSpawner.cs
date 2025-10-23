using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectToSpawn;
    [SerializeField] private GameObject[] spawner;
    //[SerializeField] private int wayCount;
    [SerializeField] public Transform[] waypoints; 

    private GameObject randomSpawner;
    private GameObject randomObjectToSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Randomize", 2f, 5f); 
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Randomize()
    {
        //выбираем спавнер
        randomSpawner = spawner[Random.Range(0,2)];
        // тут должен быть не this, надо брать его со сгенеренного спавнера
        waypoints = randomSpawner.gameObject.GetComponent<WaypointsArray>().waypoints;
        randomObjectToSpawn = objectToSpawn[Random.Range(0,4)];
        Spawn();

    }

    public void Spawn()
    {
        

        var npcmovement = randomObjectToSpawn.GetComponent<NPCMovement>();
        npcmovement.waypoints = waypoints;

        Instantiate(randomObjectToSpawn, randomSpawner.transform.position, Quaternion.identity);
    }
}
