using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WorldLoader_Script : MonoBehaviour {

    public World theWorld;

    public string nameOfWorldToLoad;

    public GameObject playerShipPrefab;
    public GameObject aiShipPrefab;

    public void LoadSelectedWorld()
    {
        theWorld = theWorld.LoadWorld(nameOfWorldToLoad);
        gameObject.GetComponent<NetworkLauncher_Script>().SetupHost();
        GenerateWorld();
    }

    void Start()
    {
        //CheckClient();
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            theWorld.worldName = null;
            theWorld.bannedIp = null;
            theWorld.players = null;
            theWorld.aiShips = null;
            theWorld.players = null;
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
        GeneratePlayerShips();
        GenerateAIShips();
    }

    public void GeneratePlayerShips()
    {
        foreach (Ship _ship in theWorld.playerShips)
        {
            GameObject thisShip = GameObject.Instantiate(playerShipPrefab, _ship.shipPos, Quaternion.Euler(_ship.shipRot));
            thisShip.GetComponent<ShipSetup_Script>().shipDetails = _ship;
            thisShip.name = "Ship_" + _ship.shipName;
            NetworkServer.Spawn(thisShip);
        }
    }

    public void GenerateAIShips()
    {
        foreach (Ship _ship in theWorld.aiShips)
        {
            GameObject thisShip = GameObject.Instantiate(aiShipPrefab, _ship.shipPos, Quaternion.Euler(_ship.shipRot));
            thisShip.GetComponent<ShipSetup_Script>().shipDetails = _ship;
            thisShip.name = "Ship_" + _ship.shipName;
            NetworkServer.Spawn(thisShip);
        }
    }
}
