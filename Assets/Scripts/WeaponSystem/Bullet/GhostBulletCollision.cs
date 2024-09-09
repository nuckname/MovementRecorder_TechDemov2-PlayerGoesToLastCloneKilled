using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("You got hit L");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("You got hit L");
        }
    }
}
