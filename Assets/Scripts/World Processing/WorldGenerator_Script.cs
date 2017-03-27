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
        if(_world.worldName == "")
        {
            _world.worldName = "New World";
        }

        GameObject.Find("Loading Panel").transform.FindChild("Loading Info").gameObject.SetActive(true);
        GameObject.Find("Loading Panel").transform.FindChild("Loading Info Background").gameObject.SetActive(true);

        _world.playerData = new Player(new GamePrefs().playerName);

        GameObject.Find("Loading Panel").transform.FindChild("Loading Info").GetComponent<Text>().text = "GENERATING ASTEROIDS...";
        CreateAsteroids();

        GameObject.Find("Loading Panel").transform.FindChild("Loading Info").GetComponent<Text>().text = "SAVING THE WORLD...";
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
            _world.asteroids.Add(newAsteroid);
            asteroidsRemaining--;
        }
    }
}
