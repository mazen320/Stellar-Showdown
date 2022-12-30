using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float accelerationForce;

    public float brakeForce;

    public float maxSpeed;

    public float rotationSpeed;

    public VehicleMovement(Rigidbody rb)
    {
        this.rb = rb;
    }

    public void UpdateMovement()
    {
        if (rb == null)
        {
            Debug.LogError("Error: No Rigidbody component found on object.");
            return;
        }

        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");
        Quaternion rotation = Quaternion.Euler(0f, rotateInput * rotationSpeed, 0f);
        rb.MoveRotation(rb.rotation * rotation);
        Vector3 facingDirection = transform.forward;
        Vector3 force = facingDirection * moveInput * accelerationForce;
        if (moveInput < 0)
        {
            force = -facingDirection * brakeForce;
        }
        rb.AddRelativeForce(force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

}
