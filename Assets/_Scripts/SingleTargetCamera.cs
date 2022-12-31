using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public float height = 2.0f;
    public float offset = 0.0f;
    public float minPitch = -30.0f;
    public float maxPitch = 30.0f;
    public float minYaw = -45.0f;
    public float maxYaw = 45.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 5.0f;
    public float minZ = 0.0f;
    public float maxZ = 0.0f;
    public float movementSpeed = 3f;

    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        // Calculate the desired position and rotation for the camera
        Vector3 desiredPosition = target.position + (target.forward * minDistance) + (target.up * height);
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        // Clamp the distance, pitch, and roll values to the desired ranges
        float distance = Vector3.Distance(desiredPosition, target.position);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        desiredRotation = ClampRotationAroundXAxis(desiredRotation, minPitch, maxPitch);
        desiredRotation = ClampRotationAroundZAxis(desiredRotation, minZ, maxZ);

        // Calculate the current yaw angle of the camera
        float currentYaw = transform.eulerAngles.y;
        float desiredYaw = desiredRotation.eulerAngles.y;

        // Calculate the difference between the current and desired yaw angles
        float yawDifference = Mathf.DeltaAngle(currentYaw, desiredYaw);

        // If the difference is greater than 180 degrees, adjust the desired yaw angle to be the shortest path to the desired rotation
        if (Mathf.Abs(yawDifference) > 180.0f)
        {
            if (yawDifference > 0.0f)
            {
                desiredYaw -= 360.0f;
            }
            else
            {
                desiredYaw += 360.0f;
            }
        }

        // Update the yaw angle of the desired rotation
        desiredRotation.eulerAngles = new Vector3(desiredRotation.eulerAngles.x, desiredYaw, desiredRotation.eulerAngles.z);

        // Update the position and rotation of the camera based on the desired values
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothTime);
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    Quaternion ClampRotationAroundZAxis(Quaternion q, float minAngle, float maxAngle)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);

        angleZ = Mathf.Clamp(angleZ, minAngle, maxAngle);

        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q;
    }
}