using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f;
    [SerializeField] private float resetPosition = 10.0f; // Distance at which to reset the background
    [SerializeField] private float resetTime = 8.0f; // Time interval after which to reset the background

    private Vector3 _startPosition;
    public bool _scrollLeft;

    private void Start()
    {
        _startPosition = transform.position;
     
    }

    private void Update()
    {
        if (_scrollLeft)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if background has scrolled past reset position
        if (transform.position.x <= -resetPosition)
        {
            StartCoroutine(ResetCoroutine());
        }
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);

        // Check if background has scrolled past reset position
        if (transform.position.x >= resetPosition)
        {
            StartCoroutine(ResetCoroutine());
        }
    }

    private IEnumerator ResetCoroutine()
    {
        // Wait for resetTime before resetting
        yield return new WaitForSeconds(resetTime);

        // Reset to start position
        transform.position = _startPosition;

        // Randomly choose new direction
        _scrollLeft = Random.value < 0.5f;
    }
}
