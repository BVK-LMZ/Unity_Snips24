using UnityEngine;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // References to other components required for animation control
    // PlayerJumpMovement handles the jumping logic
    public PlayerJumpMovement _jumpMovement;
    // PlayerHorizontalMovement handles horizontal movement input
    public PlayerHorizontalMovement _horizontalMovement;
    // Animator component for controlling animations
    public Animator _animator;

    // Enum to define different player states for animation purposes
    private enum PlayerState
    {
        IDLE,   // Player is standing still
        MOVE,   // Player is moving horizontally
        JUMP    // Player is in the air (jumping)
    }

    // Variable to store the current player state
    private PlayerState _currentState;

    private void Start()
    {
        // Uncomment and use these lines if you want to automatically fetch the components
        // _jumpMovement = GetComponent<PlayerJumpMovement>();
        // _horizontalMovement = GetComponent<PlayerHorizontalMovement>();
        // _animator = GetComponent<Animator>();

        // Initialize the player's state to IDLE at the start
        _currentState = PlayerState.IDLE;
    }

    private void Update()
    {
        // Update the player state based on current conditions
        UpdatePlayerState();
        // Apply animations based on the updated state
        ApplyAnimations();
    }

    private void UpdatePlayerState()
    {
        // Check if the player is in the air and has no jumps remaining
        if (!_jumpMovement.IsGrounded() && _jumpMovement._jumpsRemaining <= 0)
        {
            _currentState = PlayerState.JUMP; // Set state to JUMP if in air
        }
        // Check if there is horizontal movement
        else if (Mathf.Abs(_horizontalMovement.GetHorizontalInput()) > 0.1f)
        {
            _currentState = PlayerState.MOVE; // Set state to MOVE if moving horizontally
        }
        else
        {
            _currentState = PlayerState.IDLE; // Set state to IDLE if not moving
        }
    }

    private void ApplyAnimations()
    {
        // Apply the appropriate animation based on the current state
        switch (_currentState)
        {
            case PlayerState.IDLE:
                _animator.Play("Idle"); // Play idle animation
                break;
            case PlayerState.MOVE:
                _animator.Play("Walk"); // Play walking animation
                break;
            case PlayerState.JUMP:
                _animator.Play("Jump"); // Play jumping animation
                break;
        }
    }
}
