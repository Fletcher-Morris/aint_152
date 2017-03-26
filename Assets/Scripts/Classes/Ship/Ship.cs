using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Ship
{
    public string shipName;

    public int shipHealth;
    
    public Engine shipEngine;
    public Shield shipShield;
    public Turret shipTurret;

    public Vector3 shipPos;
    public Vector3 shipRot;

    public Ship()
    {
        shipName = "New Ship";
        shipHealth = 100;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health)
    {
        shipName = _name;
        shipHealth = _health;
        shipEngine = new Engine();
        shipShield = new Shield();
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }

    public Ship(string _name, int _health, Engine _engine, Shield _shield, Turret _turret)
    {
        shipName = _name;
        shipHealth = _health;
        shipEngine = _engine;
        shipShield = _shield;
        shipTurret = new Turret();

        shipPos = new Vector3(0, 0, 0);
        shipRot = new Vector3(0, 0, 0);
    }
}
