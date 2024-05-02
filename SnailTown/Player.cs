using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Animator _animator;

    [Header("Horizontal Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _inputSmoothing = 0.1f;
    private float _smoothedHorizontalInput;

    [Header("Vertical Movement")]
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _maxJumps = 2;
    
    private int _jumpsRemaining;
    private bool _bIsAlive = true;
    private bool _bisAttacking = false;

    [Header("Various Tags")]
    [SerializeField] private string _groundTag = "Ground";
    [SerializeField] private string _enemyTag = "Enemy";

    [Header("Attack Settings")]
    [SerializeField] public  Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _attackLayers;
    

    private void Update()
    {
        UpdateHorizontalMovement();
        JumpSystem();
        GroundCheck();
        HandleAnimation();
        HandleAttack();
 
    }

  

    private void HandleAnimation()
    {
        if (_animator != null)
        {
            bool isWalking = Mathf.Abs(_smoothedHorizontalInput) > 0.1f;
            _animator.SetBool("bIsWalking", isWalking);

            if (_bIsAlive == true)
            {
                _animator.SetBool("bIsAlive", true);
            }
            else
            {
                _animator.SetBool("bIsAlive", false);
                HandleDeath();
            }
        }
    }

    private void HandleDeath()
    {
        //disable self box collider
        _collider2D.enabled = false;
    }

    private void UpdateHorizontalMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _smoothedHorizontalInput = Mathf.Lerp(_smoothedHorizontalInput, horizontalInput, Time.deltaTime / _inputSmoothing);
        _rb2d.velocity = new Vector2(_smoothedHorizontalInput * _speed, _rb2d.velocity.y);

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
        if (Input.GetKeyDown(KeyCode.LeftAlt) && _jumpsRemaining > 0)
        {
            _animator.SetBool("bIsJumping", true);
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
                _animator.SetBool("bIsJumping", false);
                isGrounded = true;
                break;
            }
        }

        _jumpsRemaining = (int)(isGrounded ? _maxJumps : _jumpsRemaining);
    }

    private void HandleAttack()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _bisAttacking = true;
            _animator.SetTrigger("bIsAttacking");
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("bIsAttacking");


        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, LayerMask.GetMask("Tango"));

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }

        StartCoroutine(ResetAttack());
    }

    // Coroutine to reset attack animation
    private IEnumerator ResetAttack()
    {
        _bisAttacking = false;
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        _animator.ResetTrigger("bIsAttacking"); // Reset the trigger
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
        {
            // Draw a wire sphere representing the attack range
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }

  
}
