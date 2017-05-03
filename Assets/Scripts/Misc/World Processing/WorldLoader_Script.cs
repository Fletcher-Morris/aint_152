using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldLoader_Script : MonoBehaviour {

    public World theWorld;

    public string nameOfWorldToLoad;

    public GameObject enemyShipPrefab;
    public GameObject asteroidPrefab;

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
        }

        if (level == 1)
        {
            Debug.Log(gameObject.name + ": this is the Game Scene.");
            theWorld = theWorld.LoadWorld(nameOfWorldToLoad);
            GenerateWorld();

            ActivateMission("Get To The Cockpit");
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
            GameObject thisAsteroid = GameObject.Instantiate(asteroidPrefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
        }

    }

    public void SaveTheWorld()
    {
		GameObject.Find ("Player Ship").GetComponent<ShipSetup_Script> ().SavePlayerShip ();

		theWorld.currentStarSystem.asteroids.Clear();
        foreach(GameObject _asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
			theWorld.currentStarSystem.asteroids.Add(new Asteroid(_asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
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

        string missionUiText = _mission.missionName + "\n\n" + _mission.missionDescription + "\n\n" + "Reward: $" + _mission.missionReward;

        GameObject.Find("Player UI Canvas").transform.GetChild(5).gameObject.SetActive(true);
        GameObject.Find("Player UI Canvas").transform.GetChild(5).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = missionUiText;
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
        FindMission(_name).completed = true;

        theWorld.completedMissions.Add(FindMission(_name));

        theWorld.activeMissions.Remove(FindMission(_name));

        theWorld.money += FindMission(_name).missionReward;
    }

    public void SkipTutorial()
    {
        CompleteMission("Get To The Cockpit");
        CompleteMission("Destroy Three Asteroids");
        CompleteMission("Destroy The Theif");
    }
}
