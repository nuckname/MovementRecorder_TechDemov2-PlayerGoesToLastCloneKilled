using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int damage;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform GhostClone;
    

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hit the ghost
        if (collision.gameObject.CompareTag("GhostToKill"))
        {
            GhostClone = collision.gameObject.GetComponent<Transform>();
            
            Player.position = GhostClone.position;
            //Rotation isnt working
            Player.rotation = GhostClone.rotation;
            
            // Disable ghost's replay and deactivate the ghost object
            GameObject clone = collision.gameObject;
            clone.GetComponent<GhostReplayData>().isReplay = false; 
            clone.SetActive(false);
        }
    }
} 
