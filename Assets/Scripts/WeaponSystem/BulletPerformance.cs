using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPerformance : MonoBehaviour
{
    private int currentCollisions = 0;
    [SerializeField] private int maxCollisions = 6;

    [SerializeField] private bool destoryAfterReachedMaxLifeTime = false;
    [SerializeField] private float currentLifeTime = 0;

    [SerializeField] private float maxLifeTime = 10.0f;

    [SerializeField] private GhostRecorder ghostRecorder;
    
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
        ghostRecorder.recordRotation = false; 
    }

    private void Update()
    {
        currentLifeTime += Time.unscaledDeltaTime;
        
        //Performance
        if (currentLifeTime >= maxLifeTime)
        {
            ghostRecorder.isRecord = false;
        }

        //stop recording if hasnt moved.
        if (ghostRecorder.isRecord && currentLifeTime >= 2)
        {
            Vector3 currentPosition = transform.position;
            if (currentPosition == lastPosition)
            {
                ghostRecorder.isRecord = false;

                if (destoryAfterReachedMaxLifeTime)
                {
                    Destroy(gameObject);
                }
               //print("same as last position: stopped recording");
            }
            lastPosition = currentPosition;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        currentCollisions++;

        //Performance
        if (currentCollisions >= maxCollisions)
        {
            ghostRecorder.isRecord = false;
           // print("Maximumn collisions reached: stopped recording");
            //Destroy(gameObject); 
        }
    }
}
