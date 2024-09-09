using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    public float spiralSpeed = 5f;  // Speed of the spiral
    public float spiralRadius = 0.5f;  // Radius of the spiral
    public float forwardForce = 10f;  // Initial forward force

    private Rigidbody rb;
    private float time;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Apply the initial forward force
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime * spiralSpeed;

        // Calculate the spiral motion in local space
        float offsetX = Mathf.Sin(time) * spiralRadius;
        float offsetY = Mathf.Cos(time) * spiralRadius;

        // Create the spiral motion vector in the local space, with balanced movement
        Vector3 spiralMotion = (transform.right * offsetX + transform.up * offsetY);

        // Apply the spiral motion in world space
        rb.velocity = transform.forward * rb.velocity.magnitude + spiralMotion;
    }
}