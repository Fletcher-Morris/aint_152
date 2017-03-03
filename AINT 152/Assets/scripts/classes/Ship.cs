using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship{

    public string shipName;
    public int health;

    public Vector3 pos;
    public Vector3 rot;

    public Ship()
    {
        shipName = "New Ship";

        health = 100;

        pos = new Vector3(0, 0, 0);
        rot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;

        health = _health;

        pos = new Vector3(0, 0, 0);
        rot = new Vector3(0, 0, 0);
    }
}
