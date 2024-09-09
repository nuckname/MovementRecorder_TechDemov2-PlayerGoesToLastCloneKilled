using System.Collections.Generic;
using UnityEngine;

public class PlayingState : GameBaseState
{
    //currently playing. 
    //updates from player collision
    public static List<GhostData> AllGhostMovement;
    
    public override void EnterState(PlayStateManager gameState)
    {
        Debug.Log("Playing State");
        //is recording
        //playing clone records + spawning them in.
        //sets player isReplay to true. 
        //Does here Make sense?
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            isReplay(true);

        }

        //for bullet time value
        WorldTimer.BULLET_TIMER_LEFT_SPAWN_AREA = true;

        
        gameState.SpawnAllGhosts();

    }

    public override void UpdateState(PlayStateManager gameState)
    {
        //Call GhostRecorder Script
        //No pass in the data to GhostReplayData. 
    }

    public override void isReplay(bool isReplaying)
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GhostRecorder ghostRecorder = player.GetComponent<GhostRecorder>();
            if (ghostRecorder != null)
            {
                ghostRecorder.isRecord = isReplaying;
                ghostRecorder.recordFrequency = 70;
                
                ghostRecorder.recordRotation = true;
                //So ghost play at the same time.
                ghostRecorder.timeValue = 0;
            }
        }
    }
}
