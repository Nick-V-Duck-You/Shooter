using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    int i;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Instantiate(objectToSpawn, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        i=i+1;
    }
}
