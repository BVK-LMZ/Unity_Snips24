using UnityEngine;

public class PlayerJumpMovement : MonoBehaviour
{
    [Header("Player Components")]
    // Reference to the Rigidbody2D component for applying physics
    [SerializeField] private Rigidbody2D _rb2d;
    // Reference to the Collider2D component for collision detection
    [SerializeField] private Collider2D _collider2D;

    [Header("Vertical Movement")]
    // The amount of force applied when the player jumps
    [SerializeField] private float _jumpPower = 5f;
    // The maximum number of jumps allowed (including double jumps)
    [SerializeField] private int _maxJumps = 2;
    // Tracks the number of jumps remaining before needing to land on the ground
    public int _jumpsRemaining;

    [Header("Various Tags")]
    // Tag used to identify ground objects
    [SerializeField] private string _groundTag = "Ground";

    // Indicates whether the player is currently touching the ground
    private bool _isGrounded = false;
    // Time cooldown for checking if the player is grounded
    private float _groundCheckCooldown = 0.1f;
    // Timestamp of the last ground check
    private float _lastGroundCheckTime;

    // Method to check if the player is trying to jump
    public bool IsJumping()
    {
        // Returns true if the W key is pressed and the player has just enough jumps left
        return Input.GetKeyDown(KeyCode.W) && _jumpsRemaining == _maxJumps - 1;
    }

    // Method to check if the player is trying to perform a double jump
    public bool IsDoubleJumping()
    {
        // Returns true if the W key is pressed and the player has one jump left
        return Input.GetKeyDown(KeyCode.W) && _jumpsRemaining == _maxJumps - 2;
    }

    // Method to check if the player is currently grounded
    public bool IsGrounded()
    {
        return _isGrounded;
    }

    private void Start()
    {
        // Initialize the number of jumps remaining to the maximum allowed
        _jumpsRemaining = _maxJumps;
    }

    private void Update()
    {
        // Only perform ground check if the cooldown period has elapsed
        if (Time.time - _lastGroundCheckTime >= _groundCheckCooldown)
        {
            GroundCheck();
        }

        // Handle the jumping logic
        JumpSystem();
    }

    private void JumpSystem()
    {
        // Check if the W key is pressed and there are jumps remaining
        if (Input.GetKeyDown(KeyCode.W) && _jumpsRemaining > 0)
        {
            // Decrement the count of jumps remaining
            _jumpsRemaining--;
            // Apply upward force to the player's Rigidbody2D to perform the jump
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
        }
    }

    private void GroundCheck()
    {
        // Update the timestamp of the last ground check
        _lastGroundCheckTime = Time.time;

        // Get all colliders that overlap with the player's collider
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_collider2D.bounds.center, _collider2D.bounds.size, 0f);

        // Keep track of the previous grounded state
        bool wasGrounded = _isGrounded;
        // Assume the player is not grounded until proven otherwise
        _isGrounded = false;

        // Check each collider to see if it is tagged as ground
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(_groundTag))
            {
                _isGrounded = true;
                break; // Exit the loop if the ground is detected
            }
        }

        // If the player has just landed on the ground, reset the jumps remaining
        if (_isGrounded && !wasGrounded)
        {
            _jumpsRemaining = _maxJumps;
        }
    }
}
