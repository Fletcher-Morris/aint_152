﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This Script Handles Loading  and Saving World Data.
// It also takes care of Mission Handeling.

public class WorldLoader_Script : MonoBehaviour {

    public World theWorld;

    public string nameOfWorldToLoad;

    public GameObject enemyShipPrefab;
    public GameObject asteroidPrefab;
    public GameObject mediumAsteroidPrefab;

    public void LoadSelectedWorld()
    {
        theWorld = theWorld.LoadWorld(nameOfWorldToLoad);
        SceneManager.LoadScene("Game_Scene");
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            theWorld.worldName = null;
            theWorld.currentWave = 0;
			theWorld.score = 0;
            theWorld.money = 0;
			theWorld.gold = 0;
            theWorld.playerShip = null;
			theWorld.currentStarSystem.enemyShips = null;
			theWorld.currentStarSystem.asteroids = null;
            nameOfWorldToLoad = null;
            ClearMissions();
        }

        if (level == 1)
        {
            Debug.Log(gameObject.name + ": this is the Game Scene.");
            theWorld = theWorld.LoadWorld(nameOfWorldToLoad);
            GenerateWorld();

            if (MissionExists("Get To The Cockpit") == true)
            {
                if (FindMission("Get To The Cockpit").completed == false)
                {
                    ActivateMission("Get To The Cockpit"); 
                }
                else
                {
                    DisplayMission(theWorld.activeMissions[0]);
                }
            }
            else
            {
                ActivateMission("Get To The Cockpit");
            }
        }
    }

    public void GenerateWorld()
    {
        GenerateEnemyShips();
        GenerateAsteroids();
    }

    public void GenerateEnemyShips()
    {
		foreach (Ship _enemyShip in theWorld.currentStarSystem.enemyShips)
        {
            GameObject thisShip = GameObject.Instantiate(enemyShipPrefab, _enemyShip.shipPos, Quaternion.Euler(_enemyShip.shipRot));
        }
    }

    public void GenerateAsteroids()
    {
		foreach (Asteroid _asteroid in theWorld.currentStarSystem.asteroids)
        {
            if (_asteroid.asteroidSize == "Medium")
            {
                GameObject thisAsteroid = GameObject.Instantiate(mediumAsteroidPrefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
            }
            else
            {
                GameObject thisAsteroid = GameObject.Instantiate(asteroidPrefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
            }
        }

    }

    public void SaveTheWorld()
    {
		GameObject.Find ("Player Ship").GetComponent<ShipSetup_Script> ().SavePlayerShip ();

		theWorld.currentStarSystem.asteroids.Clear();
        foreach(GameObject _asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if (_asteroidObject.name == "Asteroid Medium(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Medium", _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Large", _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
        }

        theWorld.currentStarSystem.enemyShips.Clear();
        foreach (GameObject _enemyShipObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
			theWorld.currentStarSystem.enemyShips.Add(new Ship(_enemyShipObject.GetComponent<ShipSetup_Script>().shipDetails.shipName, _enemyShipObject.GetComponent<ShipSetup_Script>().shipDetails.shipHealth, _enemyShipObject.transform.position, _enemyShipObject.transform.rotation.eulerAngles));
        }

        theWorld.SaveWorld(theWorld);
    }

    public void ActivateMission(Mission _mission)
    {
        theWorld.activeMissions.Add(_mission);

        DisplayMission(_mission);

        if (theWorld.autoSave)
        {
            SaveTheWorld();
        }
    }

    public void ActivateMission(string _missionName)
    {
        ActivateMission(GameObject.Find("GM").GetComponent<DefaultMissions_Script>().Find(_missionName));
    }

    public Mission FindMission(string _name)
    {
        foreach (Mission _mission in theWorld.activeMissions)
        {
            if (_mission.missionName == _name)
            {
                return _mission;
            }
        }

        foreach (Mission _mission in theWorld.completedMissions)
        {
            if (_mission.missionName == _name)
            {
                return _mission;
            }
        }

        return null;
    }

    public bool MissionExists(string _name)
    {
        foreach (Mission _mission in theWorld.activeMissions)
        {
            if (_mission.missionName == _name)
            {
                return true;
            }
        }

        foreach (Mission _mission in theWorld.completedMissions)
        {
            if (_mission.missionName == _name)
            {
                return true;
            }
        }

        return false;
    }

    public void CompleteMission(string _name)
    {
        theWorld.completedMissions.Add(FindMission(_name));

        theWorld.activeMissions.Remove(FindMission(_name));

        FindMission(_name).completed = true;

        theWorld.money += FindMission(_name).missionReward;

        if (theWorld.autoSave)
        {
            SaveTheWorld();
        }
    }

    public void ClearMissions()
    {
        theWorld.activeMissions.Clear();
        theWorld.completedMissions.Clear();
    }

    public void DisplayMission(Mission _mission)
    {
        string missionUiText = "";

        if (_mission.missionReward >= 1)
        {
            missionUiText = _mission.missionName + "\n\n" + GameObject.Find("GM").GetComponent<WordReplacer_Script>().ReplaceWords(_mission.missionDescription) + "\n\n" + "Reward: $" + _mission.missionReward;
        }
        else
        {
            missionUiText = _mission.missionName + "\n\n" + GameObject.Find("GM").GetComponent<WordReplacer_Script>().ReplaceWords(_mission.missionDescription);
        }

        GameObject.Find("Player UI Canvas").transform.GetChild(5).gameObject.SetActive(true);
        GameObject.Find("Player UI Canvas").transform.GetChild(5).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = missionUiText;
    }

    public void SkipTutorial()
    {
        CompleteMission("Get To The Cockpit");
        CompleteMission("Destroy Three Asteroids");
        CompleteMission("Destroy The Theif");
    }
}
