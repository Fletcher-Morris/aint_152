using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class World {

    public string worldName;
    public string[] bannedIp;

    public Ship[] ships;

    public World()
    {
        worldName = "newWorld";
        bannedIp = new string[1];
    }

    public void SaveWorld()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
            File.WriteAllText(Application.dataPath + "/World.json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/World.json");
        }
    }

    public void SaveWorld(World _world)
    {
        string jsonString = JsonUtility.ToJson(_world);
        try
        {
            File.WriteAllText(Application.dataPath + "/World.json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/World.json");
        }
    }

    public World LoadWorld()
    {
        World _world = new World();
        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/World.json");
            _world = JsonUtility.FromJson<World>(jsonString);
        }
        catch (System.Exception)
        {
            SaveWorld(_world);
        }
        Debug.Log("Loading world file.");
        return _world;
    }
}
