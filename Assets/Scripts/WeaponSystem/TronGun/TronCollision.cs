using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronCollision : MonoBehaviour
{
//Not sure why its not working for Bullet Collision with tag. Temp Fix.
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
