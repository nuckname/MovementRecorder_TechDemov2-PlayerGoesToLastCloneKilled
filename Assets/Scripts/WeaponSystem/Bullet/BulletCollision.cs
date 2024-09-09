using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        //Tron Bullet Collision is in a different script. Not sure why it isnt working.
        if (collision.gameObject.CompareTag("GhostToKill"))
        {
            //Tp player now:
            
            
            GameObject clone = collision.gameObject;
            clone.GetComponent<GhostReplayData>().isReplay = false; 
            clone.SetActive(false);
            //Destroy(clone);
        }
        
        
        /*
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) {
            damageable.TakeDamage(damage);
        }*/
        
    }
} 
