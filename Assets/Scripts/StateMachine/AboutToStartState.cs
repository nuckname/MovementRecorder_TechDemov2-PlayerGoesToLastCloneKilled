using Unity.VisualScripting;
using UnityEngine;

public class AboutToStartState : GameBaseState
{
    //In The Spawn Area
    //updates from player collision
    private SpawnGameObjectOnNavMesh navMesh;
    //[SerializeField] private PointBoxParameters pointBoxSpawner;

    
    public override void EnterState(PlayStateManager gameState)
    {
        //navMesh = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<SpawnGameObjectOnNavMesh>();
        //I feel like the code for spawning boxes should go here???
        gameState.ClearAllGameObjectBoxes();
        gameState.PointBoxSpawn();
        gameState.WeaponBoxSpawn();
        gameState.KeyBoxSpawn();
        //navMesh.SpawnBoxes(pointBoxSpawner.pointBoxParameters, pointBoxSpawner.gameObject);
    }

    public override void UpdateState(PlayStateManager gameState)
    {
        
    }
    
    public override void isReplay(bool isReplaying)
    {
        
    }

}
