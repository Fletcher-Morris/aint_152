using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class World {

    public string worldName;
    public int currentWave;
    public int highScore;
    public int money;
	public bool hardcore;

	public bool hasIonBlaster;
	public int ionBlasterLvl;
	public int ionBlasterProgress;

	public bool hasQuantumPrism;
	public int quantumPrismLvl;
	public int quantumPrismProgress;

    public Ship playerShip;
    public List<Ship> enemyShips;
    public List<Asteroid> asteroids;

    public World()
    {
        worldName = "New World";
        currentWave = 1;
        highScore = 0;
        money = 0;
		hardcore = false;

		hasIonBlaster = true;
		ionBlasterLvl = 1;
		ionBlasterProgress = 0;

		hasQuantumPrism = false;
		quantumPrismLvl = 0;
		quantumPrismProgress = 0;

        playerShip = new Ship();
        enemyShips = new List<Ship>();
        asteroids = new List<Asteroid>();
    }

    public World(string _worldName)
    {
        worldName = _worldName;
        currentWave = 1;
        highScore = 0;
        money = 0;
		hardcore = false;
        playerShip = new Ship();
        enemyShips = new List<Ship>();
        asteroids = new List<Asteroid>();
    }

    public void SaveWorld()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
            File.WriteAllText(Application.dataPath + "/Saves/" + worldName + "/world.json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Saves/" + worldName);
            SaveWorld();
        }
    }

    public void SaveWorld(World _world)
    {
        string jsonString = JsonUtility.ToJson(_world);
        try
        {
            File.WriteAllText(Application.dataPath + "/Saves/" + _world.worldName + "/world.json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Saves/" + _world.worldName);
            SaveWorld(_world);
        }
    }

    public World LoadWorld()
    {

        World _world = new World();
        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Saves/" + worldName + "/world.json");
            _world = JsonUtility.FromJson<World>(jsonString);
        }
        catch (System.Exception)
        {
            SaveWorld();
        }
        Debug.Log("Loading world file.");
        return _world;
    }

    public World LoadWorld(string _worldName)
    {

        World _world = new World();
        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Saves/" + _worldName + "/world.json");
            _world = JsonUtility.FromJson<World>(jsonString);
        }
        catch (System.Exception)
        {
            SaveWorld();
        }
        Debug.Log("Loading world file.");
        return _world;
    }
}