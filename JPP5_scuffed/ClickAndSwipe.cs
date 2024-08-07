using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    public GameModeMain gameManager;
    public Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider boxCollider;
    private bool swiping = false;

    void Awake()
    {
        // Set up the main camera, trail renderer, and box collider
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();

        // Disable trail renderer and collider initially
        trail.enabled = false;
        boxCollider.enabled = false;
    }

    void Update()
    {
        // Only allow swiping if objects are supposed to spawn
        if (gameManager._bSpawnObj)
        {
            // Start swiping on mouse button down
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            // Stop swiping on mouse button up
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            // Update mouse position while swiping
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void UpdateMousePosition()
    {
        // Convert mouse position to world position with a specific Z-depth
        float zDepth = 10.0f; // Adjust this value to match your game setup
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        // Enable or disable the trail renderer and collider based on swiping state
        trail.enabled = swiping;
        boxCollider.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the 'Target' component
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.DestroyTarget(); // Use the public method to destroy the target
        }
    }
}
