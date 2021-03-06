﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Script : MonoBehaviour {

    public string gameState = "Normal";
	public string previousGameState;

	public string playerState = "Normal";
	public string previousPlayerState;

	public bool isUsingWeaponWheel = false;

    public string GetState()
    {
        return gameState;
    }

	public string GetPreviousState()
	{
		return previousGameState;
	}

	public string GetPlayerState()
	{
		return playerState;
	}

	public string GetPreviousPlayerState()
	{
		return previousPlayerState;
	}

	void Start()
	{
		previousGameState = gameState;
	}

	public void SetState(string newState)
	{
		previousGameState = gameState;
		gameState = newState;
	}

	public void SetPlayerState(string newState)
	{
		previousPlayerState = playerState;
		playerState = newState;
	}

    public void UnPause()
    {
        if(GetState() == "Paused")
        {
            SetState("Normal");
        }
    }

    void Update()
    {
		if (Input.GetButtonDown("Cancel"))
        {
			if (GetState () == "Paused") {
				SetState ("Normal");
				Time.timeScale = 1f;
			} else if (GetState () == "Normal") {
				SetState ("Paused");
				Time.timeScale = 0f;
			}
		}

		if (GetState () == "Normal" && isUsingWeaponWheel) {
			Time.timeScale = 0.25f;
		} else if (GetState () == "Normal" && !isUsingWeaponWheel) {
			Time.timeScale = 1;
		}
    }
}