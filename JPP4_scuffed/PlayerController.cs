using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the Rigidbody component of the player
    public Rigidbody playerRb;
    // Reference to the focal point, used for forward direction
    public GameObject _focalPoint;
    // Speed at which the player moves
    public float speed = 5.0f;




    public bool hasPowerup = false; // Assuming you have a boolean to check if the player has a powerup
    public float StandardStrength = 2.5f; // Adjust this value as needed
    public float powerupStrength = 15.0f; // Adjust this value as needed


    public GameObject _StrVisual;

    // Method to show the visual
    public void StrVisual_Show()
    {
        if (_StrVisual != null)
        {
            _StrVisual.SetActive(true);
        }
    }

    // Method to hide the visual
    public void StrVisual_Hide()
    {
        if (_StrVisual != null)
        {
            _StrVisual.SetActive(false);
        }
    }











    //MB


    private void Start()
    {
        StrVisual_Hide();
    }


    void Update()
    {
        // Check for W key press
        if (Input.GetKey(KeyCode.W))
        {
            // Move player forward
            playerRb.AddForce(_focalPoint.transform.forward * speed);
        }
        // Check for S key press
        else if (Input.GetKey(KeyCode.S))
        {
            // Move player backward
            playerRb.AddForce(-_focalPoint.transform.forward * speed);
        }
        // Check for A key press
        if (Input.GetKey(KeyCode.A))
        {
            // Move player left
            playerRb.AddForce(-_focalPoint.transform.right * speed);
        }
        // Check for D key press
        else if (Input.GetKey(KeyCode.D))
        {
            // Move player right
            playerRb.AddForce(_focalPoint.transform.right * speed);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy and the player has a powerup
        if (collision.gameObject.CompareTag("Tango") && hasPowerup)
        {
            Debug.Log("Collided with Enemy while having a powerup!");

            // Get the Rigidbody component of the enemy
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                // Calculate the direction away from the player
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                // Add an impulse force to the enemy
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Tango") && !hasPowerup)
        {
            Debug.Log("Collided with Enemy standard");

            // Get the Rigidbody component of the enemy
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                // Calculate the direction away from the player
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                // Add an impulse force to the enemy
                enemyRigidbody.AddForce(awayFromPlayer * StandardStrength, ForceMode.Impulse);
            }
        }

    }
}