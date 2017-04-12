using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            theWorld.enemyShips = null;
            theWorld.asteroids = null;
            nameOfWorldToLoad = null;
        }

        if (level == 1)
        {
            Debug.Log(gameObject.name + ": this is the Game Scene.");
            theWorld = theWorld.LoadWorld(nameOfWorldToLoad);
            GenerateWorld();
        }
    }

    public void GenerateWorld()
    {
        GenerateEnemyShips();
        GenerateAsteroids();
    }

    public void GenerateEnemyShips()
    {
        foreach (Ship _enemyShip in theWorld.enemyShips)
        {
            GameObject thisShip = GameObject.Instantiate(enemyShipPrefab, _enemyShip.shipPos, Quaternion.Euler(_enemyShip.shipRot));
        }
    }

    public void GenerateAsteroids()
    {
        foreach (Asteroid _asteroid in theWorld.asteroids)
        {
            GameObject thisAsteroid = GameObject.Instantiate(asteroidPrefab, _asteroid.asteroidPos, Quaternion.Euler(_asteroid.asteroidRot));
        }

    }

    public void SaveTheWorld()
    {
        theWorld.playerShip = GameObject.Find("Player Ship").GetComponent<ShipSetup_Script>().shipDetails;

        theWorld.asteroids.Clear();
        foreach(GameObject _asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            theWorld.asteroids.Add(new Asteroid(_asteroidObject.transform.position, _asteroidObject.transform.rotation.eulerAngles));
        }

        theWorld.enemyShips.Clear();
        foreach (GameObject _enemyShipObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            theWorld.enemyShips.Add(new Ship(_enemyShipObject.GetComponent<ShipSetup_Script>().shipDetails.shipName, _enemyShipObject.GetComponent<ShipSetup_Script>().shipDetails.shipHealth, _enemyShipObject.transform.position, _enemyShipObject.transform.rotation.eulerAngles));
        }

        theWorld.SaveWorld(theWorld);
    }
}
