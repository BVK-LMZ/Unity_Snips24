using UnityEngine;

public class PlayerJumpMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Collider2D _collider2D;

    [Header("Vertical Movement")]
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private int _maxJumps = 2;
    private int _jumpsRemaining;

    [Header("Various Tags")]
    [SerializeField] private string _groundTag = "Ground";

    private bool _isGrounded = false;
    private float _groundCheckCooldown = 0.1f;
    private float _lastGroundCheckTime;


    public bool IsJumping()
    {
        return Input.GetKeyDown(KeyCode.W) && _jumpsRemaining == _maxJumps - 1;
    }

    public bool IsDoubleJumping()
    {
        return Input.GetKeyDown(KeyCode.W) && _jumpsRemaining == _maxJumps - 2;
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }


    private void Start()
    {
        _jumpsRemaining = _maxJumps;
        Debug.Log("Game Started. Jumps remaining: " + _jumpsRemaining);
    }

    private void Update()
    {
        if (Time.time - _lastGroundCheckTime >= _groundCheckCooldown)
        {
            GroundCheck();
        }
        JumpSystem();
    }

    private void JumpSystem()
    {
        if (Input.GetKeyDown(KeyCode.W) && _jumpsRemaining > 0)
        {
            _jumpsRemaining--;
            Debug.Log("Jump initiated. Jumps remaining before jump: " + _jumpsRemaining);
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
            Debug.Log("Jumps remaining after jump: " + _jumpsRemaining);
        }
    }

    private void GroundCheck()
    {
        _lastGroundCheckTime = Time.time;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_collider2D.bounds.center, _collider2D.bounds.size, 0f);
        bool wasGrounded = _isGrounded;
        _isGrounded = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(_groundTag))
            {
                _isGrounded = true;
                break;
            }
        }

        if (_isGrounded && !wasGrounded)
        {
            Debug.Log("Grounded - resetting jumps to: " + _maxJumps);
            _jumpsRemaining = _maxJumps;
        }

        Debug.Log(_isGrounded ? "Grounded. Jumps remaining: " + _jumpsRemaining : "Not grounded. Jumps remaining: " + _jumpsRemaining);
    }

    private void OnDrawGizmos()
    {
        // Draw the ground check box in the editor for debugging purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider2D.bounds.center, _collider2D.bounds.size);
    }
}
