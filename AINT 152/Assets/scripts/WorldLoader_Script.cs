using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorldLoader_Script : NetworkBehaviour {

    public World theWorld;

    public GameObject shipPrefab;

    void Start()
    {
        if (isClient)
        {
            Destroy(gameObject);
            Debug.LogWarning("This is not the server instance of the game, deleting WM.");
            return;
        }

        theWorld.worldName = "New World";
        theWorld = theWorld.LoadWorld();

        GenerateWorld();
    }

    public void GenerateWorld()
    {
        GenerateShips();
    }

    public void GenerateShips()
    {
        foreach (Ship _ship in theWorld.playerShips)
        {
            GameObject thisShip = GameObject.Instantiate(shipPrefab, _ship.shipPos, Quaternion.Euler(_ship.shipRot));
            thisShip.GetComponent<ShipSetup_Script>().shipDetails = _ship;
            thisShip.name = "Ship_" + _ship.shipName;
            NetworkServer.Spawn(thisShip);
        }
    }
}
