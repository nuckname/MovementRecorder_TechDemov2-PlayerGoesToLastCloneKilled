using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRecorder : MonoBehaviour
{
    //Cant be assigned in inspector for some reason. 
    [SerializeField] private GhostRecorder ghostRecorder;
    [SerializeField] private int bulletRecordFrequency = 15;
    public int BulletId;
    private void Awake()
    {
        ghostRecorder = gameObject.GetComponent<GhostRecorder>(); 
    }
    private void Start()
    {
        ghostRecorder.isRecord = true;
        ghostRecorder.recordFrequency = bulletRecordFrequency;
        
        //this is so the timevalue isnt zero which would mean that it would spawn when the player leaves the spawn area.
        ghostRecorder.timeValue = WorldTimer.WorldTimeValue;
    }


}
