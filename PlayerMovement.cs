using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Collider2D _collider2D;

    [Header("Horizontal Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _inputSmoothing = 0.1f;
    private float _smoothedHorizontalInput;

    [Header("Vertical Movement")]
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _maxJumps = 2;
    [SerializeField] private float _jumpsRemaining;

    [Header("Various Tags")]
    [SerializeField] private string _groundTag = "Ground";
    [SerializeField] private string _wallTag = "Wall";

    
    private void FixedUpdate()
    {
        FixedHorizontalMovement();
        GroundCheck();
    }

    private void Update()
    {
        UpdateHorizontalMovement();
        JumpSystem();
    }



    private void FixedHorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rb2d.velocity = new Vector2(horizontalInput * _speed, _rb2d.velocity.y);
    }

    private void UpdateHorizontalMovement()
    {
        // Smooth the horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _smoothedHorizontalInput = Mathf.Lerp(_smoothedHorizontalInput, horizontalInput, Time.deltaTime / _inputSmoothing);

        // Apply smoothed input to the rigidbody velocity
        _rb2d.velocity = new Vector2(_smoothedHorizontalInput * _speed, _rb2d.velocity.y);

        //handle flipping the sprite
        if (_smoothedHorizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_smoothedHorizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void JumpSystem()
    {
        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
            _jumpsRemaining--;
        }
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_collider2D.bounds.center, _collider2D.bounds.size, 0f);
        bool isGrounded = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(_groundTag))
            {
                isGrounded = true;
                break;
            }
        }

        // Update jumps remaining based on whether the player is grounded
        _jumpsRemaining = isGrounded ? _maxJumps : _jumpsRemaining;
    }

}
