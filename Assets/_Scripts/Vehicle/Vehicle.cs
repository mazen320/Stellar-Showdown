using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Rigidbody rigidbody;

    public VehicleMovement movement;
    public Boost boost;
    public JumpsAndFlips jumpsAndFlips;

    void Update()
    {
        // Update the movement, boost, and jumps and flips based on player input
        movement.FixedUpdate();
        jumpsAndFlips.Update();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            boost.Activate();
        }
    }
}
