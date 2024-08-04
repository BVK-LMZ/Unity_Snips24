using UnityEngine;
using System.Collections;

public class SpawningManager : MonoBehaviour
{
    // The prefabs to spawn
    public GameObject Tango1;
    public GameObject Tango2;

    // An array of possible spawn points
    public Transform[] spawnPoints;

    // Time interval between spawns (in seconds)
    public float spawnInterval = 2f;

    // A timer to track the spawn interval
    private float timer;

    // Range for random spawn positions
    public float spawnRangeX = 10f;
    public float spawnRangeZ = 10f;

    // Probability of spawning Tango2 (15%)
    public float tango2Probability = 0.15f;

    // This function is called when the script starts
    private void Start()
    {
        // Initialize the timer to the spawn interval
        timer = spawnInterval;

        // Instantiate a new enemyPrefab at a predetermined location
        Vector3 initialSpawnPos = GenerateSpawnPosition();
        Instantiate(Tango2, initialSpawnPos, Quaternion.identity);

        // Start the coroutine to handle spawning
        StartCoroutine(SpawnObjectCoroutine());
    }

    // Coroutine to handle spawning objects at regular intervals
    private IEnumerator SpawnObjectCoroutine()
    {
        while (true)
        {
            // Wait for the spawn interval to pass
            yield return new WaitForSeconds(spawnInterval);

            // Call the function to spawn the object
            SpawnObject();
        }
    }

    // Function to spawn the object at a random spawn point
    private void SpawnObject()
    {
        Vector3 spawnPos = GenerateSpawnPosition();

        // Determine which prefab to spawn based on the probability
        GameObject prefabToSpawn = Random.value < tango2Probability ? Tango2 : Tango1;

        // Instantiate (create) the selected prefab at the generated spawn position
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    // Function to generate a random spawn position
    private Vector3 GenerateSpawnPosition()
    {
        float randomPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomPos = new Vector3(randomPosX, 0, randomPosZ);

        return randomPos;
    }
}
