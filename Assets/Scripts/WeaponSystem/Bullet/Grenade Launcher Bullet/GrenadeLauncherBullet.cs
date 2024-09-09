using System.Collections;
using System.Collections.Generic;
using Fragsurf.Movement;
using UnityEngine;

public class GrenadeLauncherBullet : MonoBehaviour
{
    public float speed = 10f;
    public float explosionRadius = 5f;
    public float explosionForce = 1000f;

    private Rigidbody rb;

    [SerializeField] private Collider[] colliders;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // Get the rigidbody component
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply explosion force
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // If the object is the player, apply additional force for rocket jumping
            SurfCharacter surfCharacter = nearbyObject.GetComponent<SurfCharacter>();
            if (surfCharacter != null)
            {
                Vector3 explosionDirection = (nearbyObject.transform.position - transform.position).normalized;
                Vector3 force = explosionDirection * explosionForce;
                surfCharacter.ApplyExternalForce(force);
            }
        }
    }
}