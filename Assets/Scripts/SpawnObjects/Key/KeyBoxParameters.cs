using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct STRUCTKeyBoxParameters
{
    public int MinimumAmount;
    public int MaximumAmount;
    public int MinimumHeight;
    public int MaximumHeight;
}
public class KeyBoxParameters : MonoBehaviour
{
    public STRUCTKeyBoxParameters structKeyBoxParameters;
    
    void Start()
    {
        structKeyBoxParameters.MinimumAmount = 1;
        structKeyBoxParameters.MaximumAmount = 1;

        structKeyBoxParameters.MinimumHeight = 1;
        structKeyBoxParameters.MaximumHeight = 1;
    }
}
