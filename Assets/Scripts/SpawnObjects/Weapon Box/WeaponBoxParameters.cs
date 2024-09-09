using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public struct STRUCTWeaponBoxParameters
{
    public int MinimumAmount;
    public int MaximumAmount;
    public int MinimumHeight;
    public int MaximumHeight;
}
public class WeaponBoxParameters : MonoBehaviour
{
    public STRUCTWeaponBoxParameters structWeaponBoxParameters;
    
    void Start()
    {
        structWeaponBoxParameters.MinimumAmount = 1;
        structWeaponBoxParameters.MaximumAmount = 1;

        structWeaponBoxParameters.MinimumHeight = 2;
        structWeaponBoxParameters.MaximumHeight = 6;
    }
}
