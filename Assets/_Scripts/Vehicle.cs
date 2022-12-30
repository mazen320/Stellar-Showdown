using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    // A reference to the rigidbody component on the vehicle
    public Rigidbody rigidbody;

    // The movement component for the vehicle
    public VehicleMovement movement;

    // Update is called once per frame
    void Update()
    {
        // Update the movement based on player input
        movement.UpdateMovement();
    }
}
