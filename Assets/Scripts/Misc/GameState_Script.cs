using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Script : MonoBehaviour {

    public string gameState = "Normal";

    public string GetState()
    {
        return gameState;
    }

    void Update()
    {
		if (Input.GetButtonDown("Cancel"))
        {
            if(GetState() == "Normal")
            {
				gameState = "Paused";
                Time.timeScale = 0;
            }
			else if(gameState == "Paused")
            {
				gameState = "Normal";
                Time.timeScale = 1;
            }
        }
    }
}
