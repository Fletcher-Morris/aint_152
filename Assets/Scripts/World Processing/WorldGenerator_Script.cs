using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorldGenerator_Script : MonoBehaviour {

    public World _world;

    int numberOfAsteroids = 100;

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
        _world.players.Add(new Player(new GamePrefs().playerName));

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
            _world.asteroids.Add(new Asteroid(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0), new Vector3(0,0, Random.Range(0, 360))));
            asteroidsRemaining--;
        }
    }
}
