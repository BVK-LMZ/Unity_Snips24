using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rb2d;


    [Header("Horizontal Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _inputSmoothing = 0.1f;
    private float _smoothedHorizontalInput;

    private void Update()
    {
        UpdateHorizontalMovement();
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

        bool isWalking = Mathf.Abs(_smoothedHorizontalInput) > 0.1f;
        
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

}
