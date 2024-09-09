using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRecorder : MonoBehaviour
{
    private GhostRecorder ghostRecorder;
    public bool boxHasMoved; 
    private void Awake()
    {
        ghostRecorder = gameObject.GetComponent<GhostRecorder>();
    }

    public void StartBoxRecording()
    {
        
    }

    public void StopBoxRecording()
    {
        ghostRecorder.isRecord = false; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BoxHasMoved();
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            BoxHasMoved();
        }
   
    }

    private void BoxHasMoved()
    {
        print("col");
        ghostRecorder.isRecord = true;
        ghostRecorder.timeValue = WorldTimer.WorldTimeValue;
        ghostRecorder.recordFrequency = 50;
        //.isRotation = true
        boxHasMoved = true;
    }
}
