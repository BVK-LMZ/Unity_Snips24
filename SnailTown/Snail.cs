using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Animator animator;
    private bool isAlive = true;
    private Rigidbody2D rb;
    private Vector3 direction;

    private void Start()
    {
        // Initialize direction towards point A
        rb = GetComponent<Rigidbody2D>();
        direction = (pointA.position - transform.position).normalized;
    }

    private void Update()
    {
        if (isAlive)
            MoveSnail();
    }

    public void Die()
    {
        // Update animation parameter to indicate death
        animator.SetBool("IsAlive", false);
        // Disable collider
        GetComponent<Collider2D>().enabled = false;
        // Destroy after 2 seconds
        Destroy(gameObject, 2.0f);
        // Call function on GameModeMain singleton
        GameModeMain.Instance.OnSnailDeath();
    }

    private void MoveSnail()
    {
        // Move the snail in the current direction
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        // Check if the snail has reached a patrol point
        if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
        {
            SwitchDirection(pointB.position - transform.position);
            FaceDirection(Vector3.right); // Face right
        }
        else if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            SwitchDirection(pointA.position - transform.position);
            FaceDirection(Vector3.left); // Face left
        }
    }

    private void SwitchDirection(Vector3 newDirection)
    {
        // Change direction towards the new point
        direction = newDirection.normalized;
    }

    private void FaceDirection(Vector3 targetDirection)
    {
        // Flip the sprite scale to face the target direction
        float xScale = Mathf.Sign(targetDirection.x) * Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    // 2D trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Move back slightly when colliding with the player
            Vector3 backwardDirection = -(collision.transform.position - transform.position).normalized * 0.5f;
            transform.Translate(backwardDirection);
        }
    }
}
