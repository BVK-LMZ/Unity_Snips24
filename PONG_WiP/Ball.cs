using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyInitialBurst();
    }

    private void ApplyInitialBurst()
    {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 initialDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        float initialSpeed = 8f; // Adjust initial speed as needed
        rb.velocity = initialDirection * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D ball = rb;
            Collider2D paddle = collision.collider;

            // Gather information about the collision
            Vector2 ballDirection = ball.velocity.normalized;
            Vector2 contactDistance = ball.position - (Vector2)paddle.bounds.center;
            Vector2 surfaceNormal = collision.GetContact(0).normal;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, new Vector3(surfaceNormal.x, surfaceNormal.y, 0)); // Corrected for 2D

            // Rotate the direction of the ball based on the contact distance
            // to make the gameplay more dynamic and interesting
            float maxBounceAngle = 75f;
            float bounceAngle = (contactDistance.y / paddle.bounds.size.y) * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;

            // Re-apply the new direction to the ball
            ball.velocity = ballDirection * ball.velocity.magnitude;
        }
        else if (collision.gameObject.CompareTag("ScoreZone_A"))
        {
            Debug.Log("Score for Player A!");
            // Implement score logic for Player A
        }
        else if (collision.gameObject.CompareTag("ScoreZone_B"))
        {
            Debug.Log("Score for Player B!");
            // Implement score logic for Player B
        }
    }
}

