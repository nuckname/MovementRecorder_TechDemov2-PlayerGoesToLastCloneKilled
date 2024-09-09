using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //So i can see in inspector
public class GhostData
{
    public List<float> timeStamp = new List<float>();
    public List<Vector3> position = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();
    public int GostID = new int();
    /*
    public void ResetData()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }
    */
}
