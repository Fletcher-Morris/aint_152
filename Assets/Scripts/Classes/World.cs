using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class World {

    public string worldName;
    public int currentWave;
    public int score;
    public int money;
	public int gold;
	public bool hardcore;

	public bool hasIonBlaster;
	public int ionBlasterLvl;
	public int ionBlasterProgress;

	public bool hasFusionMine;
	public int fusionMineLvl;
	public int fusionMineProgress;

	public bool hasHunterLauncher;
	public int hunterLauncherLvl;
	public int hunterLauncherProgress;

	public bool hasQuantumPrism;
	public int quantumPrismLvl;
	public int quantumPrismProgress;

    public Ship playerShip;

	public List<StarSystem> starSystems;
	public StarSystem currentStarSystem;

	public List<Mission> activeMissions;
	public List<Mission> completedMissions;

    public World()
    {
        worldName = "New World";
        currentWave = 1;
		score = 0;
        money = 0;
		gold = 0;
		hardcore = false;

		hasIonBlaster = true;
		ionBlasterLvl = 0;
		ionBlasterProgress = 0;

		hasHunterLauncher = false;
		hunterLauncherLvl = 0;
		hunterLauncherProgress = 0;

		hasFusionMine = false;
		fusionMineLvl = 0;
		fusionMineProgress = 0;

		hasQuantumPrism = false;
		quantumPrismLvl = 0;
		quantumPrismProgress = 0;

        playerShip = new Ship();
		starSystems = new List<StarSystem> ();
		currentStarSystem = new StarSystem ();


    }

    public World(string _worldName)
    {
        worldName = _worldName;
        currentWave = 1;
		score = 0;
		money = 0;
		gold = 0;
		hardcore = false;
        playerShip = new Ship();
		starSystems = new List<StarSystem> ();
		currentStarSystem = new StarSystem ();
    }

    public void SaveWorld()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
			File.WriteAllText(Application.dataPath + "/Data/Saves/" + worldName + ".json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
			Directory.CreateDirectory(Application.dataPath + "/Data/Saves/");
            SaveWorld();
        }
    }

    public void SaveWorld(World _world)
    {
        string jsonString = JsonUtility.ToJson(_world);
        try
        {
			File.WriteAllText(Application.dataPath + "/Data/Saves/" + _world.worldName + ".json", jsonString.ToString());
            Debug.Log("Saving world file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find world file. Creating a new one.");
			Directory.CreateDirectory(Application.dataPath + "/Data/Saves/");
            SaveWorld(_world);
        }
    }

    public World LoadWorld()
    {

        World _world = new World();
        try
        {
			string jsonString = File.ReadAllText(Application.dataPath + "/Data/Saves/" + worldName + ".json");
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
			string jsonString = File.ReadAllText(Application.dataPath + "/Data/Saves/" + _worldName + ".json");
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