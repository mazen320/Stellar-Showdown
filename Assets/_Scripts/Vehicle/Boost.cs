using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    public Rigidbody rb;

    public float boostAcceleration;
    public float boostDuration;
    public float boostCooldown;
    private float currentCooldown;

    public bool isBoostActive;

    // Update is called once per frame
    void Update()
    {
        currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0f);
    }

    public void Activate()
    {
        if(currentCooldown < 0f)
        {
            return;
        }

        rb.AddRelativeForce(Vector3.forward * boostAcceleration, ForceMode.Acceleration);

        currentCooldown = boostCooldown;

        isBoostActive = true;

        StartCoroutine(DisableBoost());
    }

    private IEnumerator DisableBoost()
    {
        //Wait for boost duration to finish
        yield return new WaitForSeconds(boostDuration);

        isBoostActive =false;
    }
}
