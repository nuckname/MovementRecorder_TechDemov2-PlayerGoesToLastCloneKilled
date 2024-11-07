using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public Transform firePoint;     // Point from where the bullet will be fired
    public float fireRate = 1f;     // Time between shots
    private float nextFireTime = 0f;
    public Transform player;        // Reference to the player

    void Update()
    {
        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet and set its direction
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
