using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorldGenerator_Script : MonoBehaviour {

    public World _world;

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

        _world.playerShips.Add(new Ship("The Everest", 1000, false));

        _world.SaveWorld();

        GameObject.Find("WM").GetComponent<WorldLoader_Script>().nameOfWorldToLoad = _world.worldName;
        Debug.Log(gameObject.name + ": World to load set to " + _world.worldName + ".");

        GameObject.Find("WM").GetComponent<WorldLoader_Script>().LoadSelectedWorld(); ;
    }
}
