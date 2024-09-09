using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReplayData : MonoBehaviour
{
    public GhostData ghostData;
    private float _timeValue;
    private int _index1;
    private int _index2;

    public bool isReplay;
    public bool isRecordRotation; 

    private void Awake()
    {
        _timeValue = 0;

        WorldTimer.WorldTimeValue = 0;
    }


    //void Update()
    void FixedUpdate()
    {
        if (isReplay)
        {
            //_timeValue += Time.unscaledDeltaTime;
            //performance??
            _timeValue = WorldTimer.WorldTimeValue;
            
            // Check if the replay has finished
            if (_timeValue > ghostData.timeStamp[ghostData.timeStamp.Count - 1])
            {
                Destroy(gameObject);
                return;
            }

            GetIndex();
            SetTransform();
        }
    }

    public void Initialize(GhostData ghostData)
    {
        this.ghostData = ghostData;
        _timeValue = 0;
        _index1 = 0;
        _index2 = 0;
    }
    
    private void GetIndex()
    {
        //this forloop loops 120 times before reaching first if statement. 
        for (int i = 0; i < ghostData.timeStamp.Count - 1; i++)
        {
            if (ghostData.timeStamp[i] == _timeValue)
            {
                _index1 = i;
                _index2 = i;
                return;
            }
            else if (ghostData.timeStamp[i] <= _timeValue && _timeValue < ghostData.timeStamp[i + 1])
            {
                _index1 = i;
                _index2 = i + 1;
                return;
            }
        }
        
        _index1 = ghostData.timeStamp.Count - 1;
        _index2 = ghostData.timeStamp.Count - 1;
    }

    private void SetTransform()
    {
        if (_index1 == _index2)
        {
            this.transform.position = ghostData.position[_index1];
            //How do i tell if performance is working?
            this.transform.rotation = ghostData.rotation[_index1];
            /*
            if (this.transform.position != null)
            {
                this.transform.rotation = ghostData.rotation[_index1];

            }*/
        }
        else
        {
            float interpolationFactor = (_timeValue - ghostData.timeStamp[_index1]) / (ghostData.timeStamp[_index2] - ghostData.timeStamp[_index1]);
            this.transform.position = Vector3.Lerp(ghostData.position[_index1], ghostData.position[_index2], interpolationFactor);
            this.transform.rotation = Quaternion.Slerp(ghostData.rotation[_index1], ghostData.rotation[_index2], interpolationFactor);

            //need to turn this off
            //Add record rotation to GhostData?
            /*
            if (this.transform.rotation != null)
            {
                print("DONT Replay Rotation");
                this.transform.rotation = Quaternion.Slerp(ghostData.rotation[_index1], ghostData.rotation[_index2], interpolationFactor);
            }*/
        }
    }
}
