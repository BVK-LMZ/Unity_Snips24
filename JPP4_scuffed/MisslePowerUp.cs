using UnityEngine;

public class MisslePowerUp : MonoBehaviour
{
    public PlayerController thePlayerRef;
    public GameObject missilePrefab;

    public int numberOfMissiles = 4;
    public float spacing = 2f; // Space between missiles

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        thePlayerRef = playerObject.GetComponent<PlayerController>();


        if (thePlayerRef == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
        else
        {
            Debug.Log("Player GameObject found and set.");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            print("hit  missle up!");
            FireMissile();
        }
    }

    public void FireMissile()
    {
        for (int i = 0; i < numberOfMissiles; i++)
        {
            // Calculate the offset for each missile
            Vector3 offset = transform.right * (i - (numberOfMissiles - 1) / 2f) * spacing;

            // Create a rotation with a z angle of -90 degrees
            Quaternion rotation = Quaternion.Euler(0, 0, -90);

            // Instantiate the missile
            GameObject missile = Instantiate(missilePrefab, transform.position + offset, Quaternion.identity);

            // Apply the desired rotation to the missile
            missile.transform.rotation = rotation;
        }
    }
}