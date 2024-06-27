using UnityEngine;
using System.Collections;

public class SimpleSpawner : MonoBehaviour
{
    // Array to hold spawn points
    public Transform[] spawnPoints;

    // Prefab of the object to spawn
    public GameObject objectToSpawn;

    // Time interval between spawns
    public float spawnInterval = 4f;

    void Start()
    {
        // Start spawning coroutine
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            // Choose a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawn the object at the chosen spawn point
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
