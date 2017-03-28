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

    public void SetStateFlyingShip()
    {
        gameState = "Flying Ship";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetState() == "Normal" || GetState() == "Flying Ship")
            {
                SetStatePaused();
                Time.timeScale = 0;
            }
            else
            {
                SetStateNormal();
                Time.timeScale = 1;
            }
        }
    }
}
