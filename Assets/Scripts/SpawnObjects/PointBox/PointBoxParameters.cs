using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct STRUCTPointBoxParameters
{
    public int MinimumAmount;
    public int MaximumAmount;
    public int MinimumHeight;
    public int MaximumHeight;
}
/* I should refactor this so that it is a blueprint. SpawnParamaters.
 * And create a new object for point box and weapon box
 * But too hard :(
 * This will fix SpawnGameObjectOnNavMesh.cs using two functions to spawn the same box.
 */
public class PointBoxParameters : MonoBehaviour
{ 
    public STRUCTPointBoxParameters structPointBoxParameters;
    
    void Start()
    {
        structPointBoxParameters.MinimumAmount = 1;
        structPointBoxParameters.MaximumAmount = 2;
        
        structPointBoxParameters.MinimumHeight = 2;
        structPointBoxParameters.MaximumHeight = 6;
    }
}
