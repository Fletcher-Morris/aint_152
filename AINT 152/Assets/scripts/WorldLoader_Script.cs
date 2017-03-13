using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldLoader_Script : MonoBehaviour {

    public World theWorld;

    public GameObject shipPrefab;

    void Start()
    {
        theWorld.worldName = "New World";
        theWorld = theWorld.LoadWorld();

        GenerateWorld();
    }

    public void GenerateWorld()
    {
        foreach(Ship _ship in theWorld.ships)
        {
            GameObject thisShip = GameObject.Instantiate(shipPrefab, _ship.shipPos, Quaternion.Euler(_ship.shipRot));
            thisShip.name = "Ship_" + _ship.shipName;
            NetworkServer.Spawn(thisShip);
        }
    }
}
