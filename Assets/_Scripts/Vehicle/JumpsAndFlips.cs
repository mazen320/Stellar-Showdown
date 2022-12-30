using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpsAndFlips : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce;
    public float flipForce;
    public float jumpCooldown;
    public float flipCooldown;

    public float currentJumpCooldown;
    public float currentFlipCooldown;

    public Transform targetObject;

    public void Update()
    {
        // Check if the jump or flip keys are pressed and the vehicle is grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && currentJumpCooldown == 0)
        {
            currentJumpCooldown = jumpCooldown;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded() && currentFlipCooldown == 0)
        {
            currentFlipCooldown = flipCooldown;
            rb.AddForce(Vector3.left * flipForce + Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && IsGrounded() && currentFlipCooldown == 0)
        {
            currentFlipCooldown = flipCooldown;
            rb.AddForce(Vector3.right * flipForce + Vector3.up * jumpForce, ForceMode.Impulse);
        }

        currentJumpCooldown = Mathf.Max(currentJumpCooldown - Time.deltaTime, 0f);
        currentFlipCooldown = Mathf.Max(currentFlipCooldown - Time.deltaTime, 0f);
    }


    private bool IsGrounded()
    {
        Ray ray = new Ray(targetObject.position, Vector3.down);

        return Physics.Raycast(ray, 0.5f);
    }

}