using System.Collections;
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
    public GameObject asteroid1Fragment1;
    public GameObject asteroid1Fragment2;
    public GameObject asteroid1Fragment3;
    public GameObject asteroid2Prefab;
    public GameObject asteroid2Fragment1;
    public GameObject asteroid2Fragment2;
    public GameObject asteroid2Fragment3;

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
            if (_asteroid.asteroidSize == "Fragment")
            {
                if (_asteroid.asteroidVariation == 1)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid1Fragment1, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot)); 
                }
                else if (_asteroid.asteroidVariation == 2)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid1Fragment2, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
                else if (_asteroid.asteroidVariation == 3)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid1Fragment3, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
                else if (_asteroid.asteroidVariation == 4)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid2Fragment1, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
                else if (_asteroid.asteroidVariation == 5)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid2Fragment3, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
                else if (_asteroid.asteroidVariation == 6)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid2Fragment3, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
            }
            else
            {
                if (_asteroid.asteroidVariation == 1)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroidPrefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot)); 
                }
                else if(_asteroid.asteroidVariation == 2)
                {
                    GameObject thisAsteroid = GameObject.Instantiate(asteroid2Prefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
                }
            }
        }

    }

    public void SaveTheWorld()
    {
		GameObject.Find ("Player Ship").GetComponent<ShipSetup_Script> ().SavePlayerShip ();

		theWorld.currentStarSystem.asteroids.Clear();
        foreach(GameObject _asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if (_asteroidObject.name == "Asteroid" || _asteroidObject.name == "Asteroid(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Normal", 1, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else if (_asteroidObject.name == "Asteroid 2" || _asteroidObject.name == "Asteroid 2(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Normal", 2, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }

            if (_asteroidObject.name == "Asteroid 1 Fragment 1" || _asteroidObject.name == "Asteroid 1 Fragment 1(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 1, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else if (_asteroidObject.name == "Asteroid 1 Fragment 2" || _asteroidObject.name == "Asteroid 1 Fragment 2(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 2, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else if (_asteroidObject.name == "Asteroid 1 Fragment 3" || _asteroidObject.name == "Asteroid 1 Fragment 3(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 3, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }

            if (_asteroidObject.name == "Asteroid 2 Fragment 1" || _asteroidObject.name == "Asteroid 2 Fragment 1(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 4, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else if (_asteroidObject.name == "Asteroid 2 Fragment 2" || _asteroidObject.name == "Asteroid 2 Fragment 2(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 5, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
            }
            else if (_asteroidObject.name == "Asteroid 2 Fragment 3" || _asteroidObject.name == "Asteroid 2 Fragment 3(Clone)")
            {
                theWorld.currentStarSystem.asteroids.Add(new Asteroid("Fragment", 6, _asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
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

    public void CreateNewAsteroid()
    {
        Asteroid newAsteroid = new Asteroid(Random.Range(1, 3), new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0), new Vector3(0, 0, Random.Range(0, 360)));
        while (newAsteroid.asteroidPos.x <= 5f && newAsteroid.asteroidPos.x >= -5f || newAsteroid.asteroidPos.y <= 5f && newAsteroid.asteroidPos.y >= -5f)
        {
            newAsteroid = new Asteroid(Random.Range(1, 3), new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0), new Vector3(0, 0, Random.Range(0, 360)));
        }

        theWorld.currentStarSystem.asteroids.Add(newAsteroid);

        if (newAsteroid.asteroidVariation == 1)
        {
            GameObject thisAsteroid = GameObject.Instantiate(asteroidPrefab, newAsteroid.asteroidPos, Quaternion.Euler(newAsteroid.asteroidRot));
        }
        else if (newAsteroid.asteroidVariation == 2)
        {
            GameObject thisAsteroid = GameObject.Instantiate(asteroid2Prefab, newAsteroid.asteroidPos, Quaternion.Euler(newAsteroid.asteroidRot));
        }
    }
}
