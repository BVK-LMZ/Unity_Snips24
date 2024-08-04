using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Adjustable rotation speed for the camera
    public float _cameraRotationSpeed = 100.0f;

    void Update()
    {
        // Check for left arrow key press
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate the camera to the left (negative y-axis)
            transform.Rotate(Vector3.up, -_cameraRotationSpeed * Time.deltaTime);
        }
        // Check for right arrow key press
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate the camera to the right (positive y-axis)
            transform.Rotate(Vector3.up, _cameraRotationSpeed * Time.deltaTime);
        }
    }
}
