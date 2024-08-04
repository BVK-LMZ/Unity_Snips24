using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraHolder; // Reference to the camera holder for both vertical and horizontal rotations
    public float lookSpeed = 2f; // Speed of the look rotation, determines how fast the camera responds to input

    // Clamping variables for rotation limits
    public float verticalLookRotation = 0f; // Current vertical rotation angle of the camera holder
    public float horizontalLookRotation = 0f; // Current horizontal rotation angle of the camera holder
    public float verticalMinClamp = -90f; // Minimum value for vertical rotation to limit camera's upward looking angle
    public float verticalMaxClamp = 90f; // Maximum value for vertical rotation to limit camera's downward looking angle
    public float horizontalMinClamp = -360f; // Minimum value for horizontal rotation, allows full 360 degrees rotation
    public float horizontalMaxClamp = 360f; // Maximum value for horizontal rotation, allows full 360 degrees rotation



    private void Start()
    {
        // Hide the cursor and lock it to the center of the screen
        //Cursor.visible = false; // Hide the cursor
       Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        // Get mouse input for horizontal (X) and vertical (Y) movement
        float horizontalInput = Input.GetAxisRaw("Mouse X"); // Input from the mouse for horizontal movement (left/right)
        float verticalInput = Input.GetAxisRaw("Mouse Y"); // Input from the mouse for vertical movement (up/down)

        // Update horizontal look rotation based on mouse input
        horizontalLookRotation += horizontalInput * lookSpeed; // Increase the horizontal rotation by mouse input, scaled by lookSpeed
        horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, horizontalMinClamp, horizontalMaxClamp); // Clamp the rotation within defined limits

        // Update vertical look rotation based on mouse input
        verticalLookRotation -= verticalInput * lookSpeed; // Decrease the vertical rotation by mouse input, scaled by lookSpeed
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, verticalMinClamp, verticalMaxClamp); // Clamp the rotation within defined limits

        // Apply both horizontal and vertical rotations to the camera holder
        cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, horizontalLookRotation, 0f); // Set the local rotation of the camera holder with vertical and horizontal angles
    }

}
