using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed at which the player moves
    [SerializeField] private float moveSpeed = 5f;
    // Reference to the boundary object (the stretched-out square)
    [SerializeField] private GameObject boundary;
    // Reference to the player's transform
    [SerializeField] private Transform playerTransform;

    // Boundary limits calculated from the square's dimensions
    private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the player's transform
        playerTransform = this.transform;
        // Calculate the boundaries based on the square's position and size
        CalculateBoundaries();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get player input for movement
        Vector2 input = GetInput();
        // Move the player based on input
        MovePlayer(input);
        // Restrict player movement within boundaries
        RestrictMovementWithinBounds();
        // Handle teleporting the player when they reach the camera's horizontal bounds
        HandleHorizontalWrapAround();
    }

    // Function to calculate the boundaries based on the boundary object's size and position
    private void CalculateBoundaries()
    {
        // Get the position and size of the boundary object (square)
        Vector2 boundaryPos = boundary.transform.position;
        Vector2 boundarySize = boundary.GetComponent<SpriteRenderer>().bounds.size;

        // Calculate the min and max limits for X and Y
        minX = boundaryPos.x - boundarySize.x / 2;
        maxX = boundaryPos.x + boundarySize.x / 2;
        minY = boundaryPos.y - boundarySize.y / 2;
        maxY = boundaryPos.y + boundarySize.y / 2;
    }

    // Function to get the player's input from WASD keys
    private Vector2 GetInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Get input for horizontal movement (A and D keys)
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Get input for vertical movement (W and S keys)
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;

        // Return the input as a normalized vector to prevent faster diagonal movement
        return new Vector2(moveX, moveY).normalized;
    }

    // Function to move the player based on input
    private void MovePlayer(Vector2 input)
    {
        // Calculate the movement vector
        Vector2 movement = input * moveSpeed * Time.deltaTime;
        // Apply the movement to the player's position
        playerTransform.Translate(movement);
    }

    // Function to restrict player movement within the calculated boundaries
    private void RestrictMovementWithinBounds()
    {
        // Get the player's current position
        Vector3 pos = playerTransform.position;

        // Clamp the player's position within the boundary limits
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Apply the clamped position back to the player
        playerTransform.position = pos;
    }

    // Function to handle teleporting the player when they reach the camera's horizontal bounds
    private void HandleHorizontalWrapAround()
    {
        // Get the player's current position
        Vector3 pos = playerTransform.position;

        // Check if the player has hit the left or right boundary
        if (pos.x <= minX)
        {
            // Teleport the player to the opposite side (right)
            pos.x = maxX;
        }
        else if (pos.x >= maxX)
        {
            // Teleport the player to the opposite side (left)
            pos.x = minX;
        }

        // Apply the updated position back to the player
        playerTransform.position = pos;
    }
}
