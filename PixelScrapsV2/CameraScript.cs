using UnityEngine; // Import the UnityEngine namespace, which includes essential classes for Unity development.

public class CameraScript : MonoBehaviour
{
    // Public variable to store a reference to the GameObject that the camera will follow.
    // The underscore is a naming convention indicating this is a private field exposed in the Unity Inspector.
    public GameObject _TargetToFollow;

    // Private variable to store the target location where the camera should move to.
    // This variable is of type Vector3, which represents a point in 3D space.
    private Vector3 _targetLocation;

    // Method responsible for updating the camera's position to follow the target.
    // This method will be called every frame inside the Update method.
    private void FollowTarget()
    {
        // Set the x component of the target location to the x position of the target's transform.
        // This ensures the camera will align horizontally with the target's position.
        _targetLocation.x = _TargetToFollow.transform.position.x;

        // Set the y component of the target location to the y position of the target's transform.
        // This ensures the camera will align vertically with the target's position.
        _targetLocation.y = _TargetToFollow.transform.position.y;

        // Set the z component of the target location to the camera's current z position.
        // This keeps the camera at the same depth, preventing it from moving closer or farther from the scene.
        _targetLocation.z = transform.position.z;

        // Update the camera's position by setting it to the calculated target location.
        // The camera will move to this new position, effectively following the target object.
        transform.position = _targetLocation;
    }

    // The Update method is called once per frame.
    // It is used here to continuously adjust the camera's position so it follows the target object.
    private void Update()
    {
        // Call the FollowTarget method to update the camera's position every frame.
        // This ensures the camera follows the target object smoothly and in real-time.
        FollowTarget();
    }
}
