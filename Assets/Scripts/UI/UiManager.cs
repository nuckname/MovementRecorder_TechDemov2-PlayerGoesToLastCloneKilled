using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    
    public void GameOver()
    {
        gameOverScreen.SetUp(AtFinishState.scoreCounter);    
    }
}
