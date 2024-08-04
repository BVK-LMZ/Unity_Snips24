using UnityEngine;

public class SmashPowerUp : MonoBehaviour
{
    public PlayerController thePlayerRef;
    private PlayerSmash playerSmashScript;

    void Start()
    {
        // Find the player GameObject and get the PlayerController component
        GameObject playerObject = GameObject.FindWithTag("Player");
        thePlayerRef = playerObject.GetComponent<PlayerController>();

        if (thePlayerRef == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
        else
        {
            Debug.Log("Player GameObject found and set.");
            // Get the PlayerSmash component from the player
            playerSmashScript = playerObject.GetComponent<PlayerSmash>();

            if (playerSmashScript == null)
            {
                Debug.LogError("PlayerSmash component not found on Player GameObject!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("player enter smash power up");

        // Check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            print("Hit smash power-up!");
            // Call the Smash coroutine from the PlayerSmash component
            if (playerSmashScript != null)
            {
                StartCoroutine(playerSmashScript.Smash());
            }
        }
    }
}
