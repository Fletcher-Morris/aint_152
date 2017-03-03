﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class World {

    public string worldName;
    public string[] bannedIp;

    public List<Ship> ships;

    public World()
    {
        worldName = "New World";
        bannedIp = new string[1];
        ships = new List<Ship>();
    }

    public World(string _worldName)
    {
        worldName = _worldName;
        bannedIp = new string[1];
        ships = new List<Ship>();
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
            Directory.CreateDirectory(Application.dataPath + "/Saves/" + worldName + "/world.json");
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
}