using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Vector3 target;

    public void SetTarget(Vector3 targetPoint) {
        target = targetPoint;
    }

    private void Update() {
        // Move the bullet towards the target point
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Check if the bullet reached the target point
        if (Vector3.Distance(transform.position, target) < 0.1f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Handle collision with other objects
        // Destroy the bullet or do something else
        Destroy(gameObject);
    }
}