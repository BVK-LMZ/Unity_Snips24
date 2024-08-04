using UnityEngine;
using System.Collections;

public class STR : MonoBehaviour
{
    public PlayerController thePlayerRef;

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
            print("hit str power up!");
            StartCoroutine(PowerUpRoutine());
        }
    }

    private IEnumerator PowerUpRoutine()
    {
        print("hello from str power up");

            thePlayerRef.hasPowerup = true; // Set the powerup to true
            Debug.Log("Powerup activated!");
            thePlayerRef.StrVisual_Show();
            // Wait for 5 seconds
            yield return new WaitForSeconds(5);

            thePlayerRef.hasPowerup = false; // Set the powerup to false
            thePlayerRef.StrVisual_Hide();
            Debug.Log("Powerup deactivated!");
       
    }
}
