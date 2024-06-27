using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;          // Jump force applied to the player.
    public bool isDead = false;            // Boolean to check if the player is dead.

    public Rigidbody rb;                   // Reference to the Rigidbody component.
    private bool isGrounded = false;       // Boolean to check if the player is grounded.
    private int jumpsLeft = 2;             // Maximum number of jumps allowed.

    void Update()
    {
        // Check if the player is not dead and can jump.
        if (!isDead && (isGrounded || jumpsLeft > 0))
        {
            // Check if the spacebar is pressed.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(jumpForce); // Perform jump.
                jumpsLeft--;    // Decrease jumps left.
            }
        }
    }

    // Method to perform a jump.
    void Jump(float force)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset vertical velocity to avoid additive forces.
        rb.AddForce(Vector3.up * force, ForceMode.Impulse); // Apply jump force.
        isGrounded = false; // Player is no longer grounded after jumping.
    }

    // Method to detect collision with ground or other objects.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is grounded.
            jumpsLeft = 2;     // Reset jumps left on ground contact.
        }
    }

    // Method to handle character death.
    public void Die()
    {
        isDead = true; // Set player to dead.
        // Additional logic for death such as disabling controls, showing game over screen, etc.
    }
}
