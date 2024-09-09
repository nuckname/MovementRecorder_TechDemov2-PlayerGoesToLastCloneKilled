using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AtFinishState : GameBaseState 
{
    //In the Finish Areag
    //updates from player collision
    static public int scoreCounter = 0;
    public override void EnterState(PlayStateManager gameState)
    {
        Debug.Log("Finish State");

        scoreCounter++;
        Debug.Log("Score: " + scoreCounter);

        //reset key
        PlayerCollision.playerHasKey = false;

        gameState.UpdateFinishBoxColour();        
        
        //why is this here?
        var stopRecordingGhost = gameState.stopRecordingGhost;

        stopRecordingGhost.StopRecordingOneGhost(gameState, "Player", 70);
        stopRecordingGhost.StopRecordingMultipleGhosts(gameState, "Bullet", 15);
        stopRecordingGhost.StopRecordingMultipleGhosts(gameState, "BoxRecorder", 30);
    }

    public override void UpdateState(PlayStateManager gameState)
    {
        
    }
    
    public override void isReplay(bool isReplaying)
    {

    }
}
