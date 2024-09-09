using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRecordingGhost : MonoBehaviour
{
    int ghostID = 0; 

     public void StopRecordingMultipleGhosts(PlayStateManager gameState, string tag, int recordFrequency)
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag(tag);

        if (ghosts != null)
        {
            foreach (GameObject ghost in ghosts)
            {
                GhostRecorder ghostRecorder = ghost.GetComponent<GhostRecorder>();
                if (ghostRecorder != null)
                {
                    StopGhostRecording(ghostRecorder, recordFrequency);

                    if (tag == "Bullet")
                    {
                        HandleBulletGhost(gameState, ghost, ghostRecorder);
                    }
                    else if (tag == "BoxRecorder")
                    {
                        HandleBoxRecorderGhost(gameState, ghost, ghostRecorder);
                    }
                }
            }
            //Adding this to I can remove bullets spawning for GhostToKill that have been hit. 
            //gameState.allGhostBulletData.Add(ghostRecorder.ghost);
        }
    }

    private void StopGhostRecording(GhostRecorder ghostRecorder, int recordFrequency)
    {
        ghostRecorder.isRecord = false;
        ghostRecorder.recordFrequency = recordFrequency;
    }

    private void HandleBulletGhost(PlayStateManager gameState, GameObject ghost, GhostRecorder ghostRecorder)
    {
        gameState.singleGhostBulletData.Add(ghostRecorder.ghost);

        
        gameState.DestoryGhostRecordingWhenCompleted(ghost);
        
    }

    private void HandleBoxRecorderGhost(PlayStateManager gameState, GameObject ghost, GhostRecorder ghostRecorder)
    {
        BoxRecorder boxRecorder = ghost.GetComponent<BoxRecorder>();

        if (boxRecorder != null)
        {
            gameState.allGhostBoxData.Add(ghostRecorder.ghost);

            if (boxRecorder.boxHasMoved)
            {
                gameState.DestoryGhostRecordingWhenCompleted(ghost);
            }
        }
    }

    //Player Ghost
    public void StopRecordingOneGhost(PlayStateManager gameState, string tag, int recordFrequency)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            GhostRecorder ghostRecorder = player.GetComponent<GhostRecorder>();

            if (ghostRecorder != null)
            {
                //stop ghost recording
                ghostRecorder.isRecord = false;
                ghostRecorder.recordFrequency = recordFrequency;
                
                //Transfer data from GhostRecorder to PlayStateManager
                GhostData newGhostData = new GhostData
                {
                    timeStamp = new List<float>(ghostRecorder.ghost.timeStamp),
                    position = new List<Vector3>(ghostRecorder.ghost.position),
                    rotation = new List<Quaternion>(ghostRecorder.ghost.rotation),
                    GostID = ghostID++
                };
                
                gameState.allGhostPlayerData.Add(newGhostData);
                
                ghostRecorder.ResetGhostData();
            }
            else
            {
                Debug.LogError("GhostRecorder component not found on the player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }
}
