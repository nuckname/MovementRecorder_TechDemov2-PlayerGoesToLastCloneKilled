using System;
using System.Collections;
using System.Collections.Generic;
using Fragsurf.Movement;
using UnityEngine;
using UnityEngine.Serialization;

public class TeleportPlayer : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    //named after the clone (1)
    [FormerlySerializedAs("spawnPointOutSideOfSpawnAreaMiddle")] public Transform spawnPointOutSideOfSpawnArea1;
    [FormerlySerializedAs("spawnPointOutSideOfSpawnAreaLeft")] public Transform spawnPointOutSideOfSpawnArea2;
    [FormerlySerializedAs("spawnPointOutSideOfSpawnAreaRight")] public Transform spawnPointOutSideOfSpawnArea3;
    [FormerlySerializedAs("spawnPointOutSideOfSpawnAreaRight")] public Transform spawnPointOutSideOfSpawnArea4;
    
    static public int spawnPointIndex = 1;

    [SerializeField] private SurfCharacter surfCharacter;
    
    [SerializeField] private Transform spawnPointFarLeft;
    [SerializeField] private Transform spawnPointFarMiddle;
    [SerializeField] private Transform spawnPointFarRight;
    [SerializeField] private Transform spawnPointFarBack;

    public void TeleportPlayerWhenCompletedLevel()
    {
        //Not sure what this is. I think its being called twice. I use If player has key. but doesnt work.
        StartCoroutine(RespawnPlayerWithDelay(0.35f));
    }
    
    private IEnumerator EnableCharacterMovementWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        surfCharacter.EnableInput();
        //enable character movement
    }
    
    private IEnumerator RespawnPlayerWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //debugging mutiple gamemodes
        //Change in GameManager
        if (GameManager.Debug3SpawnPoints)
        {
            switch (spawnPointIndex)
            {
                case 1:
                    player.transform.position = spawnPointFarMiddle.position;
                    spawnPointIndex++;
                    break;  
                case 2:
                    player.transform.position = spawnPointFarRight.position;
                    spawnPointIndex++;
                    break;
                case 3:
                    player.transform.position = spawnPointFarLeft.position;
                    spawnPointIndex++;
                    break;
                case 4:
                    player.transform.position = spawnPointFarBack.position;
                    spawnPointIndex = 1;
                    break;
            }
        }
        else
        {
            player.transform.position = spawnPointFarLeft.position;
        }
        
        surfCharacter.StopMomentum();
        surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, 57, 0);

        print(surfCharacter._colliderObject.transform.rotation);
        Debug.Log(surfCharacter._colliderObject.transform.rotation.eulerAngles);

        StartCoroutine(EnableCharacterMovementWithDelay(0.35f));
    }
    
    public void TeleportPlayerWhenFallIntoRedZone()
    {
        switch (spawnPointIndex)
        {
            case 1:
                player.transform.position = spawnPointOutSideOfSpawnArea1.position; 

                //So player is facing Green thing
                //Hard coded
                surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, 57, 0);
                break;  
            case 2: 
                player.transform.position = spawnPointOutSideOfSpawnArea2.position; 
                //surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, 178, 0);
                surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, 4.95f, 0.00f);
                break;
            case 3:
                player.transform.position = spawnPointOutSideOfSpawnArea3.position; 
                surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, -58, 0);
                break;
            case 4:
                player.transform.position = spawnPointOutSideOfSpawnArea4.position; 
                surfCharacter._colliderObject.transform.rotation = Quaternion.Euler(0, -58, 0);
                break;
                
        }
    }
}
