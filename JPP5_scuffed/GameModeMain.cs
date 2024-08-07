using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Include this namespace to use SceneManager


public class GameModeMain : MonoBehaviour
{
    public List<GameObject> targets; // List of prefabs to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public GameObject spawnArea; // The GameObject representing the area along the x-axis
    public bool _bSpawnObj = false;

    private float timeSinceLastSpawn; // Timer to track time since the last spawn
    private float minX; // Minimum x position for spawning
    private float maxX; // Maximum x position for spawning
    private float spawnY; // Y position for spawning

    // New variables
    private float minSpeed = 10f;
    private float maxSpeed = 35f;
    private float maxTorque = 10f;
    private float ySpawnPos = 1f; // Example value for Y spawn position

    //buttons
    public Button _start;
    public GameObject _StartCanvas;

    private void OnStartClick()
    {
      
        _StartCanvas.SetActive(false);
        _bSpawnObj = true;
    }


    void Start()
    {
        if (_start != null)
        {
            _start.onClick.AddListener(OnStartClick);
           
        }
        // Calculate the min and max x positions based on the spawnArea's size
        Vector3 spawnAreaSize = spawnArea.GetComponent<Renderer>().bounds.size;
        minX = spawnArea.transform.position.x - spawnAreaSize.x / 2;
        maxX = spawnArea.transform.position.x + spawnAreaSize.x / 2;
        spawnY = spawnArea.transform.position.y; // Assuming spawn happens at the spawnArea's y position

        // Initialize timeSinceLastSpawn to ensure immediate spawn on start
        timeSinceLastSpawn = spawnInterval;

        // Optional: Call these methods to test or set default values
        // RandomForce();
        // RandomTorque();
        // RandomSpawnPos();
    }

    void Update()
    {
        if (_bSpawnObj)
        {
            // Accumulate the time elapsed since the last frame
            timeSinceLastSpawn += Time.deltaTime;

            // Check if the accumulated time exceeds the spawn interval
            if (timeSinceLastSpawn >= spawnInterval)
            {
                // Spawn an object and reset the timer
                SpawnObject();
                timeSinceLastSpawn = 0f;
            }
        }

        //r 2 reset
        {
            // Check if the "R" key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Get the current scene
                Scene currentScene = SceneManager.GetActiveScene();

                // Reload the current scene
                SceneManager.LoadScene(currentScene.name);
            }
        }
    }

    void SpawnObject()
    {
        // Choose a random x position within the bounds of the spawn area
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = RandomSpawnPos(); // Get a random spawn position

        // Choose a random prefab from the targets list
        GameObject prefabToSpawn = targets[Random.Range(0, targets.Count)];

        // Instantiate the chosen prefab at the calculated spawn position
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Apply force and torque to the spawned object
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(RandomForce(), ForceMode.Impulse);
            rb.AddTorque(RandomTorque(), ForceMode.Impulse);
        }
    }

    Vector3 RandomForce()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        return new Vector3(0, speed, 0); // Apply force upwards
    }

    Vector3 RandomTorque()
    {
        return new Vector3(
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque)
        );
    }

    Vector3 RandomSpawnPos()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = spawnY + Random.Range(-ySpawnPos, ySpawnPos);
        return new Vector3(randomX, randomY, spawnArea.transform.position.z);
    }
}
