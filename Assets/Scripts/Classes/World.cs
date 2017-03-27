using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class World {

    public string worldName;
    public string[] bannedIp;
    public int currentWave;
    public Player playerData;
    public List<Ship> playerShips;
    public List<Ship> enemyShips;
    public List<Asteroid> asteroids;

    public World()
    {
        worldName = "New World";
        bannedIp = new string[1];
        currentWave = 1;
        playerData = new Player();
        playerShips = new List<Ship>();
        enemyShips = new List<Ship>();
        asteroids = new List<Asteroid>();
    }

    public World(string _worldName)
    {
        worldName = _worldName;
        bannedIp = new string[1];
        currentWave = 1;
        playerData = new Player();
        playerShips = new List<Ship>();
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