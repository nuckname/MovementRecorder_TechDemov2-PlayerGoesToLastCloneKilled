using System;
using System.Collections;
using System.Collections.Generic;
//Temp
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayStateManager playStateManager;
    private UiManager uiManager;
    private GameObject gameManager;

    [SerializeField] private GameObject player;

    private TeleportPlayer teleportPlayer;

    //For the key On FinshBox Object
    [SerializeField] private ChangeBoxColour changeBoxColour;
    public static bool playerHasKey = false; 
    
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if (gameManager != null)
        {
            playStateManager = gameManager.GetComponent<PlayStateManager>();
            uiManager = gameManager.GetComponent<UiManager>();
        }
        else 
        {
            Debug.LogError("GameManager object not found.");
        }

        teleportPlayer = GetComponent<TeleportPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            if (PlayerCollision.playerHasKey || GameManager.DebugTurnOffKey == true)
            {
                playStateManager.SwitchState(playStateManager.FinishState);

                //Bullet timer value. So bullets dont have a timeValue of 0.
                WorldTimer.BULLET_TIMER_LEFT_SPAWN_AREA = false;

                teleportPlayer.TeleportPlayerWhenCompletedLevel();
            }
        }
        
        if (other.gameObject.CompareTag("SpawnArea"))
        {
            playStateManager.SwitchState(playStateManager.StartState);
        }
        
        if (other.CompareTag("GhostBullet"))
        {
            print("You got hit, Reset Scene");
            //Variable in game manager, static
            //Oter in GhostCollision
            if (GameManager.DebugRestartScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            
            if (uiManager != null)
            {
                uiManager.GameOver();
            }
            else
            {
                print("Error, couldnt find UiManager");
            }
        }
        
        if (other.CompareTag("BoxRecorder"))
        {
            //doesnt matter
            print("You got hit by box1");
        }
        
        if (other.CompareTag("BoxReplayGhostData"))
        {
            print("You got hit by box2");
        }
        
        if (other.CompareTag("FloorTP"))
        {
            teleportPlayer.TeleportPlayerWhenFallIntoRedZone();
        }
        
        if (other.CompareTag("Key"))
        {
            //at finsih state player resets key. No logical flow. 
            Debug.Log("Player Has Key");
            playerHasKey = true;  
            changeBoxColour.UpdateBoxColour();
            
            //doesnt destory key
            //Destroy(other);

            GameObject key = GameObject.FindGameObjectWithTag("Key");
            Destroy(key);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnArea"))
        {
            playStateManager.SwitchState(playStateManager.PlayState);
        }
    }
}
