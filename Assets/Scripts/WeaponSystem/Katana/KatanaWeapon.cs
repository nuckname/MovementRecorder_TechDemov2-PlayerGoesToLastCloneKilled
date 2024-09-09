using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaWeapon : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashTime;
    private float dashCooldownTimer;

    private Rigidbody playerRB;

    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        if (playerRB == null)
        {
            Debug.LogError("Player Rigidbody not found!");
        }
    }

    void Update()
    {
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
                Debug.Log("Dash is ready again!");
            }
        }

        // Handle the dash input
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("Starting Dash!");

        isDashing = true;
        canDash = false;
        dashTime = Time.time + dashDuration;
        dashCooldownTimer = dashCooldown;

        Vector3 dashDirection = transform.forward * dashSpeed;
        playerRB.AddForce(dashDirection, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        // Stop the dash by setting the velocity to zero
        playerRB.velocity = Vector3.zero;

        isDashing = false;
        Debug.Log("Dash ended.");
    }
}