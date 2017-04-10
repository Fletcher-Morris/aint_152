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
			else if(GetState() == "Paused")
            {
				gameState = "Normal";
                Time.timeScale = 1;
            }
			else if(GetState() == "Weapon Wheel")
			{
				gameState = "Normal";
				Time.timeScale = 1;
			}
		}

		if (GetState () == "Weapon Wheel") {
			Time.timeScale = 0.5f;
		}
    }
}
