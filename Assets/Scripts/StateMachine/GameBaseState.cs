using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(PlayStateManager gameState);
    public abstract void UpdateState(PlayStateManager gameState);
    public abstract void isReplay(bool isReplaying);

}
