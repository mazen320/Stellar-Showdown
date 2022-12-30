using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform targetTransform;
    public float distance = 10f;
    public float height = 5f;
    public float smoothness = 0.5f;
    public float movementSpeed = 10f;
    public float minRotationX = -30f;
    public float maxRotationX = 30f;

    private Vector3 offset;
    private Vector3 currentPosition;
    private Quaternion currentRotation;
    private bool rightMouseButtonDown;

    void Start()
    {
        offset = cameraTransform.position - targetTransform.position;
        currentPosition = cameraTransform.position;
        currentRotation = cameraTransform.rotation;
    }

    void Update()
    {
        // If the right mouse button is held down, rotate the camera around the player based on the mouse movement
        if (Input.GetMouseButton(1))
        {
            // Set the right mouse button flag to true
            rightMouseButtonDown = true;

            // Calculate the difference in mouse position between the current and previous frames
            float mouseX = Input.GetAxis("Mouse X") * movementSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * movementSpeed * Time.deltaTime;

            // Clamp the rotation values to the desired range
            mouseY = Mathf.Clamp(mouseY, minRotationX, maxRotationX);

            // Rotate the camera around the player based on the mouse movement
            cameraTransform.RotateAround(targetTransform.position, targetTransform.up, mouseX);
            cameraTransform.RotateAround(targetTransform.position, cameraTransform.right, -mouseY);
        }
        else if (rightMouseButtonDown)
        {
            // The right mouse button was released, so set the flag to false
            rightMouseButtonDown = false;

            // Calculate the new offset based on the current camera position and rotation
            offset = cameraTransform.position - targetTransform.position;
        }

        // Update the position and rotation of the camera based on the current values
        currentPosition = targetTransform.position + offset;
        currentRotation = Quaternion.LookRotation(targetTransform.position - cameraTransform.position, targetTransform.up);

        // Smoothly interpolate between the current and target positions and rotations
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, currentPosition, smoothness);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, currentRotation, smoothness);
    }
}
