using UnityEngine;

public class ScreenWarp : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        // Find the Main Camera object named "MainCamera"
        mainCamera = GameObject.Find("MainCamera")?.GetComponent<Camera>();

        // Check if the camera was found
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    void Update()
    {
        WrapPosition();
    }

    void WrapPosition()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = WrapAxis(newPosition.x, Axis.X);
        newPosition.y = WrapAxis(newPosition.y, Axis.Y);

        transform.position = newPosition;
    }

    enum Axis { X, Y }

    float WrapAxis(float position, Axis axis)
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (axis == Axis.X)
        {
            if (screenPosition.x > 1)
            {
                position = mainCamera.ViewportToWorldPoint(new Vector3(0, screenPosition.y, screenPosition.z)).x;
            }
            else if (screenPosition.x < 0)
            {
                position = mainCamera.ViewportToWorldPoint(new Vector3(1, screenPosition.y, screenPosition.z)).x;
            }
        }
        else if (axis == Axis.Y)
        {
            if (screenPosition.y > 1)
            {
                position = mainCamera.ViewportToWorldPoint(new Vector3(screenPosition.x, 0, screenPosition.z)).y;
            }
            else if (screenPosition.y < 0)
            {
                position = mainCamera.ViewportToWorldPoint(new Vector3(screenPosition.x, 1, screenPosition.z)).y;
            }
        }

        return position;
    }
}
