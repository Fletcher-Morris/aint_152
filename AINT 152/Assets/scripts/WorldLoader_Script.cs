using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject.Instantiate(shipPrefab, _ship.shipPos, Quaternion.Euler(_ship.shipRot));
        }
    }
}
