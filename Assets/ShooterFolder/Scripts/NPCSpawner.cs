using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Spawn", 2f, 5f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Instantiate(objectToSpawn, spawner.transform.position, Quaternion.identity);
    }
}
