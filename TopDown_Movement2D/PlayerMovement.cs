using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rb2d; // Reference to the tank's Rigidbody component
    [SerializeField] private BoxCollider2D _col2d; // Reference to the tank's BoxCollider2D component

    [Header("Movement Customization")]
    [SerializeField] private float _movementSpeed = 5.0f; // Movement speed of the tank
    [SerializeField] private float _inputSmoothing = 0.1f; // Smoothing factor for input


    // Private variables
    private Vector2 currentVelocity = Vector2.zero; // Current velocity of tank movement



    // Function to handle tank movement based on player input
    private void HandleMovementInput()
    {
        // Get input from W, A, S, D keys for tank movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Smooth input
        float smoothHorizontalInput = Mathf.SmoothDamp(_rb2d.velocity.x, horizontalInput * _movementSpeed, ref currentVelocity.x, _inputSmoothing);
        float smoothVerticalInput = Mathf.SmoothDamp(_rb2d.velocity.y, verticalInput * _movementSpeed, ref currentVelocity.y, _inputSmoothing);

        // Create movement direction vector based on player input
        Vector2 movementDirection = new Vector2(smoothHorizontalInput, smoothVerticalInput).normalized;

        // Move the tank based on input
        MovePlayer(movementDirection);
    }

    // Function to move the tank
    private void MovePlayer(Vector2 direction)
    {
        // Calculate movement based on direction and speed
        Vector2 movement = direction * _movementSpeed * Time.fixedDeltaTime;

        // Move the tank using Rigidbody2D
        _rb2d.MovePosition(_rb2d.position + movement);
    }


    // MonoBehaviour 
    void Start()
    {
        // Get reference to the Rigidbody2D component
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovementInput(); // Handle tank movement based on player input

    }
}
