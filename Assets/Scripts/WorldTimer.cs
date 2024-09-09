using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public static float WorldTimeValue;
    public static bool BULLET_TIMER_LEFT_SPAWN_AREA; 

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        //if leftSpawnArea. 
        if (BULLET_TIMER_LEFT_SPAWN_AREA)
        {
            WorldTimeValue += Time.unscaledDeltaTime;
            //print(bulletWorldTimeValue);
        }
        //print(bulletWorldTimeValue);
    }
}
