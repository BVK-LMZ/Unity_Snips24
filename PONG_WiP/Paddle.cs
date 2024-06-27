using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 velocity = new Vector2(0, verticalInput * speed);
        _rb2d.velocity = velocity;
    }

}
