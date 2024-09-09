using System;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    [SerializeField] private BoxRecorder boxRecorder;

    private void Awake()
    {
        boxRecorder = gameObject.GetComponent<BoxRecorder>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //boxRecorder.StartBoxRecording();
        
        if (other.gameObject.CompareTag("Ghost"))
        {
            print("Ghost hit C");
        }

        if (other.gameObject.CompareTag("GhostToKill"))
        {
            print("Ghost hit C");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            print("Ghost hit T");
        }

        if (other.CompareTag("GhostToKill"))
        {
            print("Ghost hit T");
        }
        
        if (other.CompareTag("FloorTP"))
        {
            print("Stop Recording");
            //boxRecorder.StopBoxRecording();
        }
    }
    
    
    
    
    
    
}
