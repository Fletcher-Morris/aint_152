using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship{

    public string shipName;
    public int shipHealth;

	public Reactor shipReactor;
	public Engine shipEngine;
	public Shield shipShield;

    public Vector3 shipPos;
    public Vector3 shipRot;

    public Ship()
    {
        shipName = "New Ship";
        shipHealth = 100;
		shipReactor = new Reactor ("Magnox Fusion", 1000000000, 2000);
		shipEngine = new Engine ();
		shipShield = new Shield ();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;
        shipHealth = _health;
		shipReactor = new Reactor ("Antimatter", 2000000000, 1500);
		shipEngine = new Engine ();
		shipShield = new Shield ();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }
}
