using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayStateManager : MonoBehaviour
{
    private GameBaseState currentState;

    public GameObject ghostPrefab;
    public GameObject ghostBulletPrefab;
    public GameObject ghostBoxPrefab;
    
    public AboutToStartState StartState = new AboutToStartState();
    public PlayingState PlayState = new PlayingState();
    public AtFinishState FinishState = new AtFinishState();
    
    [SerializeField]
    public List<GhostData> allGhostPlayerData = new List<GhostData>();
    
    [SerializeField]
    public List<GhostData> singleGhostBulletData = new List<GhostData>();
    
    [SerializeField]
    public List<GhostData> allGhostBulletData = new List<GhostData>();
    
    [SerializeField]
    public List<GhostData> allGhostBoxData = new List<GhostData>();

    [SerializeField] public GhostManager ghostManager;

    public StopRecordingGhost stopRecordingGhost;

    [SerializeField] private PointBoxParameters pointBoxParameters;
    [SerializeField] private WeaponBoxParameters weaponBoxParameters;
    [SerializeField] private KeyBoxParameters keyBoxParameters;
    
    [SerializeField] private GameObject pointBox;
    [SerializeField] private GameObject weaponBox;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject ghostKey;

    [SerializeField] private ChangeBoxColour changeBoxColour;
    
    [SerializeField] private SpawnGameObjectOnNavMesh spawnGameObjectOnNavMesh;
    private void Awake()
    {
        //should definity fix this
        pointBoxParameters = FindObjectOfType<PointBoxParameters>();
        weaponBoxParameters = FindObjectOfType<WeaponBoxParameters>();
        keyBoxParameters = FindObjectOfType<KeyBoxParameters>();

        changeBoxColour = FindObjectOfType<ChangeBoxColour>(); 
    }

    void Start()
    {
        currentState = StartState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    //Called from AboutToStartState
    public void PointBoxSpawn()
    {
        //dont spawn point box on first round. 
        if (AtFinishState.scoreCounter != 0)
        {
            spawnGameObjectOnNavMesh.SpawnPointBox(pointBoxParameters.structPointBoxParameters, pointBox);
        }
    }
    
    public void WeaponBoxSpawn()
    {
        //dont spawn point box on first round. 
        if (AtFinishState.scoreCounter != 0)
        {
            spawnGameObjectOnNavMesh.SpawnWeaponBox(weaponBoxParameters.structWeaponBoxParameters, weaponBox);

        }
        
    }
    public void KeyBoxSpawn()
    {
        if (AtFinishState.scoreCounter != 0)
        {
            //Spawning Key and Ghost key in same location so I dont have to create a ghost clone etc.
            spawnGameObjectOnNavMesh.SpawnKeyBox(keyBoxParameters.structKeyBoxParameters, key, ghostKey);
        }
    }

    public void UpdateFinishBoxColour()
    {
        changeBoxColour.UpdateBoxColour();   
    }

    public void ClearAllGameObjectBoxes()
    {
        if (AtFinishState.scoreCounter != 0)
        {
            spawnGameObjectOnNavMesh.ClearAllPointBoxes();
        }
    }
    public void WeaponMiddleManSpawnObject()
    {
        Debug.Log("WeaponBox function called");
        //spawnGameObjectOnNavMesh.SpawnBoxes(pointBoxSpawner.pointBoxParameters, pointBox);
    }
    
    //Called from AtFinishState
    public void SpawnAllGhosts()
    {
        List<GameObject> ghostBoxClones = new List<GameObject>();

        ghostManager.InstantiateGhostGameObjects(ghostBoxClones, ghostBoxPrefab, allGhostBoxData, "GhostBox", false);
        ghostManager.InstantiateGhostData(ghostBoxClones, allGhostBoxData);
        
        List<GameObject> ghostPlayerClones = new List<GameObject>();
        //spawning this at the wrong time. 
        List<GameObject> ghostBulletClones = new List<GameObject>();
        
        ghostManager.InstantiateGhostGameObjects(ghostPlayerClones, ghostPrefab, allGhostPlayerData, "Ghost", true);
        ghostManager.InstantiateGhostGameObjects(ghostBulletClones, ghostBulletPrefab, singleGhostBulletData, "GhostBullet", false);
        
        ghostManager.InstantiateGhostData(ghostPlayerClones, allGhostPlayerData);
        ghostManager.InstantiateGhostData(ghostBulletClones, singleGhostBulletData);
    }

    public void DestoryGhostRecordingWhenCompleted(GameObject ghost)
    {
        Destroy(ghost);
    }
}
