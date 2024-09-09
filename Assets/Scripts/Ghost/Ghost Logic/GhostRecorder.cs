using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public GhostData ghost = new GhostData();
    private float timer;

    public bool isRecord;
    public float recordFrequency;
    public float timeValue;

    public bool recordRotation;

    public void ResetGhostData()
    {
        ghost.timeStamp.Clear();
        ghost.position.Clear();
        ghost.rotation.Clear();
    }

    private void Start()
    {
        ghost.timeStamp.Add(timeValue);
        ghost.position.Add(this.transform.position);
        ghost.rotation.Add(this.transform.rotation);
    }

    void FixedUpdate()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;
        
        if (isRecord)
        {
            if (timer >= 1 / recordFrequency)
            {
                ghost.timeStamp.Add(timeValue);
                ghost.position.Add(this.transform.position);
                ghost.rotation.Add(this.transform.rotation);

                /*
                if (recordRotation)
                {
                    ghost.rotation.Add(this.transform.rotation);
                }
                */
                
                timer = 0;
            }
        }
    }
                
}