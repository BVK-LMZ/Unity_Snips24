using UnityEngine; // Import the UnityEngine namespace, which includes essential classes for Unity development.

public class PlayerHorizontalMovement : MonoBehaviour
{
    // Header in the Unity Inspector to categorize the player component fields for better organization.
    [Header("Player Components")]
    // Private serialized field to reference the Rigidbody2D component attached to the player.
    // The [SerializeField] attribute allows this private field to be modified in the Unity Inspector.
    [SerializeField] private Rigidbody2D _rb2d;

    // Header in the Unity Inspector to categorize the horizontal movement fields for better organization.
    [Header("Horizontal Movement")]
    // Private serialized field to control the movement speed of the player.
    // The default speed value is set to 5f, which can be adjusted in the Unity Inspector.
    [SerializeField] private float _speed = 5f;

    // Private serialized field to control the smoothing of horizontal input for a more fluid movement experience.
    // The default smoothing value is set to 0.1f, which can be adjusted in the Unity Inspector.
    [SerializeField] private float _inputSmoothing = 0.1f;

    // Private field to store the smoothed horizontal input value.
    // This value is updated each frame to provide smooth horizontal movement.
    private float _smoothedHorizontalInput;

    // The Update method is called once per frame.
    // It is used here to update the player's horizontal movement every frame.
    private void Update()
    {
        // Call the UpdateHorizontalMovement method to handle the player's horizontal movement logic.
        UpdateHorizontalMovement();
    }

    // The Start method is called before the first frame update.
    // It is used here to initialize the Rigidbody2D component.
    private void Start()
    {
        // Get the Rigidbody2D component attached to the player and assign it to the _rb2d field.
        _rb2d = GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D component was successfully found and assigned.
        // If the component is not null, print a message to the console indicating that it should work correctly.
        if (_rb2d != null) { print("Rigidbody2D component assigned successfully."); }
    }

    // Method responsible for updating the player's horizontal movement based on input.
    // This method is called every frame inside the Update method.
    private void UpdateHorizontalMovement()
    {
        // Get the raw horizontal input from the player (using the "Horizontal" axis defined in Unity's Input settings).
        // This value will be -1 for left movement, 1 for right movement, and 0 for no movement.
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Smooth the horizontal input using linear interpolation (Lerp) to make the movement more fluid.
        // The smoothed value is calculated based on the previous smoothed input, the current raw input,
        // and the smoothing factor adjusted by deltaTime to account for frame rate.
        _smoothedHorizontalInput = Mathf.Lerp(_smoothedHorizontalInput, horizontalInput, Time.deltaTime / _inputSmoothing);

        // Update the Rigidbody2D's velocity to apply the smoothed horizontal movement.
        // The velocity is set as a new Vector2, combining the smoothed horizontal input multiplied by speed
        // with the current vertical velocity to maintain any vertical movement (e.g., jumping).
        _rb2d.velocity = new Vector2(_smoothedHorizontalInput * _speed, _rb2d.velocity.y);

        // Check if the smoothed horizontal input is positive, indicating movement to the right.
        if (_smoothedHorizontalInput > 0)
        {
            // Set the player's local scale to (1, 1, 1) to ensure the player faces right.
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Check if the smoothed horizontal input is negative, indicating movement to the left.
        else if (_smoothedHorizontalInput < 0)
        {
            // Set the player's local scale to (-1, 1, 1) to ensure the player faces left.
            // Flipping the x-axis of the scale mirrors the sprite horizontally.
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Determine if the player is currently walking by checking if the absolute value of the smoothed input
        // is greater than a small threshold (0.1f). This helps to filter out very small movements and noise.
        bool isWalking = Mathf.Abs(_smoothedHorizontalInput) > 0.1f;

        // The 'isWalking' variable could be used later to trigger animations or other behavior if needed.
    }

    // Public method to get the raw horizontal input from the player.
    // This method allows other scripts to access the player's horizontal input if needed.
    public float GetHorizontalInput()
    {
        // Return the raw horizontal input value using the "Horizontal" axis defined in Unity's Input settings.
        return Input.GetAxisRaw("Horizontal");
    }
}
