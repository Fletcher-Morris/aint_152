using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Script : MonoBehaviour {

    public string gameState = "Normal";

    public string GetState()
    {
        return gameState;
    }

    public void SetStatePaused()
    {
        gameState = "Paused";
    }

    public void SetStateNormal()
    {
        gameState = "Normal";
    }

    public void SetStateUsingTurret()
    {
        gameState = "UsingTurret";
    }

    public void SetStateFlyingShip()
    {
        gameState = "FlyingShip";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetState() == "Normal")
            {
                SetStatePaused();
            }
            else
            {
                SetStateNormal();
            }
        }
    }
}
