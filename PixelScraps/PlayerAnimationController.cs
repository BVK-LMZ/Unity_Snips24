using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerJumpMovement _jumpMovement;
    [SerializeField] private PlayerHorizontalMovement _horizontalMovement;

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        bool isWalking = Mathf.Abs(_horizontalMovement.GetHorizontalInput()) > 0.1f;
        bool isGrounded = _jumpMovement.IsGrounded();

        // Update walking and grounded state
        _animator.SetBool("IsWalking", isWalking);
        _animator.SetBool("IsGrounded", isGrounded);

        // Handle jumping and double jumping with triggers
        if (_jumpMovement.IsJumping())
        {
            _animator.SetTrigger("Jump");
        }
        else if (_jumpMovement.IsDoubleJumping())
        {
            _animator.SetTrigger("DoubleJump");
        }
    }
}
