using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public GameObject plane; // The plane that covers the area
    public int numberOfPrefabs = 10; // Number of prefabs to spawn

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnPlane();
            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionOnPlane()
    {
        // Get the plane's bounds
        Bounds planeBounds = GetPlaneBounds();

        // Generate a random position within the plane's bounds
        float randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        float randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);

        // Generate a random Y position within the plane's bounds if vertical bounds are considered
        float randomY = Random.Range(planeBounds.min.y, planeBounds.max.y);

        return new Vector3(randomX, randomY, randomZ);
    }

    Bounds GetPlaneBounds()
    {
        // Adjust this method according to how you determine the plane's bounds
        // This assumes the plane has a collider component
        Collider planeCollider = plane.GetComponent<Collider>();
        if (planeCollider != null)
        {
            return planeCollider.bounds;
        }
        else
        {
            Debug.LogError("Plane does not have a Collider component!");
            return new Bounds(Vector3.zero, Vector3.zero);
        }
    }
}
