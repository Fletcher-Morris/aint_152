using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorldGenerator_Script : MonoBehaviour {

    public World _world;

    int numberOfAsteroids = 100;

    float uiTimer = 1f;

    void Start()
    {

    }

    public void CreateWorld()
    {
        _world = new World();
        _world.worldName = GameObject.Find("Save Name Field").GetComponent<InputField>().text;
		_world.hardcore = GameObject.Find ("Hardcore Toggle").GetComponent<Toggle>().isOn;

		_world.starSystems.Add (new StarSystem ());
		_world.starSystems.Add (new StarSystem ());

        if(_world.worldName == "")
        {
            _world.worldName = "New World";
        }

        _world.playerShip = new Ship();
		_world.playerShip.shipTurret.AddWeapon (GameObject.Find("GM").GetComponent<WeaponData_Script>().ionBlasterUpgrades[0]);

        CreateAsteroids();
        
        _world.SaveWorld();

        GameObject.Find("WM").GetComponent<WorldLoader_Script>().nameOfWorldToLoad = _world.worldName;
        Debug.Log(gameObject.name + ": World to load set to " + _world.worldName + ".");

        GameObject.Find("WM").GetComponent<WorldLoader_Script>().LoadSelectedWorld(); ;
    }

    void CreateAsteroids()
    {
        int asteroidsRemaining = numberOfAsteroids;

        while(asteroidsRemaining > 0)
        {
            Asteroid newAsteroid = new Asteroid(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0), new Vector3(0, 0, Random.Range(0, 360)));
            while(newAsteroid.asteroidPos.x <= 5f && newAsteroid.asteroidPos.x >= -5f || newAsteroid.asteroidPos.y <= 5f && newAsteroid.asteroidPos.y >= -5f)
            {
                newAsteroid = new Asteroid(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0), new Vector3(0, 0, Random.Range(0, 360)));
            }
			_world.currentStarSystem.asteroids.Add(newAsteroid);
            asteroidsRemaining--;
        }
    }
}
